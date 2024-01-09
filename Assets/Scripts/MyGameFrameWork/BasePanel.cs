using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyGameFrameWork {
    public abstract class BasePanel : MonoBehaviour
    {
        //父节点
        [HideInInspector]
        public BasePanel myParentPanel;
        //是否初始化
        public bool IsInitialized = false;
        public abstract void Init();

        public void RigisterPanel(BasePanel basePanel)
        {
            if (!UIManager.GetInstance().basePanels.Contains(basePanel))
            {
                UIManager.GetInstance().basePanels.Add(basePanel);
            }
        }
        public void RemovePanel(BasePanel basePanel)
        {
            if (UIManager.GetInstance().basePanels.Contains(basePanel))
            {
                UIManager.GetInstance().basePanels.Remove(basePanel);
            }
        }
        /// <summary>
        /// 显示自己
        /// </summary>
        public virtual void ShowMe()
        {
            this.gameObject.SetActive(true);
            this.Init();
        }
        /// <summary>
        /// 记录自己的父节点时调用
        /// </summary>
        /// <param name="basePanel"></param>
        public virtual void ShowMe(BasePanel basePanel)
        {
            this.gameObject.SetActive(true);
            this.myParentPanel = basePanel;
            this.Init();
        }

        /// <summary>
        /// 隐藏自己
        /// </summary>
        public virtual void HideMe()
        {
            this.gameObject.SetActive(false);
        }
        /// <summary>
        /// 适用于多按钮
        /// </summary>
        /// <param name="btnName"></param>
        protected virtual void OnClick(string btnName)
        {

        }
        /// <summary>
        /// 适用于多Toggel
        /// </summary>
        /// <param name="btnName"></param>
        protected virtual void OnValueChanged(bool value, string toggleName)
        {

        }
        /// <summary>
        /// 适用于多Toggel
        /// </summary>
        /// <param name="btnName"></param>
        protected virtual void OnValueChanged(bool value, int index)
        {

        }
        /// <summary>
        /// 适用于多Slider
        /// </summary>
        /// <param name="btnName"></param>
        protected virtual void OnValueChanged(float floatvalue, int index)
        {

        }

        /// <summary>
        /// 得到对应子物体名字的对应控件脚本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controlName"></param>
        /// <returns></returns>
        protected T GetControl<T>(string controlName) where T : Component
        {
            Transform trans = this.gameObject.transform.FindMyChild(controlName);
            Component component = trans.GetComponent<T>();
            if (component != null)
            {
                return component as T;
            }
            return null;
        }
        /// <summary>
        /// 设置界面数据用
        /// </summary>
        public virtual void SetData()
        {

        }
        /// <summary>
        /// 设置界面数据用
        /// </summary>
        public virtual void SetData(int index)
        {

        }

    }

}
