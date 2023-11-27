using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace xml
{
    public class TestLesson4
    {
        public int test1;

        public SerizlizerDictionary<int, string> dic;
    }

    public class Lesson4 : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            #region 知识点一 思考如何让Dictionary支持xml序列和反序列化
            //1.我们没办法修改C#自带的类
            //2.那我们可以重写一个类 继承Dictionary 然后让这个类继承序列化拓展接口IXmlSerializable
            //3.实现里面的序列化和反序列化方法即可
            #endregion

            #region 知识点二 让Dictionary支持序列化和反序列化
            TestLesson4 tl4 = new TestLesson4();
            //tl4.dic = new SerizlizerDictionary<int, string>();
            //tl4.dic.Add(1, "123");
            //tl4.dic.Add(2, "234");
            //tl4.dic.Add(3, "345");
            string path = Application.persistentDataPath + "/TestLesson4.xml";
            //using(StreamWriter writer = new StreamWriter(path))
            //{
            //    XmlSerializer s = new XmlSerializer(typeof(TestLesson4));
            //    s.Serialize(writer, tl4);
            //}

            using (StreamReader reader = new StreamReader(path))
            {
                XmlSerializer s = new XmlSerializer(typeof(TestLesson4));
                tl4 = s.Deserialize(reader) as TestLesson4;
            }

            #endregion
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
