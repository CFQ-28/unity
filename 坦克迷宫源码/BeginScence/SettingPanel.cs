using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingPanel : BasePanel<SettingPanel>
{
    // 1������Ա���� �����ؼ�
    public CustomGUISlider sliderMusic;
    public CustomGUISlider sliderSound;

    public CustomGUIToggle togMusic;
    public CustomGUIToggle togSound;

    public CustomGUIButton btnClose;

    // Start is called before the first frame update
    void Start()
    {
        // 2������Ӧ�¼� ��������߼�
        // �������ֵı仯
        sliderMusic.changeValue += (value) => GameDataMgr.Instance.ChangeBKValue(value);
        // ������Ч�ı仯
        sliderSound.changeValue += (value) => GameDataMgr.Instance.ChangeSoundValue(value);
        // �������ֿ���
        togMusic.changeValue += (value) => GameDataMgr.Instance.OpenOrCloseBKMusic(value);
        // ������Ч�Ŀ���
        togSound.changeValue += (value) => GameDataMgr.Instance.OpenOrCloseSound(value);

        btnClose.clickEvent += () =>
        {
            // �����Լ�
            HideMe();
            // �жϵ�ǰ���ڳ���
            if (SceneManager.GetActiveScene().name == "BeginScene")
            {
                // �ÿ�ʼ�����ʾ����
                BeginPanel.Instance.ShowMe();
            }
        };

        HideMe();
    }

    public void UpdatePanelInfo()
    {
        // ��������ϵ���Ϣ���Ǹ��� ��Ч���� ���µ�
        MusicData data = GameDataMgr.Instance.musicData;

        // �����������
        sliderMusic.nowValue = data.bkValue;
        sliderSound.nowValue = data.soundValue;
        togMusic.isSel = data.isOpenBK;
        togSound.isSel = data.isOpenSound;
    }

    public override void ShowMe()
    {
        base.ShowMe();
        // ÿ����ʾ���ʱ �����������
        UpdatePanelInfo();
    }

    public override void HideMe()
    {
        base.HideMe();
        Time.timeScale = 1;
    }
}
