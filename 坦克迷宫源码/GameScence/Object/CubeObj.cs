using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObj : MonoBehaviour
{
    public GameObject[] rewardObjects;

    //������Ч Ԥ����
    public GameObject deadEff;

    private void OnTriggerEnter(Collider other)
    {
        //1.���Լ����ӵ� Ӧ������
        // ��һ�� ����������д ֻ��Ҫ�����ӵ�tag �ĳ�Cube
        // ֮ǰ�ӵ��߼����о��Ѿ������ ���� cube�����Լ����߼�

        //2.���Լ� Ӧ�ô��� ��������������߼�

        //���һ���� ����ȡ����
        int rangeInt = Random.Range(0, 100);
        // 50%�ļ��� ����һ������
        if(rangeInt < 50)
        {
            //�������һ�� ����Ԥ���� �ڵ�ǰλ��
            rangeInt = Random.Range(0,rewardObjects.Length);
            //�ŵ���ǰ�������ڵ�λ�� ����
            Instantiate(rewardObjects[rangeInt],this.transform.position,this.transform.rotation);
        }

        // ����һ��������Ч
        GameObject eff = Instantiate(deadEff, this.transform.position, this.transform.rotation);
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
