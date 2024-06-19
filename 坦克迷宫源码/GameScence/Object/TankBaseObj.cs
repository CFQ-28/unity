using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TankBaseObj : MonoBehaviour
{
    // ������ ������ ���Ѫ��HP���
    public int atk;
    public int def;
    public int maxHP;
    public int hp;

    // ����̹�� ������̨���
    public Transform tankHead;

    //�ƶ���ת�ٶ����
    public float moveSpeed = 10;
    public float roundSpeed = 100;
    public float headRoundSpeed = 100;

    // ������Ч ������ӦԤ���� ������ʱ�� ��̬�������� ����λ�ü���
    public GameObject deadEff;

    /// <summary>
    /// ������󷽷� ������д������Ϊ����
    /// </summary>
    public abstract void Fire();

    /// <summary>
    /// �ұ����˹��� ������Լ�����
    /// </summary>
    /// <param name="other"></param>
    public virtual void Wound(TankBaseObj other)
    {
        int dmg = other.atk - this.def;
        if (dmg <= 0) return;
        // ����˺�����0 Ӧ�ü�Ѫ
        this.hp -= dmg;
        // �ж� ���Ѫ�� <= 0 Ӧ������
        if (this.hp <= 0)
        {
            this.hp = 0;
            this.Dead();
        }
    }

    /// <summary>
    /// ������Ϊ
    /// </summary>
    public virtual void Dead()
    {
        // �������� �����ڳ������Ƴ��ö���
        Destroy(this.gameObject);
        //������ʱ�� ��������̹�� ���� ��Ӧ�ò���һ���������Ч
        if(deadEff != null ) 
        {
            //����ʵ��������ʱ ˳�� ��λ�úͽǶ� ��һ��������
            GameObject effObj = Instantiate(deadEff,this.transform.position,this.transform.rotation);
            // ���ڸ���Ч�������� ֱ�ӹ�������Ч �������ǿ����ڴ˴� ����Ч�������Ҳ������
            AudioSource audioSource = effObj.GetComponent<AudioSource>();
            // ������������ ���� ��Ч��С �� �Ƿ񲥷�
            // ������Ч��С
            audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
            // ��Ч�Ƿ񲥷�
            audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;
            // ����û�й�ѡ play on Awake
            audioSource.Play();
        }
    }
}
