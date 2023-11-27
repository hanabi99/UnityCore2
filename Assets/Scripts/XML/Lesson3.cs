using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

namespace xml
{
    public class TestLesson3 : IXmlSerializable
    {
        public int test1 = 10;
        public int test2 = 99;

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            //读属性
            //test1 = int.Parse(reader["Test1"]);
            //test2 = int.Parse(reader["Test2"]);

            //读节点
            //方式一
            //reader.Read();//这时读到的是节点
            //reader.Read();//这时读到的才是值
            //test1 = int.Parse(reader.Value);//得到值内容
            //reader.Read();//得到节点尾部配对
            //reader.Read();//读到节点开头
            //reader.Read();//读到值
            //test2 = int.Parse(reader.Value);//获取值内容
            //方式二
            //while (reader.Read())
            //{
            //    if(reader.NodeType == XmlNodeType.Element)
            //    {
            //        switch (reader.Name)
            //        {
            //            case "Test1":
            //                reader.Read();
            //                test1 = int.Parse(reader.Value) ;
            //                break;
            //            case "Test2":
            //                reader.Read();
            //                test2 = int.Parse(reader.Value);
            //                break;
            //        }
            //    }
            //}

            //读包裹点
            //XmlSerializer s = new XmlSerializer(typeof(int));
            //reader.Read();
            //reader.ReadStartElement("Test1");
            //test1 = (int)s.Deserialize(reader);
            //reader.ReadEndElement();
            //reader.ReadStartElement("Test2");
            //test1 = (int)s.Deserialize(reader);
            //reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            //写属性
            //writer.WriteAttributeString("Test1", test1.ToString());
            //writer.WriteAttributeString("Test2", test2.ToString());

            //写节点
            //writer.WriteElementString("Test1", test1.ToString());
            //writer.WriteElementString("Test2", test2.ToString());

            //写包裹节点
            XmlSerializer s = new XmlSerializer(typeof(Lesson1Test));
            writer.WriteStartElement("Test1");
            s.Serialize(writer, new Lesson1Test());
            writer.WriteEndElement();

            writer.WriteStartElement("Test2");
            s.Serialize(writer, new Lesson1Test());
            writer.WriteEndElement();

        }
    }

    public class Lesson3 : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            #region 知识点一 IXmlSerializable是什么
            //C# 的XmlSerializer 提供了可拓展内容 
            //可以让一些不能被序列化和反序列化的特殊类能被处理
            //让特殊类继承 IXmlSerializable 接口 实现其中的方法即可
            #endregion

            #region 知识点二 自定义类实践
            TestLesson3 t = new TestLesson3();
            Debug.Log(Application.persistentDataPath);
            using (StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/test.xml"))
            {
                XmlSerializer s = new XmlSerializer(typeof(TestLesson3));
                s.Serialize(writer, t);
            }

            using (StreamReader reader = new StreamReader(Application.persistentDataPath + "/test.xml"))
            {
                XmlSerializer s = new XmlSerializer(typeof(TestLesson3));
                t = s.Deserialize(reader) as TestLesson3;
            }
            #endregion
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
