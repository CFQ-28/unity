using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoint : MonoBehaviour
{
    //�����ж��ٲ�
    public int maxWave;
    //ÿ�������ж���ֻ
    public int monsterNumOneWave;
    //���ڼ�¼ ��ǰ���Ĺ��ﻹ�ж���ֻû�д���
    private int nowNum;

    //����ID �����ж�� �����Ϳ������������ͬ�Ĺ���
    public List<int> monsterIDs;
    //���ڼ�¼ ��ǰ�� Ҫ����ʲôID�Ĺ���
    private int nowID;

    //��ֻ���ﴴ�����ʱ��
    public float creatOffsetTime;

    //���벨֮��ļ��ʱ��
    public float delayTime;

    //��һ�����ﴴ���ļ��ʱ��
    public float firstDelayTime;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("CreateWave", firstDelayTime);
        //��¼���ֵ�
        GameLevelMgr.Instance.AddMonsterPoint(this);
        //���������
        GameLevelMgr.Instance.UpdateMaxNum(maxWave);
    }

    /// <summary>
    /// ��ʼ����һ���Ĺ���
    /// </summary>
    public void CreateWave()
    {
        //�õ���ǰ�������ID��ʲô
        nowID = monsterIDs[Random.Range(0, monsterIDs.Count)];
        //��ǰ�������ж���ֻ
        nowNum = monsterNumOneWave;
        //��������
        CreateMonster();
        //���ٲ���
        --maxWave;
        //֪ͨ�ؿ������� ����һ����
        GameLevelMgr.Instance.ChangeNowWaveNum(1);
    }

    private void CreateMonster()
    {
        //ֱ�Ӵ�������
        //ȡ����������
        MonsterInfo info = GameDataMgr.Instance.monsterInfoList[nowID - 1];

        //��������Ԥ����
        GameObject obj = Instantiate(Resources.Load<GameObject>(info.res), this.transform.position, Quaternion.identity);
        //Ϊ�������Ĺ���Ԥ���� ��ӹ���ű� ���г�ʼ��
        MonsterObject monsterObj = obj.AddComponent<MonsterObject>();
        monsterObj.InitInfo(info);

        //֪ͨ������ ����������1
        GameLevelMgr.Instance.ChangeMonsterNum(1);

        //������һֻ����� ��ȥҪ�����Ĺ�������1
        --nowNum;
        if(nowNum == 0)
        {
            if (maxWave > 0)
                Invoke("CreateWave", delayTime);
        }
        else
        {
            Invoke("CreateMonster",creatOffsetTime);
        }
    }

    /// <summary>
    /// ���ֵ��Ƿ����
    /// </summary>
    /// <returns></returns>
    public bool CheckOver()
    {
        return nowNum == 0 && maxWave == 0;
    }

}
