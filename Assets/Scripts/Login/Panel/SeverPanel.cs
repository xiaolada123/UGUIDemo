using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeverPanel : BasePanel
{
    public Button btnBack;
    public Button btnChangeSever;
    public Button btnConfirm;
    public Text txtSeverName;
    public override void Init()
    {
        //点击返回
        btnBack.onClick.AddListener(() =>
        {
            //隐藏自己
            UIManager.Instance.HidePanel<SeverPanel>();
            LoginManager.Instance.LoginData.autoLogin = false;
            //显示登陆面板
            UIManager.Instance.ShowPanel<LoginPanel>();
        });
        //点击换区
        btnChangeSever.onClick.AddListener(() =>
        {
            //隐藏自己
            UIManager.Instance.HidePanel<SeverPanel>();
            //显示服务器列表
            UIManager.Instance.ShowPanel<SeverListPanel>();
        });
        //点击进入游戏
        btnConfirm.onClick.AddListener(() =>
        {
            //保存登陆信息
            LoginManager.Instance.SaveLoginData();
            //隐藏自己
            UIManager.Instance.HidePanel<SeverPanel>();
            //进入游戏场景
            UIManager.Instance.HidePanel<LoginBKPanel>();
            SceneManager.LoadScene("GameScene");
        });
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //更新显示服务器数据
      SeverInfo severInfo=  LoginManager.Instance.SeverData[LoginManager.Instance.LoginData.chooseSever - 1];
      Debug.Log(LoginManager.Instance.LoginData.chooseSever);
      txtSeverName.text =severInfo.id+"区  "+ severInfo.name;
    }
}
