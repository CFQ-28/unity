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
            // �ص���ʼ��Ϸ����
            SceneManager.LoadScene("BeginScene");
        };

        btnGoOn.clickEvent += () =>
        {
            // �ر����
            HideMe();
        };
        btnClose.clickEvent += () =>
        {
            //�ر����
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
