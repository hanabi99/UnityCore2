using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{

    /// <summary>
    /// ѡ��ڵ� ѡ������һ���ӽڵ�ִ�У�ͨ���ǵ�һ���ɹ����ӽڵ����û���ӽڵ�ɹ�
    /// </summary>
    public class BTSelectNode : BTBaseNode
    {
        public BTSelectNode() : base() { }

        public override E_NodeState Excute()
        {
            BTBaseNode childNode;
            if (childList.Count != 0)
            {
                childNode = childList[NowIndex];
                switch (childNode.Excute())
                {
                    case E_NodeState.Success:
                        NowIndex = 0;
                        return E_NodeState.Success;
                    case E_NodeState.Failed:
                        ++NowIndex;
                        if (NowIndex == childList.Count)
                        {
                            NowIndex = 0;
                            return E_NodeState.Failed;
                        }
                        break;
                    case E_NodeState.Running:
                        return E_NodeState.Running;
                }
            }
            return E_NodeState.Failed;
        }
    }
}

