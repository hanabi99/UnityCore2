using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BT
{
    /// <summary>
    /// �����ڵ� ����ȥ����һ�������ж�,���붯���ڵ��������� �����ڵ����û�з���ֵ
    /// </summary>
    public class BTConditionNode : BTBaseNode
    {

        public Func<bool> action;

        public BTConditionNode(Func<bool> action)
        {
            this.action = action;
        }

        public override E_NodeState Excute()
        {
            if (action == null)
            {
                return E_NodeState.Failed;
            }

            return action.Invoke() ? E_NodeState.Success : E_NodeState.Failed;
        }

    }
}
