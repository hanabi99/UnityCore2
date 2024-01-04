using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    /// <summary>
    /// ˳��ڵ� ��˳��ִ�������ӽڵ㣬ֱ��һ���ӽڵ�ʧ�� 
    /// </summary>
    public class BTSequenceNode : BTBaseNode
    {
        public BTSequenceNode() : base() { }
        public override E_NodeState Excute()
        {
            BTBaseNode childNode;
            if (childList.Count != 0)
            {
                childNode = childList[NowIndex];
                switch (childNode.Excute())
                {
                    case E_NodeState.Success:
                        ++NowIndex;
                        if (NowIndex == childList.Count)
                        {
                            NowIndex = 0;
                            return E_NodeState.Success;
                        }
                        break;
                    case E_NodeState.Failed:
                        NowIndex = 0;
                        return E_NodeState.Failed;
                    case E_NodeState.Running:
                        return E_NodeState.Running;
                    default:
                        break;
                }
            }
            return E_NodeState.Failed;
        }
    }
}
