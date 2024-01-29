using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMain : MonoBehaviour
{  
    void Start()
    {
        UIMrg.GetInstance().Initialize();
        UIMrg.GetInstance().PopUpWindow<LoginWIndow>();
    }
    
}
