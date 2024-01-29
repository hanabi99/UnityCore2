using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameFrameWork;

public class UIMrg : Singleton<UIMrg>
{
    private Camera mUICamera;

    private Transform mUIRoot;

    //private WindowConfig mWindowConfig;

    private Dictionary<string, WindowBase> mAllWindowDic = new Dictionary<string, WindowBase>();

    private List<WindowBase> mAllWindowList = new List<WindowBase>();

    private List<WindowBase> mVisibleWindowList = new List<WindowBase>();

    private Queue<WindowBase> mWindowStack = new Queue<WindowBase>();

    private bool mStartPopStackWndStatus = false;

    public void Initialize()
    {
        mUICamera = Camera.main;
        mUIRoot = GameObject.Find("UIRoot").transform;
        //mWindowConfig = Resources.Load<WindowConfig>("WindowConfig");
        //在手机上不会触发调用
#if UNITY_EDITOR
        //mWindowConfig.GeneratorWindowConfig();
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
            //SetWidnowMaskVisible();
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
                //SetWidnowMaskVisible();
                window.ShowMe();

            }
            return window;
        }
        else
            Debug.LogError(winName + " 窗口不存在");
        return null;
    }
     private GameObject TempLoadWindow(string wndName)
    {
        //自己搞时要改成AB或者Adreesable
        GameObject window = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Window/" + wndName), mUIRoot);
        window.transform.SetParent(mUIRoot);
        window.transform.localScale = Vector3.one;
        window.transform.localPosition = Vector3.zero;
        window.transform.rotation = Quaternion.identity;
        window.name = wndName;
        return window;
    }
}
