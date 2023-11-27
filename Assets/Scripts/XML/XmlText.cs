using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XmlText : MonoBehaviour
{
    private void Start()
    {
        //Test1 test1 = new Test1();
        //test1.id = 1;
        //test1.list.Add("22");
        //test1.list.Add("33");
        //test1.list.Add("44");
        //test1.name = "thc";
        //test1.dic.Add(true, 0.1f);
        //test1.dic.Add(false, 0.2f);
        //XmlDataManager.Instance.SaveData(test1, "mrgtest");
        Test1 test1 = XmlDataManager.Instance.LoadData(typeof(Test1), "mrgtest") as Test1;
    }
}

public class Test1
{
    public int id;
    public List<string> list = new List<string>();
    public string name;
    public SerizlizerDictionary<bool, float> dic = new SerizlizerDictionary<bool, float>();
}
