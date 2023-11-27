using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Excel;
using System.Data;
using System;
using System.Text;

public class ExcelTool
{
    /// <summary>
    /// excel ���·��
    /// </summary>
    public static string EXCEL_PATH = Application.dataPath + "/ExcelTable/";

    /// <summary>
    /// ���ݽṹ�����ɵ�ַ
    /// </summary>
    public static string INITDATA_PATH = Application.dataPath + "/Scripts/ExcelData/";
    /// <summary>
    /// ���������ɵ�ַ
    /// </summary>
    public static string INITCONTAINER_PATH = Application.dataPath + "/Scripts/ExcelData/";

 

    public static int BEGIN_INDEX = 4;


    [MenuItem("GameTool/GenerateExcel")]
    private static void GenerateExcelInfo()
    {
        DirectoryInfo dInfo = Directory.CreateDirectory(EXCEL_PATH);

        FileInfo[] fileInfos =  dInfo.GetFiles();

        DataTableCollection tableCollection;

        for (int i = 0; i < fileInfos.Length; i++)
        {
            if(fileInfos[i].Extension != ".xlsx" && fileInfos[i].Extension != ".xls")
            {
                continue;
            }

            using (FileStream fs = fileInfos[i].Open(FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                tableCollection = excelDataReader.AsDataSet().Tables ;
                fs.Close();
            }

            foreach (DataTable item in tableCollection)
            {
                Debug.Log(item.TableName);
                //�������ݽṹ��
                GenerateExcelTableClass(item);
                //����������
                GenerateExcelContainer(item);
                //����2��������
                GenerateExcelBinary(item);
            }

            AssetDatabase.Refresh();
        }
    }
    public static void GenerateExcelBinary(DataTable table)
    {
        if (!Directory.Exists(BinaryDataMgr.INITBINART_PATH))
        {
            Directory.CreateDirectory(BinaryDataMgr.INITBINART_PATH);
        }
        using (FileStream fs = File.Open(BinaryDataMgr.INITBINART_PATH + table.TableName +".Binary", FileMode.OpenOrCreate, FileAccess.Write))
        {
            //�洢һ������������
            fs.Write(BitConverter.GetBytes(table.Rows.Count - 4), 0, 4);
            string keyname = GetVarNameRow(table)[GetKeyIndex(table)].ToString();
            //�洢����������
            byte[] bytes = Encoding.UTF8.GetBytes(keyname);
            //�洢�������ַ�������
            fs.Write(BitConverter.GetBytes(bytes.Length), 0, 4);
            fs.Write(bytes, 0, bytes.Length);

            //�����������ݵ��� ����2����д��
            DataRow row;
            DataRow rowtype = GetTypeNameRow(table);
            for (int i = BEGIN_INDEX; i < table.Rows.Count; i++)
            {
                row = table.Rows[i];
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    switch (rowtype[j].ToString())
                    {
                        case "int":
                            fs.Write(BitConverter.GetBytes(int.Parse(row[j].ToString())), 0, 4);
                            break;
                        case "float":
                            fs.Write(BitConverter.GetBytes(float.Parse(row[j].ToString())), 0, 4);
                            break;
                        case "bool":
                            fs.Write(BitConverter.GetBytes(bool.Parse(row[j].ToString())), 0, 1);
                            break;
                        case "string":
                            bytes = Encoding.UTF8.GetBytes(row[j].ToString());
                            fs.Write(BitConverter.GetBytes(bytes.Length), 0, 4);
                            fs.Write(bytes, 0, bytes.Length);
                            break;
                    }
                }
            }
            fs.Close();
        }
    }
   
