using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitPanel : BasePanel<QuitPanel>
{
    public CustomGUIButton btnQiut;
    public CustomGUIButton btnGoOn;
    public CustomGUIButton btnClose;

    // Start is called before the first frame update
    void Start()
    {
        btnQiut.clickEvent += () =>
        {
            // 回到开始游戏界面
            SceneManager.LoadScene("BeginScene");
        };

        btnGoOn.clickEvent += () =>
        {
            // 关闭面板
            HideMe();
        };
        btnClose.clickEvent += () =>
        {
            //关闭面板
            HideMe();
        };

        HideMe();
    }

    public override void HideMe()
    {
        base.HideMe();
        Time.timeScale = 1;
    }
}
