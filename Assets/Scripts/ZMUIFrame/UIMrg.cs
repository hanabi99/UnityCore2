using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameFrameWork;
using UnityEditor.PackageManager.UI;
using System;

public class UIMrg : Singleton<UIMrg>
{
    private Camera mUICamera;

    private Transform mUIRoot;

    private WindowConfig mWindowConfig;

    private Dictionary<string, WindowBase> mAllWindowDic = new Dictionary<string, WindowBase>();

    private List<WindowBase> mAllWindowList = new List<WindowBase>();

    private List<WindowBase> mVisibleWindowList = new List<WindowBase>();

    private Queue<WindowBase> mWindowStack = new Queue<WindowBase>();

    private bool mStartPopStackWndStatus = false;

    public void Initialize()
    {
        mUICamera = Camera.main;
        mUIRoot = GameObject.Find("UIRoot").transform;
        mWindowConfig = Resources.Load<WindowConfig>("WindowConfig");
        //在手机上不会触发调用
#if UNITY_EDITOR
        mWindowConfig.GeneratorWindowConfig();
#endif
    }
    /// <summary>
    /// 弹出一个窗口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T PopUpWindow<T>() where T : WindowBase, new()
    {
        System.Type type = typeof(T);
        string wndName = type.Name;
        WindowBase wnd = GetWindow(wndName);
        if (wnd != null)
        {
            return ShowWindow(wndName) as T;
        }

        T t = new T();
        return InitializeWindow(t, wndName) as T;
    }

    /// <summary>
    /// 堆栈系统弹出
    /// </summary>
    /// <param name="window"></param>
    /// <returns></returns>
    private WindowBase PopUpWindow(WindowBase window)
    {
        System.Type type = window.GetType();
        string wndName = type.Name;
        WindowBase wnd = GetWindow(wndName);
        if (wnd != null)
        {
            return ShowWindow(wndName);
        }
        return InitializeWindow(window, wndName);
    }
    private WindowBase InitializeWindow(WindowBase windowBase, string wndName)
    {
        //1.生成对应的窗口预制体
        GameObject nWnd = TempLoadWindow(wndName);
        //2.初始出对应管理类
        if (nWnd != null)
        {
            windowBase.gameObject = nWnd;
            windowBase.transform = nWnd.transform;
            windowBase.canvas = nWnd.GetComponent<Canvas>();
            windowBase.canvas.worldCamera = mUICamera;
            windowBase.transform.SetAsLastSibling();
            windowBase.name = nWnd.name;
            windowBase.Init();
            windowBase.SetActive(true);
            windowBase.ShowMe();
            RectTransform rectTrans = nWnd.GetComponent<RectTransform>();
            rectTrans.anchorMax = Vector2.one;
            rectTrans.offsetMax = Vector2.zero;
            rectTrans.offsetMin = Vector2.zero;
            mAllWindowDic.Add(wndName, windowBase);
            mAllWindowList.Add(windowBase);
            mVisibleWindowList.Add(windowBase);
            SetWidnowMaskVisible();
            return windowBase;
        }
        Debug.LogError("没有加载到对应的窗口 窗口名字：" + wndName);
        return null;
    }
    private WindowBase GetWindow(string winName)
    {
        if (mAllWindowDic.ContainsKey(winName))
        {
            return mAllWindowDic[winName];
        }
        return null;
    }
    /// <summary>
    /// 获取已经弹出的弹窗
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetWindow<T>() where T : WindowBase
    {
        System.Type type = typeof(T);
        foreach (var item in mVisibleWindowList)
        {
            if (item.name == type.Name)
            {
                return (T)item;
            }
        }
        Debug.LogError("该窗口没有获取到：" + type.Name);
        return null;
    }

    private WindowBase ShowWindow(string winName)
    {
        WindowBase window = null;
        if (mAllWindowDic.ContainsKey(winName))
        {
            window = mAllWindowDic[winName];
            if (window.gameObject != null && window.isActive == false)
            {
                mVisibleWindowList.Add(window);
                window.transform.SetAsLastSibling();
                window.SetActive(true);
                SetWidnowMaskVisible();
                window.ShowMe();
            }
            return window;
        }
        else
            Debug.LogError(winName + " 窗口不存在");
        return null;
    }

