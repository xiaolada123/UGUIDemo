using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginManager
{
    private static LoginManager instance = new LoginManager();
    public static LoginManager Instance => instance;

    //登陆选项数据
    private LoginData loginData;
    public LoginData LoginData => loginData;

    //注册信息数据
    private RegisterData registerData;
    public RegisterData RegisterData => registerData;

    //服务器信息数据
    private List<SeverInfo> severData;
    public List<SeverInfo> SeverData=>severData;
    private LoginManager()
    {
        loginData = SaveSystem.LoadFromJson<LoginData>("LoginData");
        severData = SaveSystem.LoadFromJson<List<SeverInfo>>("SeverInfo");
        registerData = SaveSystem.LoadFromJson<RegisterData>("RegisterData");
        
    }

    public void ClearLoginData()
    {
        loginData.chooseSever = -1;
        loginData.autoLogin = false;
        loginData.rememberPW = false;
    }
    public void SaveLoginData()
    {
        SaveSystem.SaveByJson("LoginData",loginData);
    }

    public void SaveRegisterData()
    {
        SaveSystem.SaveByJson("RegisterData",registerData);
    }

    //注册用户方法
    public bool RegisterUser(string userName, string passWord)
    {
        //检查用户是否存在
        if (registerData.registerInfo.ContainsKey(userName))
        {
            return false;
        }
        
        registerData.registerInfo.Add(userName,passWord);
        SaveRegisterData();

        return true;
    }
    //验证用户名密码
    public bool CheckInfo(string userName, string passWord)
    {
        if (registerData.registerInfo.ContainsKey(userName))
        {
            if (registerData.registerInfo[userName] == passWord)
            {
                return true;
            }
        }

        return false;
    }
    
}
