/*---------------------------------
 *Title:UI自动化组件查找代码生成工具
 *Author:THC
 *Date:2024/2/11 15:15:03
 *Description:变量需要以[Text]括号加组件类型的格式进行声明，然后右键窗口物体—— 一键生成UI组件查找脚本即可
 *注意:以下文件是自动生成的，任何手动修改都会被下次生成覆盖,若手动修改后,尽量避免自动生成
---------------------------------*/
using UnityEngine.UI;
using UnityEngine;

namespace ZMUIFrameWork
{
	public class LoginWIndowUIComponent
	{
		public  Button  LoginButton;

		public  void InitComponent(WindowBase target)
		{
		     //组件查找
		     LoginButton =target.transform.Find("UIContent/[Button]Login").GetComponent<Button>();
	
	
		     //组件事件绑定
		     LoginWIndow mWindow=(LoginWIndow)target;
		     target.AddButtonClickListener(LoginButton,mWindow.OnLoginButtonClick);
		}
	}
}
