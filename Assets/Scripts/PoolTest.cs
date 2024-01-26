using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameFrameWork;

public class PoolTest : MonoBehaviour
{
    private GameObject prefab1;
    private GameObject prefab2;
    [Header("TimeControl")]
    public float activeTime;//显示时间
    public float activeStart;//开始显示时间

    private void Start()
    {
        //ABManager.GetInstance().LoadMainfest();
        //ABManager.GetInstance().LoadResAsync("model", "Cube",(obj)=> {
        //    prefab1 = obj as GameObject; 
        //});
        //ABManager.GetInstance().LoadResAsync("model", "Sphere", (obj) => {
        //    prefab2 = obj as GameObject;
        //});
        AdressableMrg.GetInstance().LoadAssetAsync<GameObject>("Cube",(handle)=> {
            prefab1 = handle.Result;
        });
        AdressableMrg.GetInstance().LoadAssetAsync<GameObject>("Sphere", (handle) => {
            prefab2 = handle.Result;
        });
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //MyGameFrameWork.PoolMgr.GetInstance().GetObj(LoadType.Instantiate, prefab1, "Cube");
            MyGameFrameWork.PoolMgr.GetInstance().GetObj(prefab1,(obj) => { if (obj.GetComponent<PoolTestObj>() == null) { obj.AddComponent<PoolTestObj>(); } });
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //MyGameFrameWork.PoolMgr.GetInstance().GetObj(LoadType.Instantiate, prefab2, "Sphere");
            MyGameFrameWork.PoolMgr.GetInstance().GetObj(prefab2,(obj) => { if (obj.GetComponent<PoolTestObj>() == null) { obj.AddComponent<PoolTestObj>(); } });
        }
    }

    private void OnDestroy()
    {
        AdressableMrg.GetInstance().Release<GameObject>("Cube");
        AdressableMrg.GetInstance().Release<GameObject>("Sphere");
    }
}
