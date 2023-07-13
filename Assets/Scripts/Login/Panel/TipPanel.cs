using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    public Button btnConfirm;
    public Text txtInfo;
    
    public override void Init()
    {
        btnConfirm.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<TipPanel>();
        });
    }

    public void ChangeInfo(string info)
    {
        txtInfo.text = info;
    }
}
