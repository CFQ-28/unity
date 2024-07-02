using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTowerObject : MonoBehaviour
{
    //Ѫ�����
    private int hp;
    private int maxHp;
    //�Ƿ�����
    private bool isDead;

    private static MainTowerObject instance;
    public static MainTowerObject Instance => instance;

    private void Awake()
    {
        instance = this;
    }

    //����Ѫ��
    public void UpdateHp(int hp,int maxHP)
    {
        this.hp = hp;
        this.maxHp = maxHP;
        //���½�����Ѫ������ʾ
        UIManager.Instance.GetPanel<GamePanel>().UpdateTowerHp(hp,maxHp);
    }

    //�ܵ��˺�
    public void Wound(int dmg)
    {
        if(isDead) return;
        //�ܵ��˺�
        hp -= dmg;
        //����
        if(hp <= 0)
        {
            hp = 0;
            isDead = true;
            //��Ϸ����
            GameOverPanel panel = UIManager.Instance.ShowPanel<GameOverPanel>();
            panel.InitInfo((int)(GameLevelMgr.Instance.player.money * 0.5f), false);
        }
        //����Ѫ��
        UpdateHp(hp, maxHp);
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
