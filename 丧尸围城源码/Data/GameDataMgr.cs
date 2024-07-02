using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ר�������������ݵ���
/// </summary>
public class GameDataMgr
{
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance => instance;

    //��¼ѡ��Ľ�ɫ���� ����֮������Ϸ�����д���
    public RoleInfo nowSelRole;

    //��Ч�������
    public MusicData musicData;

    //����������
    public PlayerData playerData;

    //���еĽ�ɫ����
    public List<RoleInfo> roleInfoList;

    //���еĳ�������
    public List<SceneInfo> sceneInfoList;

    //���й�������
    public List<MonsterInfo> monsterInfoList;

    private GameDataMgr()
    {
        //��ʼ��һЩĬ������
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
        //��ȡ��ʼ���������
        playerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");
        //��ȡ��ɫ����
        roleInfoList = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");
        //��ȡ��������
        sceneInfoList = JsonMgr.Instance.LoadData<List<SceneInfo>>("SceneInfo");
        //��ȡ��������
        monsterInfoList = JsonMgr.Instance.LoadData<List<MonsterInfo>>("MonsterInfo");
    }

    /// <summary>
    /// �洢��Ч����
    /// </summary>
    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(musicData, "MusicData");
    }

    /// <summary>
    /// �洢�������
    /// </summary>
    public void SavePlayerData()
    {
        JsonMgr.Instance.SaveData(playerData, "PlayerData");
    }

    public void PlaySound(string resName)
    {
        GameObject musicObj = new GameObject();
        AudioSource a = musicObj.AddComponent<AudioSource>();
        a.clip = Resources.Load<AudioClip>(resName);
        a.volume = musicData.soundValue;
        a.mute = !musicData.soundOpen;
        a.Play();

        GameObject.Destroy(musicObj,1);
    }
}
