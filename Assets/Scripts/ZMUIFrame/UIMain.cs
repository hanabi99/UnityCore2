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
        UIMrg.GetInstance().PreLoadWindow<HallWindow>();
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UIMrg.GetInstance().PushAndPopStackWindow<ChatWIndow>();
            Debug.Log("压栈并弹出ChatWIndow");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //UIMrg.GetInstance().PopUpWindow<SignupWIndow>();
            UIMrg.GetInstance().PushAndPopStackWindow<UserInfoWIndow>();
            Debug.Log("压栈并弹出UserInfoWIndow");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
           // UIMrg.GetInstance().HideWindow<AccountWIndow>();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
           // UIMrg.GetInstance().HideWindow<SignupWIndow>();
        }
    }
}
