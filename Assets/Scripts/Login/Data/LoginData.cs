using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginData
{
    public string userName;
    public string passWord;
    public bool rememberPW;
    public bool autoLogin;
    //选择服务器数据
    //-1表示没有选择过服务器
    public int chooseSever=-1;
}
