using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WindowBase : WindowBehaviour
{
    private List<Button> mButtonList = new List<Button>();

    private List<Toggle> mToggleList = new List<Toggle>();

    private List<InputField> mInputFieldList = new List<InputField>();

    private CanvasGroup mUIMask;

    private CanvasGroup mCanvasGroup;

    protected Transform mUIContent;

    /// <summary>
    /// 初始化基类组件
    /// </summary>
    private void InitializeBaseComponent()
    {
        mCanvasGroup = transform.GetComponent<CanvasGroup>();
        mUIMask = transform.Find("UIMask").GetComponent<CanvasGroup>();
        mUIContent = transform.Find("UIContent").transform;
    }


    /// <summary>
    /// 按钮添加事件
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="action"></param>
    public void AddButtonClickListener(Button btn, UnityAction action)
    {
        if (btn != null)
        {
            if (!mButtonList.Contains(btn))
            {
                mButtonList.Add(btn);
            }
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(action);
        }
    }
    /// <summary>
    /// Toggle添加事件
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="action"></param>
    public void AddToggleClickListener(Toggle toggle, UnityAction<bool, Toggle> action)
    {
        if (toggle != null)
        {
            if (!mToggleList.Contains(toggle))
            {
                mToggleList.Add(toggle);
            }
            toggle.onValueChanged.RemoveAllListeners();
            toggle.onValueChanged.AddListener((isOn) =>
            {
                action?.Invoke(isOn, toggle);
            });
        }
    }
    /// <summary>
    /// InputField添加事件
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="action"></param>
    public void AddInputFieldClickListener(InputField inputField, UnityAction<string> onChangeAction, UnityAction<string> endInputAction)
    {
        if (inputField != null)
        {
            if (!mInputFieldList.Contains(inputField))
            {
                mInputFieldList.Add(inputField);
            }
            inputField.onValueChanged.RemoveAllListeners();
            inputField.onEndEdit.RemoveAllListeners();
            inputField.onValueChanged.AddListener(onChangeAction);
            inputField.onEndEdit.AddListener(endInputAction);
        }
    }
    /// <summary>
    /// 移除所有按钮监听
    /// </summary>
    public void RemoveAllButtonListener()
    {
        foreach (var item in mButtonList)
        {
            item.onClick.RemoveAllListeners();
        }
    }
    /// <summary>
    /// 移除所有Toggle监听
    /// </summary>
    public void RemoveAllToggleListener()
    {
        foreach (var item in mToggleList)
        {
            item.onValueChanged.RemoveAllListeners();
        }
    }
    /// <summary>
    /// 移除所有InputField监听
    /// </summary>
    public void RemoveAllInputFieldListener()
    {
        foreach (var item in mInputFieldList)
        {
            item.onValueChanged.RemoveAllListeners();
            item.onEndEdit.RemoveAllListeners();
        }
    }
    public override void Init()
    {
        base.Init();
        InitializeBaseComponent();
    }
    public override void ShowMe()
    {
        base.ShowMe();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    public override void HideMe()
    {
        base.HideMe();
        UIMrg.GetInstance().HideWindow(name);
    }

    public override void SetActive(bool Active)
    {
        base.SetActive(Active);
        //gameObject.SetActive(Active);
        mCanvasGroup.alpha = Active ? 1 : 0;
        mCanvasGroup.blocksRaycasts = Active;
        isActive = Active;
    }
    public void SetMaskVisible(bool isVisble)
    {
        if (!UISetting.Instance.SINGMASK_SYSTEM)
        {
            return;
        }
        mUIMask.alpha = isVisble ? 1 : 0;
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        RemoveAllButtonListener();
        RemoveAllInputFieldListener();
        RemoveAllToggleListener();
        mButtonList.Clear();
        mToggleList.Clear();
        mInputFieldList.Clear();
    }
}

