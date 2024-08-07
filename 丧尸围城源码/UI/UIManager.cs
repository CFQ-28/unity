using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager instance = new UIManager();
    public static UIManager Instance => instance;

    //用于存储显示着的面板的 每显示一个面板 就会存入这个字典
    //隐藏面板时 直接获取字典中的对应面板 进行隐藏
    private Dictionary<string,BasePanel> panelDic = new Dictionary<string,BasePanel>();

    //场景中的 Canvas对象 用于设置为面板的父对象
    private Transform canvasTrans;

    private UIManager()
    {
        //得到场景中的Canvas对象
        //Resources为Assets文件中的一个文件夹 确保该文件中的UI文件夹存在Canvas预制体
        GameObject canvas = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
        canvasTrans = canvas.transform;
        //过场景不移除该对象 保证这个游戏过程中 只有一个 canvas对象
        GameObject.DontDestroyOnLoad(canvas);
    }

    // 显示面板
    public T ShowPanel<T>() where T : BasePanel
    {
        //只需要保证 泛型T的类型 和面板预设体名字 一样 
        string panelName = typeof(T).Name;

        //判断 字典中 是否已经显示了这个面板
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;

        //显示面板 根据面板名字 动态的创建预设体 设置父对象
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        //把这个对象 放到场景中的 Canvas下面
        panelObj.transform.SetParent(canvasTrans, false);

        //显示面板上 显示逻辑 并且应该把它保存起来
        T panel = panelObj.GetComponent<T>();
        //把面板脚本 存储到字典中 方便之后的 获取 和 隐藏
        panelDic.Add(panelName, panel);
        //调用面板的显示逻辑
        panel.ShowMe();

        return panel;
    }
 
    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <typeparam name="T"> 面板类名 </typeparam>
    /// <param name="isFade"> 是否选择淡出完毕后才删除面板 默认是true </param>
    public void HidePanel<T>(bool isFade = true) where T:BasePanel
    {
        //根据泛型得名字
        string panelName = typeof(T).Name;
        //判断当前显示的面板 有没有你想要隐藏的
        if(panelDic.ContainsKey(panelName))
        {
            if(isFade)
            {
                //让面板 淡出完毕过后 再删除它
                panelDic[panelName].HideMe(() =>
                {
                    //删除对象
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    //删除字典里面存储的面板脚本
                    panelDic.Remove(panelName);
                });
            }
            else
            {
                //删除对象
                GameObject.Destroy(panelDic[panelName].gameObject);
                //删除字典里面存储的面板脚本
                panelDic.Remove(panelName);
            }
        }
    }

    // 得到面板
    public T GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;
        //如果没有对应面板显示 就返回空
        return null;
    }
}
