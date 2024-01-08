using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedDotItem : MonoBehaviour
{
    public RedDotDefine redKey;

    public Text redDotCount_txt;

    public GameObject redDotObj;
   
    void Start()
    {
        RedDotSystem.GetInstance().RegisterRedDotChangeEvent(redKey, RedDotStateChangeEvent);
        RedDotSystem.GetInstance().UpdateRedDotState(redKey);
    }

    private void OnEnable()
    {
        RedDotSystem.GetInstance().UpdateRedDotState(redKey);
    }
    /// <summary>
    /// 红点改变事件
    /// </summary>
    /// <param name="redkeyType"></param>
    /// <param name="Active"></param>
    /// <param name="redDotCount"></param>
    private void RedDotStateChangeEvent(RedDotTreeNodeType redkeyType, bool Active, int redDotCount)
    {
        redDotObj.SetActive(Active);
        if(redkeyType != RedDotTreeNodeType.Normal)
        {
            redDotCount_txt.text = redDotCount.ToString();
        }
        redDotCount_txt.gameObject.SetActive(redkeyType != RedDotTreeNodeType.Normal);
    }

    private void OnDestroy()
    {
        RedDotSystem.GetInstance().UnRegisterRedDotChangeEvent(redKey, RedDotStateChangeEvent);
    }
}
