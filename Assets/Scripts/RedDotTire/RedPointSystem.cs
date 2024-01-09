using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameFrameWork;

public class RedPointSystem : Singleton<RedPointSystem>
{
    /// <summary>
    /// ��¼���и��ڵ���
    /// </summary>
    public Dictionary<RedDotDefine, RedPointNode> _redNodeRootDic = new Dictionary<RedDotDefine, RedPointNode>();

    /// <summary>
    /// ��ʼ�����ڵ�
    /// </summary>
    /// <param name="rootRedKey"></param>
    /// <param name="ChildList"></param>
    public void InitRedPointTree(RedPointNode rootRedKey)
    {
        //������ڵ��ֵ�
        if (!_redNodeRootDic.ContainsKey(rootRedKey.nodeName))
        {
            _redNodeRootDic.Add(rootRedKey.nodeName,rootRedKey); 
        }
    }
    /// <summary>
    /// ���º��״̬
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
    /// ����ڵ�
    /// </summary>
    /// <param name="redKey"></param>
    public void InsertNode(RedPointNode redKey)
    {
        redKey.parentNode.dicChilds.Add(redKey.nodeName, redKey);
    }

    /// <summary>
    /// ���ҽڵ� ������ڵ�
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
