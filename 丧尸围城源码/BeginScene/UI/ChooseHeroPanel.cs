using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChooseHeroPanel : BasePanel
{
    //���Ҽ�
    public Button btnLeft;
    public Button btnRight;

    //����ť
    public Button btnUnLock;
    public Text txtUnLock;

    //��ʼ�ͷ���
    public Button btnStart;
    public Button btnBack;

    //���Ͻ�ӵ�е�Ǯ
    public Text txtMoney;

    //��ɫ��Ϣ
    public Text txtName;

    //Ӣ��Ԥ������Ҫ�����ڵ�λ��
    private Transform heroPos;

    //��ǰ��������ʾ�Ķ���
    private GameObject heroObj;
    //��ǰʹ�õ�����
    private RoleInfo nowRoleData;
    //��ǰʹ�����ݵ�����
    private int nowIndex;

    public override void Init()
    {
        //�ҵ������� ���ö���Ԥ�����λ��
        heroPos = GameObject.Find("HeroPos").transform;

        //�������Ͻ����ӵ�е�Ǯ
        txtMoney.text = GameDataMgr.Instance.playerData.haveMoney.ToString();

        btnLeft.onClick.AddListener(() =>
        {
            --nowIndex;
            if (nowIndex < 0)
                nowIndex = GameDataMgr.Instance.roleInfoList.Count - 1;
            //ģ�͸���
            ChangeHero();
        });

        btnRight.onClick.AddListener(() =>
        {
            ++nowIndex;
            if (nowIndex >= GameDataMgr.Instance.roleInfoList.Count)
                nowIndex = 0;
            //ģ�͸���
            ChangeHero();
        });

        btnUnLock.onClick.AddListener(() =>
        {
            //���������ť���߼�
            PlayerData data = GameDataMgr.Instance.playerData;
            //����Ǯʱ
            if(data.haveMoney >= nowRoleData.lockMoney)
            {
                //�����߼�
                //��ȥ����
                data.haveMoney -= nowRoleData.lockMoney;
                //���½�����ʾ
                txtMoney.text = data.haveMoney.ToString();
                //��¼�����id
                data.buyHero.Add(nowRoleData.id);
                //��������
                GameDataMgr.Instance.SavePlayerData();

                //���½�����ť
                UpdateLockBtn();

                //��ʾ��� ��ʾ����ɹ�
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("����ɹ�");
            }
            else
            {
                //��ʾ��� ��ʾ ��Ǯ����
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("��Ǯ����");
            }
        });

        btnStart.onClick.AddListener(() =>
        {
            //��һ ��¼��ǰѡ��Ľ�ɫ
            GameDataMgr.Instance.nowSelRole = nowRoleData;

            //�ڶ� �����Լ� ��ʾ����ѡ�����
            UIManager.Instance.HidePanel<ChooseHeroPanel>();
            UIManager.Instance.ShowPanel<ChooseScenePanel>();
        });

        btnBack.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<ChooseHeroPanel>();
            //�������ת��ȥ������ʾ��ʼ����
            Camera.main.GetComponent<CameraAnimator>().TurnRight(() =>
            {
                UIManager.Instance.ShowPanel<BeginPanel>();
            });
        });

        //����ģ����ʾ
        ChangeHero();
    }

    /// <summary>
    /// ���³�����Ҫ��ʾ��ģ��
    /// </summary>
    private void ChangeHero()
    {
        if (heroObj != null)
        {
            Destroy(heroObj);
            heroObj = null;
        }

        //ȡ�����ݵ�һ�� ��������ֵ
        nowRoleData = GameDataMgr.Instance.roleInfoList[nowIndex];
        //ʵ�������� ����¼���� �����´��л�ʱ ɾ��
        heroObj = Instantiate(Resources.Load<GameObject>(nowRoleData.res), heroPos.position, heroPos.rotation);
        //���ڶ����Ϲ�����PlerObject ���ڿ�ʼ���� ����Ҫ
        Destroy(heroObj.GetComponent<PlayerObject>());

        txtName.text = nowRoleData.tips;

        //���ݽ���������� �������Ƿ���ʾ������ť
        UpdateLockBtn();
    }

    /// <summary>
    /// ���½�����ť��ʾ���
    /// </summary>
    private void UpdateLockBtn()
    {
        //����ý�ɫ ��Ҫ���� ����û�н����Ļ� ��Ӧ����ʾ������ť �������ؿ�ʼ��ť
        if (nowRoleData.lockMoney > 0 && !GameDataMgr.Instance.playerData.buyHero.Contains(nowRoleData.id))
        {
            //���½�����ť��ʾ �����������Ǯ
            btnUnLock.gameObject.SetActive(true);
            txtUnLock.text = "��" + nowRoleData.lockMoney;
            //���ؿ�ʼ��ť ��Ϊ�ý�ɫû�н���
            btnStart.gameObject.SetActive(false);
        }
        else
        {
            btnUnLock.gameObject.SetActive(false);
            btnStart.gameObject.SetActive(true);
        }
    }

    public override void HideMe(UnityAction callBack)
    {
        base.HideMe(callBack);
        //ÿ�������Լ�ʱ Ҫ�ѵ�ǰ��ʾ��3dģ�ͽ�ɫ ɾ��
        if (heroObj != null)
        {
            DestroyImmediate(heroObj);
            heroObj = null;
        }
    }
}
