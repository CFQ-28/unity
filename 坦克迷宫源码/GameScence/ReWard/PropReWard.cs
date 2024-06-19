using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_PropType
{
    // 加属性的4种类型
    Atk,
    Def,
    MaxHp,
    Hp,
}

public class PropReWard : MonoBehaviour
{
    public E_PropType type = E_PropType.Atk;

    // 默认添加的值 获取道具后
    public int changeValue = 2;

    // 获取特效
    public GameObject getEff;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 得到对应的玩家脚本
            PlayerObj player = other.GetComponent<PlayerObj>();
            // 根据类型来加属性
            switch (type)
            {
                case E_PropType.Atk:
                    player.atk += changeValue;
                    break;
                case E_PropType.Def:
                    player.def += changeValue;
                    break;
                case E_PropType.MaxHp:
                    player.maxHP += changeValue;
                    // 更新血条
                    GamePanel.Instance.UpdateHP(player.maxHP,player.hp);
                    break;
                case E_PropType.Hp:
                    player.hp += changeValue;
                    // 生命值不能超过上限
                    if (player.hp > player.maxHP) player.hp = player.maxHP;
                    // 更新血条
                    GamePanel.Instance.UpdateHP(player.maxHP, player.hp);
                    break;
            }

            // 播放一个奖励特效
            GameObject eff = Instantiate(getEff, this.transform.position, this.transform.rotation);
            // 控制音效
            AudioSource audioS = eff.GetComponent<AudioSource>();
            // 根据音乐数据 设置 音效大小 和 是否播放
            // 设置音效大小
            audioS.volume = GameDataMgr.Instance.musicData.soundValue;
            // 音效是否播放
            audioS.mute = !GameDataMgr.Instance.musicData.isOpenSound;

            Destroy(this.gameObject);
        }
    }
}
