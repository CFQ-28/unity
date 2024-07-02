using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public Image imgHP;
    public Text txtHP;

    public Text txtWave;
    public Text txtMoney;

    //hp�ĳ�ʼ��
    public float hpw = 500;

    public Button btnQuit;

    //�·�������Ͽؼ��ĸ����� ��Ҫ���ڿ��� ����
    public Transform botTrans;

    //���� 3�����Ͽؼ�
    public List<TowerBtn> towerBtns = new List<TowerBtn>();

    public override void Init()
    {
        btnQuit.onClick.AddListener(() =>
        {
            //������Ϸ����
            UIManager.Instance.HidePanel<GamePanel>();
            //���ص���ʼ����
            SceneManager.LoadScene("BeginScene");

        });

        //һ��ʼ�����·��������
        botTrans.gameObject.SetActive(false);
        //�������
        Cursor.lockState = CursorLockMode.Confined;
    }

    /// <summary>
    /// ���°�ȫ��Ѫ������
    /// </summary>
    /// <param name="hp">��ǰѪ��</param>
    /// <param name="maxHP">���Ѫ��</param>
    public void UpdateTowerHp(int hp, int maxHP)
    {
        txtHP.text = hp + "/" + maxHP;
        //����Ѫ������
        (imgHP.transform as RectTransform).sizeDelta = new Vector2((float)hp / maxHP * hpw, 40);
    }

    /// <summary>
    /// ����ʣ�ನ��
    /// </summary>
    /// <param name="nowNum">��ǰ����</param>
    /// <param name="maxNum">�����</param>
    public void UpdateWaveNum(int nowNum,int maxNum)
    {
        txtWave.text = nowNum + "/" + maxNum;
    }

    /// <summary>
    /// ���½������
    /// </summary>
    /// <param name="money">��ǰ��õĽ��</param>
    public void UpdateMoney(int money)
    {
        txtMoney.text = money.ToString();
    }
}
