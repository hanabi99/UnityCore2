using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;


namespace xml
{
    public class Lesson1Test
    {
        [XmlElement("testPublic123123")]//改名字
        public int testPublic;
        private int testPrivate;
        protected int testProtected;
        internal int testInternal;

        public string testPUblicStr;

        public int testPro { get; set; }

        public Lesson1Test2 testClass = new Lesson1Test2();

        public int[] arrayInt;
        [XmlArray("IntList")]
        [XmlArrayItem("Int32")]//列表元素名字改变
        public List<int> listInt;
        public List<Lesson1Test2> listItem;

        //不支持字典
        //public Dictionary<int, string> testDic = new Dictionary<int, string>() { { 1, "123" } };
    }

    public class Lesson1Test2
    {
        [XmlAttribute("Test1")]//变为属性方式
        public int test1 = 1;
        [XmlAttribute()]
        public float test2 = 1.1f;
        [XmlAttribute()]
        public bool test3 = true;
    }


    public class Lesson1 : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            #region 知识点一 什么是序列化和反序列化
            //序列化：把对象转化为可传输的字节序列过程称为序列化
            //反序列化：把字节序列还原为对象的过程称为反序列化

            //说人话：
            //序列化就是把想要存储的内容转换为字节序列用于存储或传递
            //反序列化就是把存储或收到的字节序列信息解析读取出来使用
            #endregion

            #region 知识点二 xml序列化
            //1.第一步准备一个数据结构类
            Lesson1Test lt = new Lesson1Test();
            //2.进行序列化
            //  关键知识点
            //  XmlSerializer 用于序列化对象为xml的关键类
            //  StreamWriter 用于存储文件  
            //  using 用于方便流对象释放和销毁

            //第一步：确定存储路径
            string path = Application.persistentDataPath + "/Lesson1Test.xml";
            print(Application.persistentDataPath);
            //第二步：结合 using知识点 和 StreamWriter这个流对象 来写入文件
            // 括号内的代码：写入一个文件流 如果有该文件 直接打开并修改 如果没有该文件 直接新建一个文件
            // using 的新用法 括号当中包裹的声明的对象 会在 大括号语句块结束后 自动释放掉 
            // 当语句块结束 会自动帮助我们调用 对象的 Dispose这个方法 让其进行销毁
            // using一般都是配合 内存占用比较大 或者 有读写操作时  进行使用的 
            using (StreamWriter stream = new StreamWriter(path))
            {
                //第三步：进行xml文件序列化
                XmlSerializer s = new XmlSerializer(typeof(Lesson1Test));
                //这句代码的含义 就是通过序列化对象 对我们类对象进行翻译 将其翻译成我们的xml文件 写入到对应的文件中
                //第一个参数 ： 文件流对象
                //第二个参数: 想要备翻译 的对象
                //注意 ：翻译机器的类型 一定要和传入的对象是一致的 不然会报错
                s.Serialize(stream, lt);
            }
            #endregion

            #region 知识点三 自定义节点名 或 设置属性
            //可以通过特性 设置节点或者设置属性 并且修改名字
            #endregion

            #region 总结
            //序列化流程
            //1.有一个想要保存的类对象
            //2.使用XmlSerializer 序列化该对象
            //3.通过StreamWriter 配合 using将数据存储 写入文件
            //注意：
            //1.只能序列化公共成员
            //2.不支持字典序列化
            //3.可以通过特性修改节点信息 或者设置属性信息
            //4.Stream相关要配合using使用
            #endregion
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}
