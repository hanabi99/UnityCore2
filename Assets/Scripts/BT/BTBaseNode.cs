using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BT
{
    /// <summary>
    /// ��Ϊ�ڵ����
    /// </summary>
    public abstract class BTBaseNode
    {
        protected List<BTBaseNode> childList = new List<BTBaseNode>();

        public BTBaseNode parent;

        //��ǰִ���߼����ӽڵ����
        protected int NowIndex = 0;


        /// <summary>
        /// ����ӽڵ㷽��
        /// </summary>
        /// <param name="nodes"></param>
        public virtual void AddChild(params BTBaseNode[] nodes)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].parent = this;
                childList.Add(nodes[i]);
            }
        }

        public virtual void RemoveChild(int index)
        {

        }

        /// <summary>
        /// ִ�нڵ��߼� (Evaluate)
        /// </summary>
        /// <returns></returns>
        public abstract E_NodeState Excute();

     
    }
}
    
