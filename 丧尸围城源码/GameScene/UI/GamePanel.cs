using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public Image imgHP;
    public Text txtHP;

    public Text txtWave;
    public Text txtMoney;

    //hp的初始宽
    public float hpw = 500;

    public Button btnQuit;

    //下方造塔组合控件的父对象 主要用于控制 显隐
    public Transform botTrans;

    //管理 3个复合控件
    public List<TowerBtn> towerBtns = new List<TowerBtn>();

    public override void Init()
    {
        btnQuit.onClick.AddListener(() =>
        {
            //隐藏游戏界面
            UIManager.Instance.HidePanel<GamePanel>();
            //返回到开始界面
            SceneManager.LoadScene("BeginScene");

        });

        //一开始隐藏下方造塔相关
        botTrans.gameObject.SetActive(false);
        //锁定鼠标
        Cursor.lockState = CursorLockMode.Confined;
    }

    /// <summary>
    /// 更新安全区血量函数
    /// </summary>
    /// <param name="hp">当前血量</param>
    /// <param name="maxHP">最大血量</param>
    public void UpdateTowerHp(int hp, int maxHP)
    {
        txtHP.text = hp + "/" + maxHP;
        //更新血条长度
        (imgHP.transform as RectTransform).sizeDelta = new Vector2((float)hp / maxHP * hpw, 40);
    }

    /// <summary>
    /// 更新剩余波数
    /// </summary>
    /// <param name="nowNum">当前波数</param>
    /// <param name="maxNum">最大波数</param>
    public void UpdateWaveNum(int nowNum,int maxNum)
    {
        txtWave.text = nowNum + "/" + maxNum;
    }

    /// <summary>
    /// 更新金币数量
    /// </summary>
    /// <param name="money">当前获得的金币</param>
    public void UpdateMoney(int money)
    {
        txtMoney.text = money.ToString();
    }
}
