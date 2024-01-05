using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDotSystem : Singleton<RedDotSystem>
{
    /// <summary>
    /// 红点字典
    /// </summary>
    public Dictionary<RedDotDefine, RedDotTreeNode> redDotDic = new Dictionary<RedDotDefine, RedDotTreeNode>();

    /// <summary>
    /// 初始化红点数
    /// </summary>
    public void InitRedDotTree(List<RedDotTreeNode> nodeList)
    {
        foreach (var item in nodeList)
        {
            redDotDic.Add(item.node, item);
        }
    }

    /// <summary>
    /// 更新红点状态
    /// </summary>
    public void UpdateRedDotState(RedDotDefine redKey)
    {
        RedDotTreeNode redDotTreeNode = null;
        if (redDotDic.TryGetValue(redKey, out redDotTreeNode))
        {
            redDotTreeNode.RefreshRedDotState();
            UpdateRedDotState(redDotTreeNode.parentNode);
        }
    }

    /// <summary>
    /// 注册红点改变状态事件
    /// </summary>
    public void RegisterRedDotChangeEvent(RedDotDefine redKey, Action<RedDotTreeNodeType, bool,int> changeEvent)
    {
        RedDotTreeNode redDotTreeNode = null;
        if(redDotDic.TryGetValue(redKey,out redDotTreeNode))
        {
            redDotTreeNode.OnRedDotActiveChange += changeEvent;
        }
        else
        {
            Debug.LogError(redKey.ToString() + ": key不存在");
        }
    }

    /// <summary>
    /// 注销红点改变状态事件
    /// </summary>
    public void UnRegisterRedDotChangeEvent(RedDotDefine redKey, Action<RedDotTreeNodeType, bool, int> changeEvent)
    {
        RedDotTreeNode redDotTreeNode = null;
        if (redDotDic.TryGetValue(redKey, out redDotTreeNode))
        {
            redDotTreeNode.OnRedDotActiveChange -= changeEvent;
        }
        else
        {
            Debug.LogError(redKey.ToString() + ": key不存在");
        }
    }
}
