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
        if(redKey == RedDotDefine.None)
        {
            return;
        }
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
    /// <summary>
    /// ��ȡ�ڵ������
    /// </summary>
    /// <param name="redKey"></param>
    /// <param name="childRedDotCount"></param>
    public int  GetChildNodeRedDotCount(RedDotDefine redKey)
    {
        int childRedDotCount = 0;
        ComputeChildRedDotCount(redKey, ref childRedDotCount);
        return childRedDotCount;

    }
    /// <summary>
    /// ����ڵ������(�ݹ麯��)
    /// </summary>
    /// <param name="redKey"></param>
    /// <param name="childRedDotCount"></param>
    private void ComputeChildRedDotCount(RedDotDefine redKey,ref int  childRedDotCount)
    {
        foreach (RedDotTreeNode item in redDotDic.Values)
        {
            if(item.parentNode == redKey)
            {
                item.RefreshRedDotState();
                if (item.redDotActive)
                {
                    childRedDotCount += item.redDotCount;
                    if (item.nodeType != RedDotTreeNodeType.ReDotNodeDataNum)
                    {
                        ComputeChildRedDotCount(item.node,ref childRedDotCount);
                    }
                }
            }
        }
    }
}
