using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReward : MonoBehaviour
{
    // 用于奖励的武器列表
    public GameObject[] weaponObj;

    // 特效
    public GameObject getEff;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // 让玩家随机 切换武器
            int index =Random.Range(0, weaponObj.Length);
            // 得到撞到的玩家身上挂载的 脚本 命令它切换武器
            PlayerObj player = other.GetComponent<PlayerObj>();
            player.ChangeWeapon(weaponObj[index]);

            // 播放一个奖励特效
            GameObject eff = Instantiate(getEff,this.transform.position,this.transform.rotation);
            // 控制音效
            AudioSource audioS = eff.GetComponent<AudioSource>();
            // 根据音乐数据 设置 音效大小 和 是否播放
            // 设置音效大小
            audioS.volume = GameDataMgr.Instance.musicData.soundValue;
            // 音效是否播放
            audioS.mute = !GameDataMgr.Instance.musicData.isOpenSound;

            //碰到自己后 移除自己
            Destroy(this.gameObject);
        }
    }
}
