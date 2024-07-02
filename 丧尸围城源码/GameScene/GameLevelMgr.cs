using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelMgr
{
    private static GameLevelMgr instance = new GameLevelMgr();
    public static GameLevelMgr Instance => instance;
    
    public PlayerObject player;

    //所有的出怪点
    private List<MonsterPoint> points = new List<MonsterPoint>();

    private int nowWaveNum = 0;
    private int maxWaveNum = 0;

    //记录当前场景上的怪物数量
    private int nowMonsterNum = 0;

    private GameLevelMgr()
    {

    }

    //切换到游戏场景时 需要动态的创建玩家
    public void InitInfo(SceneInfo info)
    {
        //显示游戏界面
        UIManager.Instance.ShowPanel<GamePanel>();
        //玩家的创建
        RoleInfo roleInfo = GameDataMgr.Instance.nowSelRole;
        //首先获取到场景中的 玩家出生位置
        Transform heroPos = GameObject.Find("HeroBornPos").transform;
        //实例化玩家预设体
        GameObject heroObj = GameObject.Instantiate(Resources.Load<GameObject>(roleInfo.res),
            heroPos.position,heroPos.rotation);
        //对玩家对象进行初始化
        player = heroObj.GetComponent<PlayerObject>();
        //初始化玩家的基础属性
        player.InitPlayerInfo(roleInfo.atk, info.money);
        //让摄像机 看向动态创建出来的玩家
        Camera.main.GetComponent<CameraMove>().SetTarget(player.transform); ;

        //初始化 中央 保护区域的血量
        MainTowerObject.Instance.UpdateHp(info.towerHp, info.towerHp);
    }

    //用于记录出怪点的地方
    public void AddMonsterPoint(MonsterPoint point)
    {
        points.Add(point);
    }

    /// <summary>
    /// 更新一共有多少波
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
    /// 检测是否胜利
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
    /// 改变当前场景上怪物的数量
    /// </summary>
    /// <param name="num"></param>
    public void ChangeMonsterNum(int num)
    {
        nowMonsterNum += num;
    }

    /// <summary>
    /// 清空数据
    /// </summary>
    public void ClearInfo()
    {
        points.Clear();
        nowMonsterNum = nowWaveNum = maxWaveNum = 0;
        player = null;
    }
}
