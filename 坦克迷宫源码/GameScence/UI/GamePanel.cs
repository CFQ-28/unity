using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : BasePanel<GamePanel>
{
    // 获取控件 关联场景上的 控件对象
    // 分数
    public CustomGUILabel labScore;
    // 时间
    public CustomGUILabel labTime;
    // 退出按钮
    public CustomGUIButton btnQuit;
    // 设置按钮
    public CustomGUIButton btnSetting;
    // 血量图
    public CustomGUITexture texHP;
    // 血条控件的宽
    public float hpW = 800;
    // 用于记录该玩家当前分数
    [HideInInspector]
    public int nowScore = 0;
    // 用于记录该玩家当前所有时间
    [HideInInspector]
    public float nowTime = 0;
    private int time;
    // Start is called before the first frame update
    void Start()
    {
        // 监听事件
        btnSetting.clickEvent += () =>
        {
            // 打开设置面板
            SettingPanel.Instance.ShowMe();
            // 改变时间缩放值 为0 就是时间停止
            Time.timeScale = 0;
        };

        btnQuit.clickEvent += () =>
        {
            // 返回开始游戏界面
            // 弹出一个确定按钮
            QuitPanel.Instance.ShowMe();
            // 改变时间缩放值 为0 就是时间停止
            Time.timeScale = 0;
        };
    }

    // Update is called once per frame
    void Update()
    {
        // 通过帧间隔时间 进行累加 会比较准确
        nowTime += Time.deltaTime;

        // 把秒 转换成 时 分 秒
        time = (int)nowTime;
        labTime.content.text = "";
        if (time / 3600 > 0)
        {
            labTime.content.text += time / 3600 + "时";
        }
        if (time % 3600 / 60 > 0 || labTime.content.text != "")
        {
            labTime.content.text += time % 3600 / 60 + "分";
        }
        labTime.content.text += time % 60 + "秒";
    }

    /// <summary>
    /// 提供给外部加分的方法
    /// </summary>
    /// <param name="score"></param>
    public void AddScore(int score)
    {
        nowScore += score;
        // 更新面板显示
        labScore.content.text = nowScore.ToString();
    }

    /// <summary>
    /// 更新血条的方法
    /// </summary>
    /// <param name="maxHP"></param>
    /// <param name="HP"></param>
    public void UpdateHP(int maxHP,int HP)
    {
        texHP.guiPos.width = (float)HP/maxHP * hpW;
    }
}
