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
        //���ֻ��ϲ��ᴥ������
#if UNITY_EDITOR
        mWindowConfig.GeneratorWindowConfig();
#endif
    }
    /// <summary>
    /// ����һ������
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
    /// ��ջϵͳ����
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
        //1.���ɶ�Ӧ�Ĵ���Ԥ����
        GameObject nWnd = TempLoadWindow(wndName);
        //2.��ʼ����Ӧ������
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
        Debug.LogError("û�м��ص���Ӧ�Ĵ��� �������֣�" + wndName);
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
    /// ��ȡ�Ѿ������ĵ���
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
        Debug.LogError("�ô���û�л�ȡ����" + type.Name);
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
            Debug.LogError(winName + " ���ڲ�����");
        return null;
    }

    public void HideWindow(string wndName)
    {
        WindowBase window = GetWindow(wndName);
        HideWindow(window);
    }
    /// <summary>
    /// ���ؽ���
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
            window.SetActive(false);//���ص�������
            SetWidnowMaskVisible();
            window.HideMe();
        }
        //�ڳ�ջ������£���һ����������ʱ���Զ���ջ�ֵ���һ������
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
            //�ڳ�ջ������£���һ����������ʱ���Զ���ջ�ֵ���һ������
            PopNextStackWindow(window);
        }
    }
    /// <summary>
    /// �����˴����� ��������ȫ������
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
    /// ���õ�����
    /// </summary>
    private void SetWidnowMaskVisible()
    {
        if (!UISetting.Instance.SINGMASK_SYSTEM)
        {
            return;
        }
        WindowBase MaxOrderwindow = null;//�����Ⱦ�㼶�Ĵ���
        int maxOrder = 0;//�����Ⱦ�㼶
        int maxIndex = 0;//��������±� ����ͬ���ڵ��µ�λ���±�
        //1.�ر����д��ڵ�Mask ����Ϊ���ɼ�
        //2.�����пɼ��������ҵ�һ���㼶���Ĵ��ڣ���Mask����Ϊ�ɼ�
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
                    //�ҵ������Ⱦ�㼶�Ĵ��ڣ��õ���
                    if (maxOrder < windowBase.canvas.sortingOrder)
                    {
                        MaxOrderwindow = windowBase;
                        maxOrder = windowBase.canvas.sortingOrder;
                    }
                    //����������ڵ���Ⱦ�㼶��ͬ�����ҵ�ͬ�ڵ������һ�����壬������ȾMask
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
    /// ʵ��������
    /// </summary>
    /// <param name="wndName"></param>
    /// <returns></returns>
    private GameObject TempLoadWindow(string wndName)
    {
        //�Լ���ʱҪ�ĳ�AB����Adreesable
        GameObject window = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(mWindowConfig.GetWindowPath(wndName)), mUIRoot);
        //window.transform.SetParent(mUIRoot);
        window.transform.localScale = Vector3.one;
        window.transform.localPosition = Vector3.zero;
        window.transform.rotation = Quaternion.identity;
        window.name = wndName;
        return window;
    }

    /// <summary>
    /// ��ջһ������
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
    /// ������ջ�е�һ������
    /// </summary>
    public void StartPopFirstStackWindow()
    {
        if (mStartPopStackWndStatus == true) return;//���뵯ջ�����˲�����ȥ��ʾWindow ����ֻ��ѹջ
        mStartPopStackWndStatus = true;//������ʾWindow
        PopStackWindow();
    }

    /// <summary>
    /// ������ջ�е���һ������
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
    /// ѹ�벢�ҵ�����ջ����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="popCallBack"></param>
    public void PushAndPopStackWindow<T>(Action<WindowBase> popCallBack = null) where T : WindowBase, new()
    {
        PushWindowToStack<T>(popCallBack);
        StartPopFirstStackWindow();
    }

    /// <summary>
    /// ������ջ����
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
