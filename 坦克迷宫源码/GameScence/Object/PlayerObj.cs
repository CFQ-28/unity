using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerObj : TankBaseObj
{
    //��ǰװ��������
    public WeaponObj nowWeapon;

    // ����������λ��
    public Transform weaponPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 1.W S �� ���� ǰ��
        // ֪ʶ��
        // 1.Transform λ��
        // 2.Input ����������
        this.transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * moveSpeed * Time.deltaTime);

        //2.A D �� ���� ��ת
        // 1.Transform λ��
        // 2.Input ����������
        this.transform.Rotate(Input.GetAxis("Horizontal") * Vector3.up * roundSpeed * Time.deltaTime);

        //3.��������ƶ� ���� ��̨��ת
        // 1.Transform λ��
        // 2.Input �������������
        tankHead.transform.Rotate(Input.GetAxis("Mouse X") * Vector3.up * headRoundSpeed * Time.deltaTime);
        //4.����
        // Input
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    public override void Fire()
    {
        if (nowWeapon != null)
            nowWeapon.Fire();
    }

    public override void Dead()
    {
        //���� ��ִ�� ��������� ��Ϊ ���̹�� ����� �������Ӷ���
        //���ִ�и������� ������̹�˴ӳ������Ƴ� ��ô�ͻ��ӵ�ɾ�������
        //base.Dead();
        //Ӧ�ô��� ʧ���߼� ��ʾʧ����� ����
        Time.timeScale = 0;
        LosePanel.Instance.ShowMe();
    }

    public override void Wound(TankBaseObj other)
    {
        base.Wound(other);
        // ��������� Ѫ��
        GamePanel.Instance.UpdateHP(this.maxHP, this.hp);
    }

    /// <summary>
    /// �л�����
    /// </summary>
    /// <param name="obj"></param>
    public void ChangeWeapon(GameObject weapon)
    {
        // ɾ����ǰӵ�е�����
        if(nowWeapon != null)
        {
            Destroy(nowWeapon.gameObject);
            nowWeapon = null;
        }

        // �л�����
        // ���������� �������ĸ�����
        GameObject weaponObj = Instantiate(weapon,weaponPos,false);
        nowWeapon = weaponObj.GetComponent<WeaponObj>();
        //��������ӵ����
        nowWeapon.SetFather(this);
    }
}