    public void HideWindow(string wndName)
    {
        WindowBase window = GetWindow(wndName);
        HideWindow(window);
    }
    /// <summary>
    /// 隐藏界面
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void HideWindow<T>() where T : WindowBase
    {
        HideWindow(typeof(T).Name);
    }
    private void HideWindow(WindowBase window)
    {
        if (window != null && window.isActive)
        {
            mVisibleWindowList.Remove(window);
            window.SetActive(false);//隐藏弹窗物体
            SetWidnowMaskVisible();
            window.HideMe();
        }
        //在出栈的情况下，上一个界面隐藏时，自动打开栈种的下一个界面
        PopNextStackWindow(window);
    }
    private void DestroyWindow(string wndName)
    {
        WindowBase window = GetWindow(wndName);
        DestoryWindow(window);
    }
    public void DestroyWinodw<T>() where T : WindowBase
    {
        DestroyWindow(typeof(T).Name);
    }
    private void DestoryWindow(WindowBase window)
    {
        if (window != null)
        {
            if (mAllWindowDic.ContainsKey(window.name))
            {
                mAllWindowDic.Remove(window.name);
                mAllWindowList.Remove(window);
                mVisibleWindowList.Remove(window);
            }
            window.SetActive(false);
            SetWidnowMaskVisible();
            window.HideMe();
            window.OnDestroy();
            GameObject.Destroy(window.gameObject);
            //在出栈的情况下，上一个界面销毁时，自动打开栈种的下一个界面
            PopNextStackWindow(window);
        }
    }
    /// <summary>
    /// 处过滤窗口外 其他窗口全部销毁
    /// </summary>
    /// <param name="filterlist"></param>
    public void DestroyAllWindow(List<string> filterlist = null)
    {
        for (int i = mAllWindowList.Count - 1; i >= 0; i--)
        {
            WindowBase window = mAllWindowList[i];
            if (window == null || (filterlist != null && filterlist.Contains(window.name)))
            {
                continue;
            }
            DestroyWindow(window.name);
        }
        Resources.UnloadUnusedAssets();
    }
    /// <summary>
    /// 设置单遮罩
    /// </summary>
    private void SetWidnowMaskVisible()
    {
        if (!UISetting.Instance.SINGMASK_SYSTEM)
        {
            return;
        }
        WindowBase MaxOrderwindow = null;//最大渲染层级的窗口
        int maxOrder = 0;//最大渲染层级
        int maxIndex = 0;//最大排序下标 在相同父节点下的位置下标
        //1.关闭所有窗口的Mask 设置为不可见
        //2.从所有可见窗口中找到一个层级最大的窗口，把Mask设置为可见
        for (int i = 0; i < mVisibleWindowList.Count; i++)
        {
            WindowBase windowBase = mVisibleWindowList[i];
            if(windowBase != null && windowBase.gameObject != null)
            {
                windowBase.SetMaskVisible(false);
                if (MaxOrderwindow == null)
                {
                    MaxOrderwindow = windowBase;
                    maxOrder = windowBase.canvas.sortingOrder;
                    maxIndex = windowBase.transform.GetSiblingIndex();
                }
                else
                {
                    //找到最大渲染层级的窗口，拿到它
                    if (maxOrder < windowBase.canvas.sortingOrder)
                    {
                        MaxOrderwindow = windowBase;
                        maxOrder = windowBase.canvas.sortingOrder;
                    }
                    //如果两个窗口的渲染层级相同，就找到同节点下最靠下一个物体，优先渲染Mask
                    else if (maxOrder == windowBase.canvas.sortingOrder && maxIndex < windowBase.transform.GetSiblingIndex())
                    {
                        MaxOrderwindow = windowBase;
                        maxIndex = windowBase.transform.GetSiblingIndex();
                    }
                }
            }
        }
        if (MaxOrderwindow != null)
        {
            MaxOrderwindow.SetMaskVisible(true);
        }
    }

    /// <summary>
    /// 实例化界面
    /// </summary>
    /// <param name="wndName"></param>
    /// <returns></returns>
    private GameObject TempLoadWindow(string wndName)
    {
        //自己搞时要改成AB或者Adreesable
        GameObject window = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(mWindowConfig.GetWindowPath(wndName)), mUIRoot);
        //window.transform.SetParent(mUIRoot);
        window.transform.localScale = Vector3.one;
        window.transform.localPosition = Vector3.zero;
        window.transform.rotation = Quaternion.identity;
        window.name = wndName;
        return window;
    }

    /// <summary>
    /// 进栈一个界面
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="popCallBack"></param>
    public void PushWindowToStack<T>(Action<WindowBase> popCallBack = null) where T : WindowBase, new()
    {
        WindowBase wndBase = null;
        try
        {
            wndBase = mAllWindowDic[typeof(T).Name];
        }
        catch (Exception e)
        {
            wndBase = new T();
        }
        //T wndBase = new T();
        wndBase.PopStackListener = popCallBack;
        mWindowStack.Enqueue(wndBase);
    }
    /// <summary>
    /// 弹出堆栈中第一个弹窗
    /// </summary>
    public void StartPopFirstStackWindow()
    {
        if (mStartPopStackWndStatus == true) return;//进入弹栈流程了不会在去显示Window 而是只是压栈
        mStartPopStackWndStatus = true;//可以显示Window
        PopStackWindow();
    }

    /// <summary>
    /// 弹出堆栈中的下一个窗口
    /// </summary>
    /// <param name="windowBase"></param>
    private void PopNextStackWindow(WindowBase windowBase)
    {
        if (windowBase != null && mStartPopStackWndStatus && windowBase.PopStack)
        {
            windowBase.PopStack = false;
            PopStackWindow();
        }
    }
    /// <summary>
    /// 压入并且弹出堆栈弹窗
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="popCallBack"></param>
    public void PushAndPopStackWindow<T>(Action<WindowBase> popCallBack = null) where T : WindowBase, new()
    {
        PushWindowToStack<T>(popCallBack);
        StartPopFirstStackWindow();
    }

    /// <summary>
    /// 弹出堆栈弹窗
    /// </summary>
    /// <returns></returns>
    public bool PopStackWindow()
    {
        if (mWindowStack.Count > 0)
        {
            WindowBase window = mWindowStack.Dequeue();
            WindowBase popWindow = PopUpWindow(window);
            popWindow.PopStackListener = window.PopStackListener;
            popWindow.PopStack = true;
            popWindow.PopStackListener?.Invoke(popWindow);
            popWindow.PopStackListener = null;
            return true;
        }
        else
        {
            mStartPopStackWndStatus = false;
            return false;
        }
    }
    public void ClearStackWindows()
    {
        mWindowStack.Clear();
    }
}
