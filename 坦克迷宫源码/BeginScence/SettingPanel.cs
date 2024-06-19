using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingPanel : BasePanel<SettingPanel>
{
    // 1声明成员变量 关联控件
    public CustomGUISlider sliderMusic;
    public CustomGUISlider sliderSound;

    public CustomGUIToggle togMusic;
    public CustomGUIToggle togSound;

    public CustomGUIButton btnClose;

    // Start is called before the first frame update
    void Start()
    {
        // 2监听对应事件 处理相关逻辑
        // 处理音乐的变化
        sliderMusic.changeValue += (value) => GameDataMgr.Instance.ChangeBKValue(value);
        // 处理音效的变化
        sliderSound.changeValue += (value) => GameDataMgr.Instance.ChangeSoundValue(value);
        // 处理音乐开关
        togMusic.changeValue += (value) => GameDataMgr.Instance.OpenOrCloseBKMusic(value);
        // 处理音效的开关
        togSound.changeValue += (value) => GameDataMgr.Instance.OpenOrCloseSound(value);

        btnClose.clickEvent += () =>
        {
            // 隐藏自己
            HideMe();
            // 判断当前所在场景
            if (SceneManager.GetActiveScene().name == "BeginScene")
            {
                // 让开始面板显示出来
                BeginPanel.Instance.ShowMe();
            }
        };

        HideMe();
    }

    public void UpdatePanelInfo()
    {
        // 我们面板上的信息都是根据 音效数据 更新的
        MusicData data = GameDataMgr.Instance.musicData;

        // 设置面板内容
        sliderMusic.nowValue = data.bkValue;
        sliderSound.nowValue = data.soundValue;
        togMusic.isSel = data.isOpenBK;
        togSound.isSel = data.isOpenSound;
    }

    public override void ShowMe()
    {
        base.ShowMe();
        // 每次显示面板时 更新面板内容
        UpdatePanelInfo();
    }

    public override void HideMe()
    {
        base.HideMe();
        Time.timeScale = 1;
    }
}
