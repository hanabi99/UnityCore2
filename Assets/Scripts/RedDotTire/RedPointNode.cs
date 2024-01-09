using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPointNode
{
    /// <summary>
    /// 节点类型
    /// </summary>
    public RedDotTreeNodeType nodeType;
    /// <summary>
    /// 节点
    /// </summary>
    public RedDotDefine nodeName;
    /// <summary>
    /// 父节点
    /// </summary>
    public RedPointNode parentNode;

    /// <summary>
    /// 节点的根节点
    /// </summary>
    public RedPointNode nodeRoot;

    /// <summary>
    /// 是否Active
    /// </summary>
    public bool redDotActive;

    /// <summary>
    /// 节点显示的个数
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
