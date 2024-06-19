using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReward : MonoBehaviour
{
    // ���ڽ����������б�
    public GameObject[] weaponObj;

    // ��Ч
    public GameObject getEff;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // �������� �л�����
            int index =Random.Range(0, weaponObj.Length);
            // �õ�ײ����������Ϲ��ص� �ű� �������л�����
            PlayerObj player = other.GetComponent<PlayerObj>();
            player.ChangeWeapon(weaponObj[index]);

            // ����һ��������Ч
            GameObject eff = Instantiate(getEff,this.transform.position,this.transform.rotation);
            // ������Ч
            AudioSource audioS = eff.GetComponent<AudioSource>();
            // ������������ ���� ��Ч��С �� �Ƿ񲥷�
            // ������Ч��С
            audioS.volume = GameDataMgr.Instance.musicData.soundValue;
            // ��Ч�Ƿ񲥷�
            audioS.mute = !GameDataMgr.Instance.musicData.isOpenSound;

            //�����Լ��� �Ƴ��Լ�
            Destroy(this.gameObject);
        }
    }
}
