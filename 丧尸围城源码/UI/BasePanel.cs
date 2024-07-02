using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    //ר�����ڿ������͸���ȵ����
    private CanvasGroup canvasGroup;
    //���뵭�����ٶ�
    private float alphaSpeed = 10;

    public bool isShow = false;

    // ��������Ϻ� ��Ҫ��������
    private UnityAction hideCallBack = null;

    protected virtual void Awake()
    {
        //һ��ʼ��ȡ����Ϲ��ص� CanvasGroup���
        canvasGroup = this.GetComponent<CanvasGroup>();
        //��������������һ���ű�
        if(canvasGroup == null)
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Init();
    }

    /// <summary>
    /// ע��ؼ��¼��ķ��� ���е������ ����Ҫȥע��һЩ�ؼ��¼�
    /// ����д�ɳ��󷽷� ���������ȥʵ��
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// ��ʾ�Լ����ʱ�߼�
    /// </summary>
    public virtual void ShowMe()
    {
        canvasGroup.alpha = 0;
        isShow = true;
    }

    /// <summary>
    /// �����Լ����ʱ�߼�
    /// </summary>
    public virtual void HideMe(UnityAction callBack) // ί��
    {
        canvasGroup.alpha = 1;
        isShow = false;

        hideCallBack = callBack;
    }

    // Update is called once per frame
    void Update()
    {
        // ��������ʾ״̬ʱ ���͸���� ��Ϊ1 �ͻ᲻ͣ�ļӵ�1 �ӵ�1 ���� �ͻ�ֹͣ�仯��
        // ����
        if(isShow && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1)
                canvasGroup.alpha = 1;
        }
        // ����
        else if(!isShow && canvasGroup.alpha != 0)
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
            if(canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                //��嵭���� ִ�е�һЩ�߼�
                hideCallBack?.Invoke();
            }
        }
    }
}
