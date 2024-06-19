using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : BasePanel<GamePanel>
{
    // ��ȡ�ؼ� ���������ϵ� �ؼ�����
    // ����
    public CustomGUILabel labScore;
    // ʱ��
    public CustomGUILabel labTime;
    // �˳���ť
    public CustomGUIButton btnQuit;
    // ���ð�ť
    public CustomGUIButton btnSetting;
    // Ѫ��ͼ
    public CustomGUITexture texHP;
    // Ѫ���ؼ��Ŀ�
    public float hpW = 800;
    // ���ڼ�¼����ҵ�ǰ����
    [HideInInspector]
    public int nowScore = 0;
    // ���ڼ�¼����ҵ�ǰ����ʱ��
    [HideInInspector]
    public float nowTime = 0;
    private int time;
    // Start is called before the first frame update
    void Start()
    {
        // �����¼�
        btnSetting.clickEvent += () =>
        {
            // ���������
            SettingPanel.Instance.ShowMe();
            // �ı�ʱ������ֵ Ϊ0 ����ʱ��ֹͣ
            Time.timeScale = 0;
        };

        btnQuit.clickEvent += () =>
        {
            // ���ؿ�ʼ��Ϸ����
            // ����һ��ȷ����ť
            QuitPanel.Instance.ShowMe();
            // �ı�ʱ������ֵ Ϊ0 ����ʱ��ֹͣ
            Time.timeScale = 0;
        };
    }

    // Update is called once per frame
    void Update()
    {
        // ͨ��֡���ʱ�� �����ۼ� ��Ƚ�׼ȷ
        nowTime += Time.deltaTime;

        // ���� ת���� ʱ �� ��
        time = (int)nowTime;
        labTime.content.text = "";
        if (time / 3600 > 0)
        {
            labTime.content.text += time / 3600 + "ʱ";
        }
        if (time % 3600 / 60 > 0 || labTime.content.text != "")
        {
            labTime.content.text += time % 3600 / 60 + "��";
        }
        labTime.content.text += time % 60 + "��";
    }

    /// <summary>
    /// �ṩ���ⲿ�ӷֵķ���
    /// </summary>
    /// <param name="score"></param>
    public void AddScore(int score)
    {
        nowScore += score;
        // ���������ʾ
        labScore.content.text = nowScore.ToString();
    }

    /// <summary>
    /// ����Ѫ���ķ���
    /// </summary>
    /// <param name="maxHP"></param>
    /// <param name="HP"></param>
    public void UpdateHP(int maxHP,int HP)
    {
        texHP.guiPos.width = (float)HP/maxHP * hpW;
    }
}