    /// <summary>
    /// �������ݽṹ��
    /// </summary>
    /// <param name="table"></param>
    private static void GenerateExcelTableClass(DataTable table)
    {
        DataRow rowName = GetVarNameRow(table);
        DataRow typeName = GetTypeNameRow(table);

        if (!Directory.Exists(INITDATA_PATH))
        {
            Directory.CreateDirectory(INITDATA_PATH);
        }

        string str = "public class " + table.TableName + "\n{\n";

        //�ַ���ƴ��
        for (int i = 0; i < table.Columns.Count; i++)
        {
            str += "    public" + " "+ typeName[i].ToString() + " " + rowName[i].ToString() + ";\n";
        }

        str += "}";

        File.WriteAllText(INITDATA_PATH + table.TableName + ".cs", str);
    }

    public static void  GenerateExcelContainer(DataTable table)
    {
        //�õ�����������
        int keyIndex = GetKeyIndex(table);
        //�õ��ֶ�������
        DataRow rowType = GetTypeNameRow(table);

        if (!Directory.Exists(INITCONTAINER_PATH))
        {
            Directory.CreateDirectory(INITCONTAINER_PATH);
        }

        string str = "using System.Collections.Generic;\n";

        str += "public class " + table.TableName + "Container" + "\n{\n";

        str += "      " + "public Dictionary<" + rowType[keyIndex].ToString() + "," + table.TableName + ">"+"dataDic= new " + "Dictionary<" + rowType[keyIndex].ToString() + "," + table.TableName + ">();\n";

        str += "}";

        File.WriteAllText(INITCONTAINER_PATH + table.TableName + "Container"+".cs", str);
    }

    /// <summary>
    /// ��ȡ������
    /// </summary>
    public static DataRow GetVarNameRow(DataTable table)
    {
       return table.Rows[0];
    }
    /// <summary>
    /// ��ȡ����������
    /// </summary>
    public static DataRow GetTypeNameRow(DataTable table)
    {
        return table.Rows[1];
    }

    public static int GetKeyIndex(DataTable dataTable)
    {

        DataRow row = dataTable.Rows[2];
        for (int i = 0; i < dataTable.Columns.Count; i++)
        {
            if(row[i].ToString() == "key")
            {
                return i;
            }
        }
        return 0;
    }



    [MenuItem("GameTool/��Excel��")]
    private static void OpenExcel()
    {
        using (FileStream fs = File.Open(Application.dataPath + "/ExcelTable/PlayerInfo.xlsx", FileMode.Open, FileAccess.Read))
        {
            //ͨ�����ǵ��ļ�����ȡExcel����
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
            //��excel���е�����ת��ΪDataSet�������� �������� ��ȡ���е�����
            DataSet result = excelReader.AsDataSet();
            //�õ�Excel�ļ��е����б���Ϣ
            for (int i = 0; i < result.Tables.Count; i++)
            {
                Debug.Log("������" + result.Tables[i].TableName);
                Debug.Log("������" + result.Tables[i].Rows.Count);
                Debug.Log("������" + result.Tables[i].Columns.Count);
            }
            fs.Close();
        }
    }

    [MenuItem("GameTool/��ȡExcel��ľ�����Ϣ")]
    private static void ReadExcel()
    {
        using (FileStream fs = File.Open(Application.dataPath + "/ExcelTable/PlayerInfo.xlsx", FileMode.Open, FileAccess.Read))
        {
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
            DataSet result = excelReader.AsDataSet();

            for (int i = 0; i < result.Tables.Count; i++)
            {
                //�õ�����һ�ű�ľ�������
                DataTable table = result.Tables[i];
                //�õ�����һ�е�����
                //DataRow row = table.Rows[0];
                //�õ�����ĳһ�е���Ϣ
                //Debug.Log(row[1].ToString());
                DataRow row;
                for (int j = 0; j < table.Rows.Count; j++)
                {
                    //�õ�ÿһ�е���Ϣ
                    row = table.Rows[j];
                    Debug.Log("*********�µ�һ��************");
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        Debug.Log(row[k].ToString());
                    }
                }
            }

            fs.Close();
        }
    }

}
