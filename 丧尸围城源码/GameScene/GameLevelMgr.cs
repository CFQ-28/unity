using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelMgr
{
    private static GameLevelMgr instance = new GameLevelMgr();
    public static GameLevelMgr Instance => instance;
    
    public PlayerObject player;

    //���еĳ��ֵ�
    private List<MonsterPoint> points = new List<MonsterPoint>();

    private int nowWaveNum = 0;
    private int maxWaveNum = 0;

    //��¼��ǰ�����ϵĹ�������
    private int nowMonsterNum = 0;

    private GameLevelMgr()
    {

    }

    //�л�����Ϸ����ʱ ��Ҫ��̬�Ĵ������
    public void InitInfo(SceneInfo info)
    {
        //��ʾ��Ϸ����
        UIManager.Instance.ShowPanel<GamePanel>();
        //��ҵĴ���
        RoleInfo roleInfo = GameDataMgr.Instance.nowSelRole;
        //���Ȼ�ȡ�������е� ��ҳ���λ��
        Transform heroPos = GameObject.Find("HeroBornPos").transform;
        //ʵ�������Ԥ����
        GameObject heroObj = GameObject.Instantiate(Resources.Load<GameObject>(roleInfo.res),
            heroPos.position,heroPos.rotation);
        //����Ҷ�����г�ʼ��
        player = heroObj.GetComponent<PlayerObject>();
        //��ʼ����ҵĻ�������
        player.InitPlayerInfo(roleInfo.atk, info.money);
        //������� ����̬�������������
        Camera.main.GetComponent<CameraMove>().SetTarget(player.transform); ;

        //��ʼ�� ���� ���������Ѫ��
        MainTowerObject.Instance.UpdateHp(info.towerHp, info.towerHp);
    }

    //���ڼ�¼���ֵ�ĵط�
    public void AddMonsterPoint(MonsterPoint point)
    {
        points.Add(point);
    }

    /// <summary>
    /// ����һ���ж��ٲ�
    /// </summary>
    /// <param name="num"></param>
    public void UpdateMaxNum(int num)
    { 
        maxWaveNum += num;
        nowWaveNum = maxWaveNum;
        UIManager.Instance.GetPanel<GamePanel>().UpdateWaveNum(nowWaveNum, maxWaveNum);
    }

    public void ChangeNowWaveNum(int num)
    {
        nowWaveNum -= num;
        UIManager.Instance.GetPanel<GamePanel>().UpdateWaveNum(nowWaveNum, maxWaveNum);
    }

    /// <summary>
    /// ����Ƿ�ʤ��
    /// </summary>
    /// <returns></returns>
    public bool CheckOver()
    {
        for (int i = 0; i < points.Count; i++)
        {
            if (!points[i].CheckOver())
                return false;
        }

        if (nowMonsterNum > 0)
            return false;

        return true;
    }

    /// <summary>
    /// �ı䵱ǰ�����Ϲ��������
    /// </summary>
    /// <param name="num"></param>
    public void ChangeMonsterNum(int num)
    {
        nowMonsterNum += num;
    }

    /// <summary>
    /// �������
    /// </summary>
    public void ClearInfo()
    {
        points.Clear();
        nowMonsterNum = nowWaveNum = maxWaveNum = 0;
        player = null;
    }
}
