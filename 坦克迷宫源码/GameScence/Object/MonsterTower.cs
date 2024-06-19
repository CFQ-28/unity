using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTower : TankBaseObj
{
    // �Զ���ת��ʵ��

    //�������
    // Ӧ����һ�� ���ʱ��
    public float fireOffsetTime = 1;
    //��¼�ۼ�ʱ�� ���� ��������ж�
    private float nowTime = 0;
    //����λ��
    public Transform[] shootPos;
    // �ӵ�Ԥ���� ����
    public GameObject bulletObj;

    // Update is called once per frame
    void Update()
    {
        nowTime += Time.deltaTime;
        if(nowTime >= fireOffsetTime)
        {
            Fire();
            nowTime = 0;
        }
    }

    public override void Fire()
    {
        for (int i = 0; i < shootPos.Length; i++)
        {
            // ʵ���������ӵ�
            GameObject obj = Instantiate(bulletObj, shootPos[i].position, shootPos[i].rotation);
            // �����������ӵ���ӵ���� ����֮�� �������Լ���
            BulletObj bullet = obj.GetComponent<BulletObj>();
            bullet.SetFather(this);
        }
    }

    public override void Wound(TankBaseObj other)
    {
        // ������ʲô���ݶ���д
        //Ŀ�ľ��� ����� �̶�������̹�� �����Բ����˺� ��Զ��������
    }
}
