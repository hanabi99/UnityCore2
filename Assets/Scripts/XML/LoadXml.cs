using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class LoadXml : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //C#读取XML的方法有几种 
        //1.XmlDocument    (把数据加载到内存中，方便读取)
        //2.XmlTextReader  (以流形式加载，内存占用更少，但是是单向只读，使用不是特别方便，除非有特殊需求，否则不会使用)
        //3.Linq           (以后专门讲Linq的时候讲)

        //使用XmlDocument类读取是较方便最容易理解和操作的方法
        #region 知识点一 读取xml文件信息
        XmlDocument xml = new XmlDocument();
        //通过XmlDocument读取xml文件 有两个API
        //1.直接根据xml字符串内容 来加载xml文件
        //存放在Resorces文件夹下的xml文件加载处理
        TextAsset asset = Resources.Load<TextAsset>("TestXml");
        print(asset.text);
        //通过这个方法 就能够翻译字符串为xml对象
        xml.LoadXml(asset.text);

        //2.是通过xml文件的路径去进行加载
        xml.Load(Application.streamingAssetsPath + "/TestXml.xml");
        #endregion

        #region 知识点二 读取元素和属性信息
        //节点信息类
        //XmlNode 单个节点信息类
        //节点列表信息
        //XmlNodeList 多个节点信息类

        //获取xml当中的根节点
        XmlNode root = xml.SelectSingleNode("Root");
        //再通过根节点 去获取下面的子节点
        XmlNode nodeName = root.SelectSingleNode("name");
        //如果想要获取节点包裹的元素信息 直接 .InnerText
        print(nodeName.InnerText);

        XmlNode nodeAge = root.SelectSingleNode("age");
        print(nodeAge.InnerText);

        XmlNode nodeItem = root.SelectSingleNode("Item");
        //第一种方式 直接 中括号获取信息
        print(nodeItem.Attributes["id"].Value);
        print(nodeItem.Attributes["num"].Value);
        //第二种方式 
        print(nodeItem.Attributes.GetNamedItem("id").Value);
        print(nodeItem.Attributes.GetNamedItem("num").Value);

        //这里是获取 一个节点下的同名节点的方法
        XmlNodeList friendList = root.SelectNodes("Friend");

        //遍历方式一：迭代器遍历
        //foreach (XmlNode item in friendList)
        //{
        //    print(item.SelectSingleNode("name").InnerText);
        //    print(item.SelectSingleNode("age").InnerText);
        //}
        //遍历方式二：通过for循环遍历
        //通过XmlNodeList中的 成员变量 Count可以得到 节点数量
        for (int i = 0; i < friendList.Count; i++)
        {
            print(friendList[i].SelectSingleNode("name").InnerText);
            print(friendList[i].SelectSingleNode("age").InnerText);
        }
        #endregion

        #region 总结
        //1.读取XML文件
        //XmlDocument xml = new XmlDocument();
        //读取文本方式1-xml.LoadXml(传入xml文本字符串)
        //读取文本方式2-xml.Load(传入路径)

        //2.读取元素和属性
        //获取单个节点 : XmlNode node = xml.SelectSingleNode(节点名)
        //获取多个节点 : XmlNodeList nodeList = xml.SelectNodes(节点名)

        //获取节点元素内容：node.InnerText
        //获取节点元素属性：
        //1.item.Attributes["属性名"].Value
        //2.item.Attributes.GetNamedItem("属性名").Value

        //通过迭代器遍历或者循环遍历XmlNodeList对象 可以获取到各单个元素节点

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
