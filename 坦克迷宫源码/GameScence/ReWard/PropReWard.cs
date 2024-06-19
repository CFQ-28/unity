using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_PropType
{
    // �����Ե�4������
    Atk,
    Def,
    MaxHp,
    Hp,
}

public class PropReWard : MonoBehaviour
{
    public E_PropType type = E_PropType.Atk;

    // Ĭ����ӵ�ֵ ��ȡ���ߺ�
    public int changeValue = 2;

    // ��ȡ��Ч
    public GameObject getEff;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // �õ���Ӧ����ҽű�
            PlayerObj player = other.GetComponent<PlayerObj>();
            // ����������������
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
                    // ����Ѫ��
                    GamePanel.Instance.UpdateHP(player.maxHP,player.hp);
                    break;
                case E_PropType.Hp:
                    player.hp += changeValue;
                    // ����ֵ���ܳ�������
                    if (player.hp > player.maxHP) player.hp = player.maxHP;
                    // ����Ѫ��
                    GamePanel.Instance.UpdateHP(player.maxHP, player.hp);
                    break;
            }

            // ����һ��������Ч
            GameObject eff = Instantiate(getEff, this.transform.position, this.transform.rotation);
            // ������Ч
            AudioSource audioS = eff.GetComponent<AudioSource>();
            // ������������ ���� ��Ч��С �� �Ƿ񲥷�
            // ������Ч��С
            audioS.volume = GameDataMgr.Instance.musicData.soundValue;
            // ��Ч�Ƿ񲥷�
            audioS.mute = !GameDataMgr.Instance.musicData.isOpenSound;

            Destroy(this.gameObject);
        }
    }
}
