using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMain : MonoBehaviour
{  
    void Start()
    {
        UIMrg.GetInstance().Initialize();
        UIMrg.GetInstance().PopUpWindow<LoginWIndow>();
        UIMrg.GetInstance().GetWindow<LoginWIndow>().Test();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UIMrg.GetInstance().HideWindow<LoginWIndow>();
            //UIMrg.GetInstance().DestroyWinodw<LoginWIndow>();
            //UIMrg.GetInstance().DestroyAllWindow();
           //Debug.Log (UISetting.Instance.SINGMASK_SYSTEM);

        }
    }
}
