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
    /// �ڵ�����
    /// </summary>
    public RedDotTreeNodeType nodeType;
    /// <summary>
    /// ���ڵ�
    /// </summary>
    public RedDotDefine parentNode;

    /// <summary>
    /// �ڵ�
    /// </summary>
    public RedDotDefine node;
    /// <summary>
    /// �Ƿ�Active
    /// </summary>
    public bool redDotActive;
     
    /// <summary>
    /// �ڵ���ʾ�ĸ���
    /// </summary>
    public int redDotCount;

    /// <summary>
    /// �ڵ��߼�ί�У�ˢ������ʱ�ɵ��ã�
    /// </summary>
    public Action<RedDotTreeNode> logicHander;

    /// <summary>
    ///����������ִ�е��߼�ί��
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
                    //��ʾ��������ȥ��ʾ������
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
