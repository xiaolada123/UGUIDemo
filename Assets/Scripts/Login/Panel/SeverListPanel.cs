using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class SeverListPanel: BasePanel
{
    public ScrollRect svLeft;
    public ScrollRect svRight;

    public Text txtName;
    public Image imgState;

    public Text txtRange;

    private List<GameObject> itemList = new List<GameObject>();
    public override void Init()
    {
        List<SeverInfo> infoList = LoginManager.Instance.SeverData;
        int num;
        if (infoList.Count % 5 == 0)
        {
            num = infoList.Count / 5;
        }
        else
        {
            num = infoList.Count / 5 + 1;
        }

        for (int i = 0; i < num; i++)
        {
            //动态创建预设体对象
            GameObject item = Instantiate(Resources.Load<GameObject>("UI/btnSeverRangeLeft"));
            item.transform.SetParent(svLeft.content,false);
            //初始化
            SeverLeftItem severLeft = item.GetComponent<SeverLeftItem>();
            int beginIndex = i * 5 + 1;
            int endIndex = 5 * (i + 1);
            if (endIndex > infoList.Count)
            {
                endIndex = infoList.Count;
            }
            
            
            severLeft.InitInfo(beginIndex,endIndex);
        }
    }

    public override void ShowMe()
    {
        base.ShowMe();
        int id = LoginManager.Instance.LoginData.chooseSever;
        //初始化上一次选择的服务器
        if (id <= 0)
        {
            txtName.text = "无";
            imgState.gameObject.SetActive(false);
        }
        else
        {
            SeverInfo info = LoginManager.Instance.SeverData[id - 1];
            txtName.text = info.id + "区  " + info.name;
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
        UpdatePanel(1,5>LoginManager.Instance.SeverData.Count?LoginManager.Instance.SeverData.Count:5);
    }

    public void UpdatePanel(int beginIndex, int endIndex)
    {
        //更新服务器区间显示
        txtRange.text = "服务器 " + beginIndex + "-" + endIndex;
        //删除之前的单个按钮
        for (int i = 0; i < itemList.Count; i++)
        {
            Destroy(itemList[i]);
        }
       // itemList.Clear();
        //创建新的按钮
        for (int i = beginIndex; i <= endIndex; i++)
        {
            SeverInfo nowInfo = LoginManager.Instance.SeverData[i - 1];

            GameObject severItem = Instantiate(Resources.Load<GameObject>("UI/btnSever"));
            severItem.transform.SetParent(svRight.content,false);

            SeverRightItem rightItem = severItem.GetComponent<SeverRightItem>();
            rightItem.InitInfo(nowInfo);
            itemList.Add(severItem);
        }
        
        
    }
}
