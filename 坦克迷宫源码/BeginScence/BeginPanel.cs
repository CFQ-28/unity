using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BeginPanel : BasePanel<BeginPanel>
{
    // �������������ĳ�Ա���� �����������ؼ�
    public CustomGUIButton btnBegin;
    public CustomGUIButton btnSetting;
    public CustomGUIButton btnQuit;
    public CustomGUIButton btnRank;

    // Start is called before the first frame update
    void Start()
    {
        //Ŀ������Ϊ�������̹�˵�ͷ��ת�� ������������ڴ����� ��������ȥ���ÿ���
        Cursor.lockState = CursorLockMode.Confined;

        // ����һ�ΰ�ť�������Ҫ��ʲô
        btnBegin.clickEvent += () =>
        {
            // �л�����
            SceneManager.LoadScene("GameScene");
        };
        btnSetting.clickEvent += () => {
            // ��������Ϸ���
            SettingPanel.Instance.ShowMe();
            // �����Լ� ���⴩͸
            HideMe();
        };
        btnQuit.clickEvent += () =>
        {
            // �˳���Ϸ
            Application.Quit();
        };
        btnRank.clickEvent += () =>
        {
            // �����а����
            RankPanel.Instance.ShowMe();
            // �����Լ� ���⴩͸
            HideMe();
        };
    }

}
