using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ϸ���ݹ����� ��һ������ģʽ����
/// </summary>

public class GameDataMgr
{
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance { get => instance; }

    // ��Ч���ݶ���
    public MusicData musicData;
    // ���а����ݶ���
    public RankList rankData;

    private GameDataMgr()
    {
        // ����ȥ��ʼ�� ��Ϸ����
        musicData = PlayerPrefsDataMgr.Instance.LoadData(typeof(MusicData),"Music") as MusicData;
        // �����һ�ν�����Ϸ û����Ч���� ��ô���е�����ҪôΪ false ҪôΪ0
        if (!musicData.notFirst)
        {
            musicData.notFirst = true;
            musicData.isOpenBK = true;
            musicData.isOpenSound = true;
            musicData.bkValue = 1;
            musicData.soundValue = 1;
            PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");
        }

        // ��ʼ�� ��ȡ ���а�����
        rankData = PlayerPrefsDataMgr.Instance.LoadData(typeof(RankList),"Rank") as RankList;

    }

    // �ṩһЩAPI���ⲿ �������ݵĸı�洢

    // �ṩһ�� �����а���������ݵķ���
    public void AddRankInfo(string name,int score,float time)
    {
        rankData.list.Add(new RankInfo(name, score, time));
        // ����
        rankData.list.Sort((a, b) => a.time < b.time ? -1 : 1);
        // ������� �Ƴ�10�����������
        // ��β����ǰ���� �Ƴ�ÿһ��
        for(int i = rankData.list.Count - 1; i >= 10; i--)
        {
            rankData.list.RemoveAt(i);
        }
        // �洢����
        PlayerPrefsDataMgr.Instance.SaveData(rankData, "Rank");
    }

    // ������رձ�������
    public void OpenOrCloseBKMusic(bool isOpen)
    {
        musicData.isOpenBK = isOpen;
        // ��������Ƴ����ϵı������ֿ���
        BKMusic.Instance.ChangeOpen(isOpen);

        // �洢�ı�������
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");
    }

    // ������ر���Ч
    public void OpenOrCloseSound(bool isOpen)
    {
        musicData.isOpenSound = isOpen;
        // �洢�ı�������
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");
    }

    public void ChangeBKValue(float value)
    {
        musicData.bkValue = value;
        // ��������Ƴ����ϵı������ִ�С
        BKMusic.Instance.ChangeValue(value);
        // �洢�ı�������
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");
    }

    public void ChangeSoundValue(float value)
    {
        musicData.soundValue = value;
        // �洢�ı�������
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");
    }
}
