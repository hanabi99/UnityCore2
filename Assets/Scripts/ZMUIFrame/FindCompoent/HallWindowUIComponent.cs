/*---------------------------------
 *Title:UI自动化组件查找代码生成工具
 *Author:铸梦
 *Date:2024/2/14 16:19:16
 *Description:变量需要以[Text]括号加组件类型的格式进行声明，然后右键窗口物体—— 一键生成UI组件查找脚本即可
 *注意:以下文件是自动生成的，任何手动修改都会被下次生成覆盖,若手动修改后,尽量避免自动生成
---------------------------------*/
using UnityEngine.UI;
using UnityEngine;

namespace ZMUIFrameWork
{
	public class HallWindowUIComponent
	{
		public   Button  ChatButton;

		public   Button  SettingButton;

		public   Button  UserInfoButton;

		public   Button  FriendButton;

		public  void InitComponent(WindowBase target)
		{
		     //组件查找
		     ChatButton =target.transform.Find("UIContent/[Button]Chat").GetComponent<Button>();
		     SettingButton =target.transform.Find("UIContent/[Button]Setting").GetComponent<Button>();
		     UserInfoButton =target.transform.Find("UIContent/[Button]UserInfo").GetComponent<Button>();
		     FriendButton =target.transform.Find("UIContent/[Button]Friend").GetComponent<Button>();
	
	
		     //组件事件绑定
		     HallWindow mWindow=(HallWindow)target;
		     target.AddButtonClickListener(ChatButton,mWindow.OnChatButtonClick);
		     target.AddButtonClickListener(SettingButton,mWindow.OnSettingButtonClick);
		     target.AddButtonClickListener(UserInfoButton,mWindow.OnUserInfoButtonClick);
		     target.AddButtonClickListener(FriendButton,mWindow.OnFriendButtonClick);
		}
	}
}
