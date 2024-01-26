using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolTestObj : MonoBehaviour
{
    [Header("TimeControl")]
    private float activeTime = 2;//显示时间
    private float activeStart;//开始显示时间
    private void OnEnable()
    {
        activeStart = Time.time;
    }


    void Update()
    {
        if (Time.time >= activeStart + activeTime)
        {
            //返回对象池
           MyGameFrameWork.PoolMgr.GetInstance().PushObj(this.gameObject);
        }
    }
}
