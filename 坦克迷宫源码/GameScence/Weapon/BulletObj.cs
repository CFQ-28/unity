using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    // 移动速度
    public float moveSpeed = 50;
    // 谁发射的我
    public TankBaseObj fatherObj;

    // 特效对象
    public GameObject effObj;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    // 和别人碰撞触发时
    private void OnTriggerEnter(Collider other)
    {
        //子弹 射击到立方体 会爆炸
        //同样 子弹射击到 不同阵营的对象也应该爆炸
        if (other.CompareTag("Cube") || 
            other.CompareTag("Player") && fatherObj.CompareTag("Monster") ||
            other.CompareTag("Monster") && fatherObj.CompareTag("Player"))
        {
            // 判断是否受伤
            // 得到碰撞到的对象身上 是否有坦克相关脚本 我们用里氏替换原则
            //通过父类去获取
            TankBaseObj obj = other.GetComponent<TankBaseObj>();
            if (obj != null) 
            {
                obj.Wound(fatherObj);
            }

            // 当子弹销毁时 发生 爆炸特效
            if (effObj != null)
            {
                GameObject eff = Instantiate(effObj,this.transform.position,this.transform.rotation);
                // 由于该特效对象身上 直接关联了音效 所以我们可以在此处 把音效播放相关也控制了
                AudioSource audioS = eff.GetComponent<AudioSource>();
                // 根据音乐数据 设置 音效大小 和 是否播放
                // 设置音效大小
                audioS.volume = GameDataMgr.Instance.musicData.soundValue;
                // 音效是否播放
                audioS.mute = !GameDataMgr.Instance.musicData.isOpenSound;
            }

            Destroy(this.gameObject);
        }
    }

    // 设置拥有者
    public void SetFather(TankBaseObj obj)
    {
        fatherObj = obj;
    }
}
