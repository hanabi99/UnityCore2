using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;


namespace xml {
    public class Lesson2 : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            #region 知识回顾
            // 序列化 就是把类对象 转换为可存储和传输的数据
            // 反序列化 就是把存储或收到的数据 转换为 类对象

            // xml序列化关键知识
            // 1.using 和 StreamWriter
            // 2.XmlSerializer 的 Serialize序列化方法
            #endregion

            #region 知识点一 判断文件是否存在
            string path = Application.persistentDataPath + "/Lesson1Test.xml";
            if (File.Exists(path))
            {
                #region 知识点二 反序列化
                //关键知识
                // 1.using 和 StreamReader
                // 2.XmlSerializer 的 Deserialize反序列化方法

                //读取文件
                using (StreamReader reader = new StreamReader(path))
                {
                    //产生了一个 序列化反序列化的翻译机器
                    XmlSerializer s = new XmlSerializer(typeof(Lesson1Test));
                    Lesson1Test lt = s.Deserialize(reader) as Lesson1Test;
                }
                #endregion
            }
            #endregion

            #region 总结
            //1.判断文件是否存在 File.Exists
            //2.文件流获取 StreamReader reader = new StreamReader(path)
            //3.根据文件流 XmlSerializer通过Deserialize反序列化 出对象

            //注意：List对象 如果有默认值 反序列化时 不会清空 会往后面添加
            #endregion
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
