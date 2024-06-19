using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : BasePanel<RankPanel>
{
    // 关联相关的控件

    public CustomGUIButton btnClose;
    // 控件较多
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
        
        // 处理事件逻辑
        btnClose.clickEvent += () =>
        {
            HideMe();
            // 让开始面板显示出来
            BeginPanel.Instance.ShowMe();
        };

        // 测试代码 数据是否添加成功
        //GameDataMgr.Instance.AddRankInfo("测试数据",100,8432);

        HideMe();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        UpdatePanelInfo();
    }

    public void UpdatePanelInfo()
    {
        // 根据排行榜数据 更新面板
        // 获取 GameDataMgr中的排行榜列表 用于在这更新
        // 得数据
        List<RankInfo> list = GameDataMgr.Instance.rankData.list;
        // 根据列表更新面板数据
        for(int i = 0; i < list.Count;i++)
        {
            // 名字
            labPName[i].content.text = list[i].name;
            // 分数
            labScore[i].content.text = list[i].score.ToString();
            // 时间
            // 把秒数 转换成 时 分 秒
            int time = (int)list[i].time;
            labTime[i].content.text = "";
            if(time / 3600 > 0)
            {
                labTime[i].content.text += time / 3600 + "时";
            }
            if(time % 3600 / 60 > 0 || labTime[i].content.text != "")
            {
                labTime[i].content.text += time % 3600 / 60 + "分";
            }
            labTime[i].content.text += time % 60 + "秒";
        }

    }
}
