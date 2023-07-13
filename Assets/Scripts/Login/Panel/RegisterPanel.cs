using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
    public Button btnCancel;
    public Button btnConfirm;
    
    public InputField InputUN;
    public InputField InputPW;
    public override void Init()
    {
        //点击取消
        btnCancel.onClick.AddListener(() =>
        {
            //返回登陆页面
            UIManager.Instance.ShowPanel<LoginPanel>();
            //隐藏自己
            UIManager.Instance.HidePanel<RegisterPanel>();
        });
        //点击确定
        btnConfirm.onClick.AddListener(() =>
        {
            if (LoginManager.Instance.RegisterUser(InputUN.text, InputPW.text))
            {
                //注册成功
                //返回登陆页面
                UIManager.Instance.ShowPanel<LoginPanel>();
                UIManager.Instance.HidePanel<RegisterPanel>();
                //清理登陆数据
                LoginManager.Instance.ClearLoginData();
                
                
                //显示用户名和密码
              LoginPanel panel=  UIManager.Instance.GetPanel<LoginPanel>();
              panel.SetUserInfo(InputUN.text, InputPW.text);
              
              
            }
            else
            {
                //提示用户名已存在
               TipPanel tipPanel= UIManager.Instance.ShowPanel<TipPanel>();
               tipPanel.ChangeInfo("用户名已存在！");
               //清空账号密码内容
               InputPW.text = "";
               InputUN.text = "";
            }
        });
    }
}
