using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeverLeftItem : MonoBehaviour
{
    public Button btnSelf;
    public Text txtInfo;

    private int beginIndex;
    private int endIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        btnSelf.onClick.AddListener(() =>
        {
            SeverListPanel panel = UIManager.Instance.GetPanel<SeverListPanel>();
            panel.UpdatePanel(beginIndex,endIndex);
        });
    }

    public void InitInfo(int beginIndex, int endIndex)
    {
        this.beginIndex = beginIndex;
        this.endIndex = endIndex;


        txtInfo.text = this.beginIndex + "-" + endIndex + "区";
    }
    
}
