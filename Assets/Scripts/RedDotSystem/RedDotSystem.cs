using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDotSystem : Singleton<RedDotSystem>
{
    /// <summary>
    /// ����ֵ�
    /// </summary>
    public Dictionary<RedDotDefine, RedDotTreeNode> redDotDic = new Dictionary<RedDotDefine, RedDotTreeNode>();

    /// <summary>
    /// ��ʼ�������
    /// </summary>
    public void InitRedDotTree(List<RedDotTreeNode> nodeList)
    {
        foreach (var item in nodeList)
        {
            redDotDic.Add(item.node, item);
        }
    }

    /// <summary>
    /// ���º��״̬
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
    /// ע����ı�״̬�¼�
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
            Debug.LogError(redKey.ToString() + ": key������");
        }
    }

    /// <summary>
    /// ע�����ı�״̬�¼�
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
            Debug.LogError(redKey.ToString() + ": key������");
        }
    }
}
