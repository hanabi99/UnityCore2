using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class XmlDataManager
{
    private static XmlDataManager instance = new XmlDataManager();

    public static XmlDataManager Instance => instance;

    public void SaveData(object data ,string filename)
    {
        string path = Application.persistentDataPath + "/" + filename + ".xml";
        Debug.Log(path);
        using (StreamWriter streamWriter = new StreamWriter(path))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(data.GetType());
            xmlSerializer.Serialize(streamWriter, data);
        }
    }

    public object LoadData(Type type ,string filename)
    {
        string path = Application.persistentDataPath + "/" + filename + ".xml";
        if (!File.Exists(path))
        {
            path = Application.streamingAssetsPath + "/" + filename + ".xml";
            if (!File.Exists(path))
            {
                return Activator.CreateInstance(type);
            }
        }
        using (StreamReader reader = new StreamReader(path))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(type);
            return xmlSerializer.Deserialize(reader);
        }
    }
    
}
