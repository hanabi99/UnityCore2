using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPointNode
{
    /// <summary>
    /// �ڵ�����
    /// </summary>
    public RedDotTreeNodeType nodeType;
    /// <summary>
    /// �ڵ�
    /// </summary>
    public RedDotDefine nodeName;
    /// <summary>
    /// ���ڵ�
    /// </summary>
    public RedPointNode parentNode;

    /// <summary>
    /// �ڵ�ĸ��ڵ�
    /// </summary>
    public RedPointNode nodeRoot;

    /// <summary>
    /// �Ƿ�Active
    /// </summary>
    public bool redDotActive;

    /// <summary>
    /// �ڵ���ʾ�ĸ���
    /// </summary>
    public int redDotCount = 0;

    public Dictionary<RedDotDefine, RedPointNode> dicChilds = new Dictionary<RedDotDefine, RedPointNode>();

    public virtual bool RefreshRedDotState()
    {
        redDotCount = 0;

        if (nodeType == RedDotTreeNodeType.RedDotNodeNum)
        {
            //redDotCount = RedDotSystem.GetInstance().GetChildNodeRedDotCount(nodeName);
            redDotActive = redDotCount > 0;
        }
        else
        {
            redDotCount = RefreshRedDotCount();
        }
        //logicHander?.Invoke(this);

        if (nodeType == RedDotTreeNodeType.ReDotNodeDataNum)
        {
            redDotActive = redDotCount > 0;
        }

        //OnRedDotActiveChange?.Invoke(nodeType, redDotActive, redDotCount);

        return redDotActive;
    }


    public virtual int RefreshRedDotCount()
    {
        return 1;
    }

}
