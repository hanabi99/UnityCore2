/*---------------------------------
 *Title:UI表现层脚本自动化生成工具
 *Author:ZM 铸梦
 *Date:2022/5/16 22:10:23
 *Description:UI 表现层，该层只负责界面的交互、表现相关的更新，不允许编写任何业务逻辑代码
 *注意:以下文件是自动生成的，再次生成不会覆盖原有的代码，会在原有的代码上进行新增，可放心使用
---------------------------------*/
using UnityEngine.UI;
using UnityEngine;
using ZMUIFrameWork;
using System.Reflection;

public class HallWindow : WindowBase
{

    public HallWindowDataComponent dataCompt;

    #region 声明周期函数
    //调用机制与Mono Awake一致
    public override void Init()
    {
        dataCompt = gameObject.GetComponent<HallWindowDataComponent>();
        dataCompt.InitComponent(this);
        base.Init();
    }
    //物体显示时执行
    public override void ShowMe()
    {
        base.ShowMe();
        //UIModule.Instance.PushWindowToStack<SettingWIndow>();
        //UIModule.Instance.PushWindowToStack<ChatWIndow>();
        //UIModule.Instance.PushWindowToStack<UserInfoWIndow>();

        //UIModule.Instance.StartPopFirstStackWindow();
        UIMrg.GetInstance().PushWindowToStack<UserInfoWIndow>();
        UIMrg.GetInstance().PushWindowToStack<SettingWIndow>();
        UIMrg.GetInstance().PushWindowToStack<ChatWIndow>();

        UIMrg.GetInstance().StartPopFirstStackWindow();


    }
    //物体隐藏时执行
    public override void HideMe()
    {
        base.HideMe();
    }
    //物体销毁时执行
    public override void OnDestroy()
    {
        base.OnDestroy();
    }
    #endregion
    #region API Function

    #endregion
    #region UI组件事件
    public void OnChatButtonClick()
    {
        UIMrg.GetInstance().PopUpWindow<ChatWIndow>();
    }
    public void OnSettingButtonClick()
    {
        UIMrg.GetInstance().PopUpWindow<SettingWIndow>();
    }
    public void OnUserInfoButtonClick()
    {
        UIMrg.GetInstance().PopUpWindow<UserInfoWIndow>();
    }
    public void OnFriendButtonClick()
    {
        UIMrg.GetInstance().PopUpWindow<FriendWIndow>();
    }
    #endregion
}
