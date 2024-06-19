using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    // �ƶ��ٶ�
    public float moveSpeed = 50;
    // ˭�������
    public TankBaseObj fatherObj;

    // ��Ч����
    public GameObject effObj;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    // �ͱ�����ײ����ʱ
    private void OnTriggerEnter(Collider other)
    {
        //�ӵ� ����������� �ᱬը
        //ͬ�� �ӵ������ ��ͬ��Ӫ�Ķ���ҲӦ�ñ�ը
        if (other.CompareTag("Cube") || 
            other.CompareTag("Player") && fatherObj.CompareTag("Monster") ||
            other.CompareTag("Monster") && fatherObj.CompareTag("Player"))
        {
            // �ж��Ƿ�����
            // �õ���ײ���Ķ������� �Ƿ���̹����ؽű� �����������滻ԭ��
            //ͨ������ȥ��ȡ
            TankBaseObj obj = other.GetComponent<TankBaseObj>();
            if (obj != null) 
            {
                obj.Wound(fatherObj);
            }

            // ���ӵ�����ʱ ���� ��ը��Ч
            if (effObj != null)
            {
                GameObject eff = Instantiate(effObj,this.transform.position,this.transform.rotation);
                // ���ڸ���Ч�������� ֱ�ӹ�������Ч �������ǿ����ڴ˴� ����Ч�������Ҳ������
                AudioSource audioS = eff.GetComponent<AudioSource>();
                // ������������ ���� ��Ч��С �� �Ƿ񲥷�
                // ������Ч��С
                audioS.volume = GameDataMgr.Instance.musicData.soundValue;
                // ��Ч�Ƿ񲥷�
                audioS.mute = !GameDataMgr.Instance.musicData.isOpenSound;
            }

            Destroy(this.gameObject);
        }
    }

    // ����ӵ����
    public void SetFather(TankBaseObj obj)
    {
        fatherObj = obj;
    }
}
