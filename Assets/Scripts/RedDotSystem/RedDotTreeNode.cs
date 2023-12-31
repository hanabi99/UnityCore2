using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RedDotTreeNodeType
{
    Normal,
    RedDotNodeNum,
    ReDotNodeDataNum

}
public class RedDotTreeNode 
{
    /// <summary>
    /// 节点类型
    /// </summary>
    public RedDotTreeNodeType nodeType;
    /// <summary>
    /// 父节点
    /// </summary>
    public RedDotDefine parentNode;

    /// <summary>
    /// 节点
    /// </summary>
    public RedDotDefine node;
    /// <summary>
    /// 是否Active
    /// </summary>
    public bool redDotActive;
     
    /// <summary>
    /// 节点显示的个数
    /// </summary>
    public int redDotCount;

    /// <summary>
    /// 节点逻辑委托（刷新数量显隐时可调用）
    /// </summary>
    public Action<RedDotTreeNode> logicHander;

    /// <summary>
    ///控制显隐后执行的逻辑委托(更新UI)
    /// </summary>
    public Action<RedDotTreeNodeType, bool, int> OnRedDotActiveChange;


    public virtual bool RefreshRedDotState()
    {
        redDotCount = 0;

        if (nodeType == RedDotTreeNodeType.RedDotNodeNum)
        {
            redDotCount = RedDotSystem.GetInstance().GetChildNodeRedDotCount(node);
            redDotActive = redDotCount > 0;
        }
        else
        {
            redDotCount = RefreshRedDotCount();
        }
        logicHander?.Invoke(this);

        if(nodeType == RedDotTreeNodeType.ReDotNodeDataNum)
        {
            redDotActive = redDotCount > 0;
        }

        OnRedDotActiveChange?.Invoke(nodeType, redDotActive, redDotCount);

        return redDotActive;
    }

    public virtual int RefreshRedDotCount()
    {
        return 1;
    }


}
