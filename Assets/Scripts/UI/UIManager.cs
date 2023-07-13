using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// UI管理类
/// </summary>
public class UIManager
{
    private static UIManager instance = new UIManager();
    public static UIManager Instance => instance;

    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    private Transform canvasTrans;
    private UIManager()
    {
        canvasTrans = GameObject.Find("Canvas").transform;
        GameObject.DontDestroyOnLoad(canvasTrans.gameObject);
    }
    //显示面板
    public T ShowPanel<T>() where T: BasePanel
    {
        string panelName = typeof(T).Name;
        
        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }
        
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        panelObj.transform.SetParent(canvasTrans,false);

        T panel = panelObj.GetComponent<T>();
        panelDic.Add(panelName,panel);
        panel.ShowMe();
        return panel;
    }
    //隐藏面板
    public void HidePanel<T>(bool isFade = true) where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            if (isFade)
            {
                panelDic[panelName].HideMe(()=>
                {
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    panelDic.Remove(panelName);
                });
            }
            else
            {
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
        }
    }
    //获得面板
    public T GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T ;
        }

        return null;
    }
    
}
