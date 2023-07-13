using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SeverRightItem : MonoBehaviour
{
    public Button btnSelf;

    public Text txtInfo;

    public Image imgState;

    public Image imgNew;

    //当前选择的服务器
    public SeverInfo nowSeverInfo;
    // Start is called before the first frame update
    void Start()
    {
        btnSelf.onClick.AddListener(() =>
        {
            //当前选择服务器
            LoginManager.Instance.LoginData.chooseSever = nowSeverInfo.id;
            //隐藏选服面板
            UIManager.Instance.HidePanel<SeverListPanel>();
            //显示服务器面板
            UIManager.Instance.ShowPanel<SeverPanel>();
        });
    }

    public void InitInfo(SeverInfo info)
    {
        nowSeverInfo = info;
        txtInfo.text = info.id + "区" + info.name;
        imgNew.gameObject.SetActive(info.isNew);
        imgState.gameObject.SetActive(true);
        //加载图集
        SpriteAtlas sa = Resources.Load<SpriteAtlas>("Login");
        
        switch (info.state)
        {
            case 0:
                imgState.gameObject.SetActive(false);
                break;
            case 1://流畅
                imgState.sprite = sa.GetSprite("ui_DL_liuchang_01");
                break;
            case 2://繁忙
                imgState.sprite = sa.GetSprite("ui_DL_fanhua_01");
                break;
            case 3://火爆
                imgState.sprite = sa.GetSprite("ui_DL_huobao_01");
                break;
            case 4://维护
                imgState.sprite = sa.GetSprite("ui_DL_weihu_01");
                break;
            
        }

    }
}
