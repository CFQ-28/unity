using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : BasePanel<LosePanel>
{
    public CustomGUIButton btnBack;
    public CustomGUIButton btnGoOn;

    // Start is called before the first frame update
    void Start()
    {
        btnBack.clickEvent += () =>
        {
            //ȡ����ͣ
            Time.timeScale = 1;
            //�л�����
            SceneManager.LoadScene("BeginScene");
        };
        btnGoOn.clickEvent += () =>
        {
            //ȡ����ͣ
            Time.timeScale = 1;

            //�ٴ��л��� ��Ϸ���� �Ϳ��� �ﵽ�����������¼��� ��ͷ��ʼ�� Ŀ��
            SceneManager.LoadScene("GameScene");
        };

        HideMe();
    }
}
