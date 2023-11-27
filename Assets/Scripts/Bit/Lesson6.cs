using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Lesson6 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 反序列化之 反序列化文件中数据
        //主要类
        //FileStream文件流类
        //BinaryFormatter 2进制格式化类
        //主要方法
        //Deserizlize
        //通过文件流打开指定的2进制数据文件
        using (FileStream fs = File.Open(Application.dataPath + "/Lesson5_2.tang", FileMode.Open, FileAccess.Read))
        {
            //申明一个 2进制格式化类
            BinaryFormatter bf = new BinaryFormatter();
            //反序列化
            Person p = bf.Deserialize(fs) as Person;

            fs.Close();
        }

        #endregion

        #region 知识点二 反序列化之 反序列化网络传输过来的2进制数据
        //主要类
        //MemoryStream内存流类
        //BinaryFormatter 2进制格式化类
        //主要方法
        //Deserizlize
        //目前没有网络传输 我们还是直接从文件中获取
        byte[] bytes = File.ReadAllBytes(Application.dataPath + "/Lesson5_2.tang");
        //申明内存流对象 一开始就把字节数组传输进去
        using (MemoryStream ms = new MemoryStream(bytes))
        {
            //申明一个 2进制格式化类
            BinaryFormatter bf = new BinaryFormatter();
            //反序列化
            Person p = bf.Deserialize(ms) as Person;

            ms.Close();
        }

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
