using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameFrameWork
{
    public abstract class CommandBase : MonoBehaviour
    {
        /// <summary>
        /// ִ������
        /// </summary>
        public abstract void Excute();


        /// <summary>
        /// ��������
        /// </summary>
        public abstract void Undo();
    }
}
