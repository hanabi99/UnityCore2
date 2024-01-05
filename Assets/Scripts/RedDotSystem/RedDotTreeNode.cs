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
    /// 节点逻辑委托（刷新数量时可调用）
    /// </summary>
    public Action<RedDotTreeNode> logicHander;

    /// <summary>
    ///控制显隐后执行的逻辑委托
    /// </summary>
    public Action<RedDotTreeNodeType, bool, int> OnRedDotActiveChange;


    public virtual bool RefreshRedDotState()
    {
        redDotCount = 0;
        switch (nodeType)
        {
            case RedDotTreeNodeType.Normal:
                break;
            case RedDotTreeNodeType.RedDotNodeNum:
                if (nodeType == RedDotTreeNodeType.RedDotNodeNum)
                {
                    //显示红点个数，去显示红点个数
                }
                else
                {
                    redDotCount = RefreshRedDotCount();
                }
                logicHander?.Invoke(this);
                break;
            case RedDotTreeNodeType.ReDotNodeDataNum:
                redDotActive = redDotCount > 0;
                break;
            default:
                break;
        }
        OnRedDotActiveChange?.Invoke(nodeType, redDotActive, redDotCount);

        return redDotActive;
    }

    public virtual int RefreshRedDotCount()
    {
        return 1;
    }


}
