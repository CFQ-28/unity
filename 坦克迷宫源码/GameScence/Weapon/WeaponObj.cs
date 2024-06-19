using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObj : MonoBehaviour
{
    // �ӵ�
    public GameObject bullet;

    // �ⲿ���������м�������λ��
    public Transform[] shootPos;

    // ������ӵ����
    public TankBaseObj fatherObj;

    // ����ӵ����
    public void SetFather(TankBaseObj obj)
    {
        fatherObj = obj;
    }

    public void Fire()
    {
        // ����λ�� ������Ӧ���ӵ� 
        for (int i = 0; i < shootPos.Length; i++)
        {
            // �����ӵ�Ԥ����
            GameObject obj = Instantiate(bullet, shootPos[i].position, shootPos[i].rotation);
            //�����ӵ���Ӧ����
            BulletObj bulletObj = obj.GetComponent<BulletObj>();
            bulletObj.SetFather(fatherObj);
        }
    }
}
