using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    public Button btnRegister;
    public Button btnConfirm;
    public InputField InputUN;
    public InputField InputPW;

    public Toggle togRemember;
    public Toggle togAuto;
    public override void Init()
    {
        //点击注册
        btnRegister.onClick.AddListener(() =>
        {
            //显示注册页面
            UIManager.Instance.ShowPanel<RegisterPanel>();
            //隐藏自己
            UIManager.Instance.HidePanel<LoginPanel>();
        });
        //点击登陆
        btnConfirm.onClick.AddListener(() =>
        {
            //验证账号密码是否正确
            if (LoginManager.Instance.CheckInfo(InputUN.text, InputPW.text))
            {
                //记录数据
                LoginManager.Instance.LoginData.autoLogin = togAuto.isOn;
                LoginManager.Instance.LoginData.rememberPW = togRemember.isOn;
                LoginManager.Instance.LoginData.userName = InputUN.text;
                LoginManager.Instance.LoginData.passWord = InputPW.text;
                LoginManager.Instance.SaveLoginData();
                //显示选服页面
                if (LoginManager.Instance.LoginData.chooseSever != -1)
                {
                    //显示选好服务器的页面
                    UIManager.Instance.ShowPanel<SeverPanel>();
                }
                else
                {
                    //显示服务器列表
                    UIManager.Instance.ShowPanel<SeverListPanel>();
                }
                //隐藏自己
                UIManager.Instance.HidePanel<LoginPanel>();
            }
            else
            {
                //提示用户名或密码错误
               TipPanel tipPanel= UIManager.Instance.ShowPanel<TipPanel>();
               tipPanel.ChangeInfo("用户名或密码错误！");
            }
            
        });
        //点击记住密码
        togRemember.onValueChanged.AddListener((isOn) =>
        {
            if (!isOn)
            {
                togAuto.isOn = false;
            }
        });
        
        
        //点击自动登陆
        togAuto.onValueChanged.AddListener((isOn) =>
        {
            //如果记住密码没有被选中，让他选中
            if (isOn)
            {
                togRemember.isOn = true;
            }
        });
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //显示时根据数据更新面板内容
        LoginData loginData = LoginManager.Instance.LoginData;

        togRemember.isOn = loginData.rememberPW;
        togAuto.isOn = loginData.autoLogin;
        InputUN.text = loginData.userName;
        if (togRemember.isOn)
        {
            InputPW.text = loginData.passWord;
        }

        if (togAuto.isOn)
        {
            //自动登陆
            //显示选服页面
            if (LoginManager.Instance.CheckInfo(InputUN.text, InputPW.text))
            {
                //显示选服页面
                if (LoginManager.Instance.LoginData.chooseSever != -1)
                {
                    //显示选好服务器的页面
                    UIManager.Instance.ShowPanel<SeverPanel>();
                }
                else
                {
                    //显示服务器列表
                    UIManager.Instance.ShowPanel<SeverListPanel>();
                }
                //隐藏自己
                UIManager.Instance.HidePanel<LoginPanel>(false);
            }else
            {
                //提示用户名或密码错误
                TipPanel tipPanel= UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("用户名或密码错误！");
            }
            
        }
    }

    public void SetUserInfo(string userName,string passWord)
    {
        InputUN.text = userName;
        InputPW.text = passWord;
    }
}
