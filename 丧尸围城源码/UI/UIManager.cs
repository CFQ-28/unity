using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager instance = new UIManager();
    public static UIManager Instance => instance;

    //���ڴ洢��ʾ�ŵ����� ÿ��ʾһ����� �ͻ��������ֵ�
    //�������ʱ ֱ�ӻ�ȡ�ֵ��еĶ�Ӧ��� ��������
    private Dictionary<string,BasePanel> panelDic = new Dictionary<string,BasePanel>();

    //�����е� Canvas���� ��������Ϊ���ĸ�����
    private Transform canvasTrans;

    private UIManager()
    {
        //�õ������е�Canvas����
        //ResourcesΪAssets�ļ��е�һ���ļ��� ȷ�����ļ��е�UI�ļ��д���CanvasԤ����
        GameObject canvas = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
        canvasTrans = canvas.transform;
        //���������Ƴ��ö��� ��֤�����Ϸ������ ֻ��һ�� canvas����
        GameObject.DontDestroyOnLoad(canvas);
    }

    // ��ʾ���
    public T ShowPanel<T>() where T : BasePanel
    {
        //ֻ��Ҫ��֤ ����T������ �����Ԥ�������� һ�� 
        string panelName = typeof(T).Name;

        //�ж� �ֵ��� �Ƿ��Ѿ���ʾ��������
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;

        //��ʾ��� ����������� ��̬�Ĵ���Ԥ���� ���ø�����
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        //��������� �ŵ������е� Canvas����
        panelObj.transform.SetParent(canvasTrans, false);

        //��ʾ����� ��ʾ�߼� ����Ӧ�ð�����������
        T panel = panelObj.GetComponent<T>();
        //�����ű� �洢���ֵ��� ����֮��� ��ȡ �� ����
        panelDic.Add(panelName, panel);
        //����������ʾ�߼�
        panel.ShowMe();

        return panel;
    }
 
    /// <summary>
    /// �������
    /// </summary>
    /// <typeparam name="T"> ������� </typeparam>
    /// <param name="isFade"> �Ƿ�ѡ�񵭳���Ϻ��ɾ����� Ĭ����true </param>
    public void HidePanel<T>(bool isFade = true) where T:BasePanel
    {
        //���ݷ��͵�����
        string panelName = typeof(T).Name;
        //�жϵ�ǰ��ʾ����� ��û������Ҫ���ص�
        if(panelDic.ContainsKey(panelName))
        {
            if(isFade)
            {
                //����� ������Ϲ��� ��ɾ����
                panelDic[panelName].HideMe(() =>
                {
                    //ɾ������
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    //ɾ���ֵ�����洢�����ű�
                    panelDic.Remove(panelName);
                });
            }
            else
            {
                //ɾ������
                GameObject.Destroy(panelDic[panelName].gameObject);
                //ɾ���ֵ�����洢�����ű�
                panelDic.Remove(panelName);
            }
        }
    }

    // �õ����
    public T GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;
        //���û�ж�Ӧ�����ʾ �ͷ��ؿ�
        return null;
    }
}
