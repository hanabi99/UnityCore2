using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyGameFrameWork {
    public class UIManager : Singleton<UIManager>
    {
        public List<BasePanel> basePanels = new List<BasePanel>();

        [HideInInspector]
        public GameObject uiRoot;

        protected void Awake()
        {
            AutoRegister();
            HideAllPanel();
            DontDestroyOnLoad(this);
        }

        /// <summary>
        /// 移除面板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="panelName"></param>
        public void RemovePanel<T>(string panelName) where T : BasePanel
        {
            basePanels.Remove(GetPanel<T>(panelName));
        }
        /// <summary>
        /// 移除所有面板
        /// </summary>
        public void RemoveAllPanel()
        {
            basePanels.Clear();
        }

        /// <summary>
        /// 展示制定面板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="panelName"></param>
        public void ShowPanel<T>(string panelName) where T : BasePanel
        {
            for (int i = 0; i < basePanels.Count; i++)
            {
                if (basePanels[i].gameObject.name == panelName && basePanels[i] is T)
                {
                    basePanels[i].ShowMe();
                }
            }
        }
        /// <summary>
        /// 隐藏指定面板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="panelName"></param>
        public void HidePanel<T>(string panelName) where T : BasePanel
        {
            for (int i = 0; i < basePanels.Count; i++)
            {
                if (basePanels[i].gameObject.name == panelName && basePanels[i] is T)
                {
                    basePanels[i].HideMe();
                }
            }
        }
        /// <summary>
        /// 隐藏所有面板
        /// </summary>
        public void HideAllPanel()
        {
            for (int i = 0; i < basePanels.Count; i++)
            {
                basePanels[i].HideMe();
            }
        }
        /// <summary>
        /// 获得一个面板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="panelName"></param>
        /// <returns></returns>
        public T GetPanel<T>(string panelName) where T : BasePanel
        {
            for (int i = 0; i < basePanels.Count; i++)
            {
                if (basePanels[i].gameObject.name.Equals(panelName))
                {
                    return basePanels[i] as T;
                }
            }

            return null;
        }
        /// <summary>
        /// 自动注册
        /// </summary>

        public void AutoRegister()
        {
            uiRoot = GameObject.Find("UIRoot");

            BasePanel[] children = uiRoot.GetComponentsInChildren<BasePanel>(true);

            if (children.Length > 0)
            {
                foreach (BasePanel child in children)
                {
                    basePanels.Add(child);
                }
            }

        }
        /// <summary>
        /// 给控件添加自定义事件监听
        /// </summary>
        /// <param name="control">控件对象</param>
        /// <param name="type">事件类型</param>
        /// <param name="callBack">事件的响应函数</param>
        public void AddCustomEventListener(UIBehaviour control, EventTriggerType type, UnityAction<BaseEventData> callBack)
        {
            EventTrigger trigger = control.GetComponent<EventTrigger>();
            if (trigger == null)
                trigger = control.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback.AddListener(callBack);

            trigger.triggers.Add(entry);
        }
    }
}
