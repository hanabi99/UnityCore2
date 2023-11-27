using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private void Start()
    {
        //BagMgr.GetInstance().Init();
        UIManager.GetInstance().ShowPanel<BagPanel>("BagPanel");
    }
}
