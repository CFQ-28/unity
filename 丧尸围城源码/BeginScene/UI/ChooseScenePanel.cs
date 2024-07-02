using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseScenePanel : BasePanel
{
    //�ĸ���ť
    public Button btnLeft;
    public Button btnRight;
    public Button btnStart;
    public Button btnBack;

    //�����ı���ͼƬ����
    public Text txtInfo;
    public Image imgScene;

    //��¼��ǰ��������
    private int nowIndex;
    //��¼��ǰѡ�������
    private SceneInfo nowSceneInfo;

    public override void Init()
    {
        btnLeft.onClick.AddListener(() =>
        {
            --nowIndex;
            if (nowIndex < 0)
                nowIndex = GameDataMgr.Instance.sceneInfoList.Count - 1;
            ChangeScene();
        });

        btnRight.onClick.AddListener(() =>
        {
            ++nowIndex;
            if (nowIndex >= GameDataMgr.Instance.sceneInfoList.Count)
                nowIndex = 0;
            ChangeScene();
        });

        btnStart.onClick.AddListener(() =>
        {
            //���ص�ǰ���
            UIManager.Instance.HidePanel<ChooseScenePanel>();
            //�л����� �첽����
            AsyncOperation ao = SceneManager.LoadSceneAsync(nowSceneInfo.sceneName);
            //���йؿ���ʼ��
            ao.completed += (obj) =>
            {
                GameLevelMgr.Instance.InitInfo(nowSceneInfo);
            };
        });

        btnBack.onClick.AddListener(() =>
        {
            //�����Լ�
            UIManager.Instance.HidePanel<ChooseScenePanel>();
            //�����ϼ���� ��ʾѡ�����
            UIManager.Instance.ShowPanel<ChooseHeroPanel>();
        });

        //һ����� ��ʼ��ʱ Ӧ�ð����ݸ�����
        ChangeScene();
    }

    /// <summary>
    /// �л�������ʾ�ĳ�����Ϣ
    /// </summary>
    public void ChangeScene()
    {
        nowSceneInfo = GameDataMgr.Instance.sceneInfoList[nowIndex];
        //����ͼƬ����ʾ��������Ϣ
        imgScene.sprite = Resources.Load<Sprite>(nowSceneInfo.imgRes);
        txtInfo.text = "���ƣ� \n\t" + nowSceneInfo.name + "\n" + "������\n\t" + nowSceneInfo.tips;
    }
}
