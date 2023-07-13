using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 面板基类
/// 实现淡入淡出，初始化,显示隐藏方法
/// </summary>
public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private float fadSpeed = 10;

    private bool isShow;

    private UnityAction hideCallBack;
    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    // Start is called before the first frame update
   protected virtual void Start()
    {
        Init();
    }

   public abstract void Init();

   public virtual void ShowMe()
   {
       isShow = true;
       canvasGroup.alpha = 0;
   }

   public virtual void HideMe(UnityAction callBack)
   {
       isShow = false;
       canvasGroup.alpha = 1;
       hideCallBack = callBack;
   }
   
    // Update is called once per frame
    void Update()
    {
        if (isShow && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += fadSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
            }
        }

        if (!isShow && canvasGroup.alpha != 0)
        {
            canvasGroup.alpha -= fadSpeed * Time.deltaTime;
            if (canvasGroup.alpha <=0)
            {
                canvasGroup.alpha = 0;
                //让管理器删除自己
                hideCallBack?.Invoke();
            }
        }
    }
}
