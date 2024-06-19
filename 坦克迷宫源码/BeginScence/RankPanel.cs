using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : BasePanel<RankPanel>
{
    // ������صĿؼ�

    public CustomGUIButton btnClose;
    // �ؼ��϶�
    private List<CustomGUILabel> labPName = new List<CustomGUILabel>();
    private List<CustomGUILabel> labScore = new List<CustomGUILabel>();
    private List<CustomGUILabel> labTime = new List<CustomGUILabel>();


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= 10; i++)
        {
            labPName.Add(this.transform.Find("PName/labPName" + i).GetComponent<CustomGUILabel>());
            labScore.Add(this.transform.Find("Score/labScore" + i).GetComponent<CustomGUILabel>());
            labTime.Add(this.transform.Find("Time/labTime" + i).GetComponent<CustomGUILabel>());
        }
        
        // �����¼��߼�
        btnClose.clickEvent += () =>
        {
            HideMe();
            // �ÿ�ʼ�����ʾ����
            BeginPanel.Instance.ShowMe();
        };

        // ���Դ��� �����Ƿ���ӳɹ�
        //GameDataMgr.Instance.AddRankInfo("��������",100,8432);

        HideMe();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        UpdatePanelInfo();
    }

    public void UpdatePanelInfo()
    {
        // �������а����� �������
        // ��ȡ GameDataMgr�е����а��б� �����������
        // ������
        List<RankInfo> list = GameDataMgr.Instance.rankData.list;
        // �����б�����������
        for(int i = 0; i < list.Count;i++)
        {
            // ����
            labPName[i].content.text = list[i].name;
            // ����
            labScore[i].content.text = list[i].score.ToString();
            // ʱ��
            // ������ ת���� ʱ �� ��
            int time = (int)list[i].time;
            labTime[i].content.text = "";
            if(time / 3600 > 0)
            {
                labTime[i].content.text += time / 3600 + "ʱ";
            }
            if(time % 3600 / 60 > 0 || labTime[i].content.text != "")
            {
                labTime[i].content.text += time % 3600 / 60 + "��";
            }
            labTime[i].content.text += time % 60 + "��";
        }

    }
}
