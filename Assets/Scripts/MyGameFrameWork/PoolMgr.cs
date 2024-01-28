using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyGameFrameWork
{
    public class PoolMgr : Singleton<PoolMgr>
    {
        public Dictionary<string, Pool> PoolDic = new Dictionary<string, Pool>();//������ӵ�����

        /// <summary>
        /// ������ط�ʽ�Ƕ�̬���أ�prefabnameҪ��ab��Դ�����߿�Ѱַ��Դ��һ��
        /// </summary>
        /// <param name="prefabname"></param>
        /// <param name="loadType"></param>
        /// <returns></returns>
        public GameObject GetObj(GameObject asset,Action<GameObject> action = null)
        {
            string poolname = asset.name + "(Clone)" + "Pool";
            if (PoolDic.ContainsKey(poolname))
            {
                return PoolDic[poolname].GetObj(action);
            }
            else
            {  
                Pool pool = new Pool(asset, 10, poolname);
                PoolDic.Add(poolname, pool);
                return PoolDic[poolname].GetObj(action);
            }
        }

        public void PushObj(GameObject asset, Action<GameObject> action = null)
        {
            string poolname = asset.name + "(Clone)" + "Pool";
            if (PoolDic.ContainsKey(poolname))
            {
                 PoolDic[poolname].PushObj(asset);
            }
            else
            {
                PoolDic[poolname].PushObj(GetObj(asset,action));
            }
        }
    }

    public class Pool
    {
        public GameObject parentObj; //����ĸ�����

        public GameObject prefabObj;//���ɵ�����

        public Queue<GameObject> queue;

        public int initCount;//��ʼ������

        public Pool(GameObject prefab, int Count, string parentObjName)
        {
            parentObj = new GameObject(parentObjName);
            parentObj.transform.parent = PoolMgr.GetInstance().gameObject.transform;
            initCount = Count;
            queue = new Queue<GameObject>();
            prefabObj = prefab;
            InitPool();
        }

        void InitPool(Action<GameObject> action = null)
        {
            for (int i = 0; i < initCount; i++)
            {
                if (prefabObj != null)
                {
                    var obj = GameObject.Instantiate(prefabObj);
                    action?.Invoke(obj);
                    PushObj(obj);
                }
            }
        }
        public void PushObj(GameObject gameObject)
        {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(parentObj.transform);
            queue.Enqueue(gameObject);
        }

        public GameObject GetObj(Action<GameObject> action = null)
        {
            if (queue.Count == 0)
            {
                InitPool(action);
            }
            var obj = queue.Dequeue();
            obj.SetActive(true);
            return obj;
        }

    }
}
