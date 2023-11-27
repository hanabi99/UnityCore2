using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
namespace xml
{
    public class Item
{
    public int id;
    public int num;
}

    public class PlayerInfo
    {
        public string name;
        public int atk;
        public int def;
        public float moveSpeed;
        public float roundSpeed;
        public Item weapon;
        public List<int> listInt;
        public List<Item> itemList;
        public Dictionary<int, Item> itemDic;

        public void LoadData(string fileName)
        {
            //目的是 如果可读可写中 从来没有存储过 是不存在这个文件的
            //那么读取时  就先从默认文件中获取内容
            string path = Application.persistentDataPath + "/" + fileName + ".xml";
            if (!File.Exists(path))
            {
                path = Application.streamingAssetsPath + "/" + fileName + ".xml";
            }
            //加载XML文件信息
            XmlDocument xml = new XmlDocument();
            //加载
            xml.Load(path);

            //从文件中加载出具体的数据
            //加载根节点 才能加载后面的内容
            XmlNode playerInfo = xml.SelectSingleNode("PlayerInfo");
            //通过根节点 去加载具体的信息
            this.name = playerInfo.SelectSingleNode("name").InnerText;
            this.atk = int.Parse(playerInfo.SelectSingleNode("atk").InnerText);
            this.def = int.Parse(playerInfo.SelectSingleNode("def").InnerText);
            this.moveSpeed = float.Parse(playerInfo.SelectSingleNode("moveSpeed").InnerText);
            this.roundSpeed = float.Parse(playerInfo.SelectSingleNode("roundSpeed").InnerText);

            XmlNode weaponNode = playerInfo.SelectSingleNode("weapon");
            this.weapon = new Item();
            this.weapon.id = int.Parse(weaponNode.SelectSingleNode("id").InnerText);
            this.weapon.num = int.Parse(weaponNode.SelectSingleNode("num").InnerText);

            XmlNode listIntNode = playerInfo.SelectSingleNode("listInt");
            XmlNodeList intList = listIntNode.SelectNodes("int");
            this.listInt = new List<int>();
            for (int i = 0; i < intList.Count; i++)
            {
                this.listInt.Add(int.Parse(intList[i].InnerText));
            }

            XmlNode itemList = playerInfo.SelectSingleNode("itemList");
            XmlNodeList items = itemList.SelectNodes("Item");
            this.itemList = new List<Item>();
            foreach (XmlNode item in items)
            {
                Item item2 = new Item();
                item2.id = int.Parse(item.Attributes["id"].Value);
                item2.num = int.Parse(item.Attributes["num"].Value);
                this.itemList.Add(item2);
            }

            XmlNode itemDic = playerInfo.SelectSingleNode("itemDic");
            XmlNodeList keyInt = itemDic.SelectNodes("int");
            XmlNodeList valueItem = itemDic.SelectNodes("Item");

            this.itemDic = new Dictionary<int, Item>();
            for (int i = 0; i < keyInt.Count; i++)
            {
                int key = int.Parse(keyInt[i].InnerText);
                Item value = new Item();
                value.id = int.Parse(valueItem[i].Attributes["id"].Value);
                value.num = int.Parse(valueItem[i].Attributes["num"].Value);
                this.itemDic.Add(key, value);
            }
        }

        public void SaveData(string fileName)
        {
            //决定存储路径
            string path = Application.persistentDataPath + "/" + fileName + ".xml";

            Debug.Log(Application.persistentDataPath);
            //5个步骤

            //1.创建我们的Xml文本 
            XmlDocument xml = new XmlDocument();

            //2.添加固定内容
            XmlDeclaration xmlDec = xml.CreateXmlDeclaration("1.0", "UTF-8", "");
            xml.AppendChild(xmlDec);

            //3.添加根节点
            XmlElement playerInfo = xml.CreateElement("PlayerInfo");
            xml.AppendChild(playerInfo);

            //4.添加子节点 或者 属性相关
            XmlElement name = xml.CreateElement("name");
            name.InnerText = this.name;
            playerInfo.AppendChild(name);

            XmlElement atk = xml.CreateElement("atk");
            atk.InnerText = this.atk.ToString();
            playerInfo.AppendChild(atk);

            XmlElement def = xml.CreateElement("def");
            def.InnerText = this.def.ToString();
            playerInfo.AppendChild(def);

            XmlElement moveSpeed = xml.CreateElement("moveSpeed");
            moveSpeed.InnerText = this.moveSpeed.ToString();
            playerInfo.AppendChild(moveSpeed);

            XmlElement roundSpeed = xml.CreateElement("roundSpeed");
            roundSpeed.InnerText = this.roundSpeed.ToString();
            playerInfo.AppendChild(roundSpeed);

            //weapon
            XmlElement weapon = xml.CreateElement("weapon");

            XmlElement id = xml.CreateElement("id");
            id.InnerText = this.weapon.id.ToString();
            weapon.AppendChild(id);
            XmlElement num = xml.CreateElement("num");
            num.InnerText = this.weapon.num.ToString();
            weapon.AppendChild(num);

            playerInfo.AppendChild(weapon);

            //listInt
            XmlElement listInt = xml.CreateElement("listInt");
            for (int i = 0; i < this.listInt.Count; i++)
            {
                XmlElement intNode = xml.CreateElement("int");
                intNode.InnerText = this.listInt[i].ToString();
                listInt.AppendChild(intNode);
            }
            playerInfo.AppendChild(listInt);

            //itemList
            XmlElement itemList = xml.CreateElement("itemList");

            for (int i = 0; i < this.itemList.Count; i++)
            {
                XmlElement itemNode = xml.CreateElement("Item");
                itemNode.SetAttribute("id", this.itemList[i].id.ToString());
                itemNode.SetAttribute("num", this.itemList[i].num.ToString());
                itemList.AppendChild(itemNode);
            }

            playerInfo.AppendChild(itemList);

            //itemDic
            XmlElement itemDic = xml.CreateElement("itemDic");

            foreach (int key in this.itemDic.Keys)
            {
                //key
                XmlElement intNode = xml.CreateElement("int");
                intNode.InnerText = key.ToString();
                itemDic.AppendChild(intNode);
                //value
                XmlElement itemNode = xml.CreateElement("Item");
                itemNode.SetAttribute("id", this.itemDic[key].id.ToString());
                itemNode.SetAttribute("num", this.itemDic[key].num.ToString());
                itemDic.AppendChild(itemNode);
            }

            playerInfo.AppendChild(itemDic);

            //5.存储
            xml.Save(path);
        }
    }
}
