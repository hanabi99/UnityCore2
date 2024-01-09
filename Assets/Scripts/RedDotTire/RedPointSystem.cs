using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameFrameWork;

public class RedPointSystem : Singleton<RedPointSystem>
{
    /// <summary>
    /// 记录所有根节点树
    /// </summary>
    public Dictionary<RedDotDefine, RedPointNode> _redNodeRootDic = new Dictionary<RedDotDefine, RedPointNode>();

    /// <summary>
    /// 初始化根节点
    /// </summary>
    /// <param name="rootRedKey"></param>
    /// <param name="ChildList"></param>
    public void InitRedPointTree(RedPointNode rootRedKey)
    {
        //加入根节点字典
        if (!_redNodeRootDic.ContainsKey(rootRedKey.nodeName))
        {
            _redNodeRootDic.Add(rootRedKey.nodeName,rootRedKey); 
        }
    }
    /// <summary>
    /// 更新红点状态
    /// </summary>
    public void UpdateRedDotState(RedPointNode redKey)
    {
        if (redKey.nodeName == RedDotDefine.None)
        {
            return;
        }
        RedPointNode redDotTreeNode = null;
        if (_redNodeRootDic[redKey.nodeName].dicChilds.TryGetValue(redKey.nodeName, out redDotTreeNode))
        {
            redDotTreeNode.RefreshRedDotState();
            UpdateRedDotState(redDotTreeNode.parentNode);
        }
    }
    /// <summary>
    /// 插入节点
    /// </summary>
    /// <param name="redKey"></param>
    public void InsertNode(RedPointNode redKey)
    {
        redKey.parentNode.dicChilds.Add(redKey.nodeName, redKey);
    }

    /// <summary>
    /// 查找节点 传入根节点
    /// </summary>
    /// <param name="redKey"></param>
    /// <param name="cur"></param>
    /// <returns></returns>
    public RedPointNode SerchNode(RedPointNode redKey, RedPointNode cur)
    {
        if (cur.dicChilds.ContainsKey(redKey.nodeName))
        {
            return cur.dicChilds[redKey.nodeName];
        }
        else
        {
            foreach (RedPointNode item in cur.dicChilds.Values)
            {

                RedPointNode res = SerchNode(redKey, item);
                if(res != null)
                {
                    return res;
                }
            }
        }
        return null;
    }
}
