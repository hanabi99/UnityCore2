using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolTestObj : MonoBehaviour
{
    [Header("TimeControl")]
    private float activeTime = 2;//��ʾʱ��
    private float activeStart;//��ʼ��ʾʱ��
    private void OnEnable()
    {
        activeStart = Time.time;
    }


    void Update()
    {
        if (Time.time >= activeStart + activeTime)
        {
            //���ض����
           MyGameFrameWork.PoolMgr.GetInstance().PushObj(this.gameObject);
        }
    }
}
