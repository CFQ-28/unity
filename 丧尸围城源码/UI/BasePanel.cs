using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    //专门用于控制面板透明度的组件
    private CanvasGroup canvasGroup;
    //淡入淡出的速度
    private float alphaSpeed = 10;

    public bool isShow = false;

    // 当隐藏完毕后 想要做的事情
    private UnityAction hideCallBack = null;

    protected virtual void Awake()
    {
        //一开始获取面板上挂载的 CanvasGroup组件
        canvasGroup = this.GetComponent<CanvasGroup>();
        //如果忘记添加这样一个脚本
        if(canvasGroup == null)
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Init();
    }

    /// <summary>
    /// 注册控件事件的方法 所有的子面板 都需要去注册一些控件事件
    /// 所以写成抽象方法 让子类必须去实现
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// 显示自己面板时逻辑
    /// </summary>
    public virtual void ShowMe()
    {
        canvasGroup.alpha = 0;
        isShow = true;
    }

    /// <summary>
    /// 隐藏自己面板时逻辑
    /// </summary>
    public virtual void HideMe(UnityAction callBack) // 委托
    {
        canvasGroup.alpha = 1;
        isShow = false;

        hideCallBack = callBack;
    }

    // Update is called once per frame
    void Update()
    {
        // 当处于显示状态时 如果透明度 不为1 就会不停的加到1 加到1 过后 就会停止变化了
        // 淡入
        if(isShow && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1)
                canvasGroup.alpha = 1;
        }
        // 淡出
        else if(!isShow && canvasGroup.alpha != 0)
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
            if(canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                //面板淡出后 执行的一些逻辑
                hideCallBack?.Invoke();
            }
        }
    }
}
