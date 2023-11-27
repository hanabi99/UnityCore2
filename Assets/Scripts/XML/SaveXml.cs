using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class SaveXml : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 决定存储在哪个文件夹下
        //注意：存储xml文件 在Unity中一定是使用各平台都可读可写可找到的路径
        // 1.Resources 可读 不可写 打包后找不到  ×
        // 2.Application.streamingAssetsPath 可读 PC端可写 找得到  ×
        // 3.Application.dataPath 打包后找不到  ×
        // 4.Application.persistentDataPath 可读可写找得到   √

        string path = Application.persistentDataPath + "/PlayerInfo2.xml";
        print(Application.persistentDataPath);
        #endregion

        #region 知识点二 存储xml文件
        //关键类 XmlDocument 用于创建节点 存储文件
        //关键类 XmlDeclaration 用于添加版本信息
        //关键类 XmlElement 节点类

        //存储有5步
        //1.创建文本对象
        XmlDocument xml = new XmlDocument();

        //2.添加固定版本信息
        //这一句代码 相当于就是创建<?xml version="1.0" encoding="UTF-8"?>这句内容
        XmlDeclaration xmlDec = xml.CreateXmlDeclaration("1.0", "UTF-8", "");
        //创建完成过后 要添加进入 文本对象中
        xml.AppendChild(xmlDec);

        //3.添加根节点
        XmlElement root = xml.CreateElement("Root");
        xml.AppendChild(root);

        //4.为根节点添加子节点
        //加了一个 name子节点
        XmlElement name = xml.CreateElement("name");
        name.InnerText = "唐老狮";
        root.AppendChild(name);


        XmlElement atk = xml.CreateElement("atk");
        atk.InnerText = "10";
        root.AppendChild(atk);

        XmlElement listInt = xml.CreateElement("listInt");
        for (int i = 1; i <= 3; i++)
        {
            XmlElement childNode = xml.CreateElement("int");
            childNode.InnerText = i.ToString();
            listInt.AppendChild(childNode);
        }
        root.AppendChild(listInt);

        XmlElement itemList = xml.CreateElement("itemList");
        for (int i = 1; i <= 3; i++)
        {
            XmlElement childNode = xml.CreateElement("Item");
            //添加属性
            childNode.SetAttribute("id", i.ToString());
            childNode.SetAttribute("num", (i * 10).ToString());
            itemList.AppendChild(childNode);
        }
        root.AppendChild(itemList);

        //5.保存
        xml.Save(path);
        #endregion

        #region 知识点三 修改xml文件
        //1.先判断是否存在文件
        if( File.Exists(path) )
        {
            //2.加载后 直接添加节点 移除节点即可
            XmlDocument newXml = new XmlDocument();
            newXml.Load(path);

            //修改就是在原有文件基础上 去移除 或者添加
            //移除
            XmlNode node;// = newXml.SelectSingleNode("Root").SelectSingleNode("atk");
            //这种是一种简便写法 通过/来区分父子关系
            node = newXml.SelectSingleNode("Root/atk");
            //得到自己的父节点
            XmlNode root2 = newXml.SelectSingleNode("Root");
            //移除子节点方法
            root2.RemoveChild(node);

            //添加节点
            XmlElement speed = newXml.CreateElement("moveSpeed");
            speed.InnerText = "20";
            root2.AppendChild(speed);

            //改了记得存
            newXml.Save(path);
        }

        #endregion

        #region 总结
        //1.路径选取
        //在运行过程中存储 只能往可写且能找到的文件夹存储
        //故 选择了Application.persistentDataPath

        //2.存储xml关键类
        //XmlDocument  文件  
        //   创建节点 CreateElement
        //   创建固定内容方法 CreateXmlDeclaration
        //   添加节点 AppendChild
        //   保存 Save
        //XmlDeclaration 版本
        //XmlElement 元素节点  
        //   设置属性方法SetAttribute

        //3.修改
        //RemoveChild移除节点
        //可以通过 /的形式 来表示 子节点的子节点 
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
