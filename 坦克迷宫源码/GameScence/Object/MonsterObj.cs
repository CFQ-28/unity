using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObj : TankBaseObj
{
    //1.Ҫ��̹�� ��������֮�� ���ص��ƶ�
    //��ǰ��Ŀ���
    private Transform targetPos;
    //����õĵ� ����ȥ����
    public Transform[] randomPos;

    //2.̹��Ҫһֱ�����Լ���Ŀ��
    public Transform lookAtTarget;

    //3.��Ŀ�굽��һ����Χ�ڹ��� ���һ��ʱ�� ����һ��Ŀ��
    //������� ��С���������ʱ �ͻ���������
    public float fireDis = 5;
    //Ϊ�˱��� ̫�� ��һ�� �������ʱ��
    public float fireOffsetTime = 1;
    private float nowTime = 0;

    //�����
    public Transform[] shootPos;
    //�ӵ�Ԥ����
    public GameObject bulletObj;

    public Texture maxHpBK;
    public Texture hpBK;

    private float showTime = 0;

    private Rect maxHpRect;
    private Rect hpRect;

    // Start is called before the first frame update
    void Start()
    {
        RandomPos();
    }

    // Update is called once per frame
    void Update()
    {
        #region �����֮�������ƶ�
        //�����Լ���Ŀ���
        this.transform.LookAt(targetPos);
        //��ͣ�����Լ����泯��λ��
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        //֪ʶ�� Vector3������һ���õ�����֮�����ķ���
        //�������С ��Ϊ������Ŀ�ĵ� �������һ����
        if (Vector3.Distance(this.transform.position, targetPos.position) < 0.05f)
        {
            RandomPos();
        }
        #endregion

        #region �����Լ���Ŀ��
        if (lookAtTarget != null)
        {
            tankHead.LookAt(lookAtTarget);

            //���Լ���Ŀ�����ľ��� С�ڵ��� ���õ� �������ʱ
            if (Vector3.Distance(this.transform.position, lookAtTarget.position) <= fireDis)
            {
                nowTime += Time.deltaTime;
                if (nowTime >= fireOffsetTime)
                {
                    Fire();
                    nowTime = 0;
                }
            }
        }
        #endregion

    }

    private void RandomPos()
    {
        if (randomPos.Length == 0) return;
        targetPos = randomPos[Random.Range(0, randomPos.Length)];
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

    public override void Dead()
    {
        base.Dead();

        //��������ʱ ��Ҫ�ӷ�
        GamePanel.Instance.AddScore(10);
    }

    // ���������Ѫ��UI�Ļ���
    private void OnGUI()
    {
        if (showTime > 0)
        {
            // ��ͣ��ʱ
            showTime -= Time.deltaTime;

            //��ͼ ��Ѫ��
            // 1.�ѹ��ﵱǰλ�� ת���� ��Ļλ��
            // ����������ṩ��API ���Խ� �������� תΪ ��Ļ����
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            // 2.��Ļλ�� ת���� GUIλ��
            screenPos.y = Screen.height - screenPos.y;

            // Ȼ���ٻ���
            // ֪ʶ�㣺GUI�е� ͼƬ����
            // ��ͼ
            maxHpRect.x = screenPos.x - 50;
            maxHpRect.y = screenPos.y - 50;
            maxHpRect.width = 100;
            maxHpRect.height = 15;
            GUI.DrawTexture(maxHpRect, maxHpBK);

            hpRect.x = screenPos.x - 50;
            hpRect.y = screenPos.y - 50;
            hpRect.width = (float)hp / maxHP * 100f;
            hpRect.height = 15;
            GUI.DrawTexture(hpRect, hpBK);
        }
    }

    public override void Wound(TankBaseObj other)
    {
        base.Wound(other);
        //������ʾѪ����ʱ��
        showTime = 3;
    }
}
