using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerObject : MonoBehaviour
{
    private Animator animator;

    //1.������Եĳ�ʼ��
    //��ҹ�����
    private int atk;
    //���ӵ�е�Ǯ
    public int money;
    //��ת���ٶ�
    private float roundSpeed = 50;

    private float distance = 10f;

    //�����
    public Transform gunPoint;

    public LineRenderer line;
    public Transform start;
    public Transform end;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        line = this.GetComponent<LineRenderer>();
        // ����LineRenderer�Ķ�����
        line.positionCount = 2;
        line.startColor = Color.red;  // ���������ɫ
        line.endColor = Color.red;  // �����յ���ɫ
        line.startWidth = 0.05f;  // ���������
        line.endWidth = 0.05f;  // �����յ���
    }

    /// <summary>
    /// ��ʼ����һ�������
    /// </summary>
    /// <param name="atk"></param>
    /// <param name="money"></param>
    public void InitPlayerInfo(int atk, int money)
    {
        this.atk = atk;
        this.money = money;
        //���½�ҽ���
        UpdateMoney();
    }

    // Update is called once per frame
    void Update()
    {
        //2.�ƶ��仯 �����仯
        animator.SetFloat("VSpeed", Input.GetAxis("Vertical"));
        animator.SetFloat("HSpeed", Input.GetAxis("Horizontal"));
        //��ת
        this.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * roundSpeed * Time.deltaTime);

        //�¶�
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetLayerWeight(1, 1);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetLayerWeight(1, 0);
        }

        //����
        if (Input.GetKeyDown(KeyCode.R))
            animator.SetTrigger("Roll");

        //����
        if (Input.GetMouseButtonDown(0))
            animator.SetTrigger("Fire");
    }

    /// <summary>
    /// ר�����ڴ������������������˺�����¼�
    /// </summary>
    public void KnifeEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position + this.transform.forward
            + this.transform.up, 1, 1 << LayerMask.NameToLayer("Monster"));

        //������Ч
        GameDataMgr.Instance.PlaySound("Music/Knife");

        for (int i = 0; i < colliders.Length; i++)
        {
            //�õ���ײ���Ķ����ϵĹ���ű� ��������
            MonsterObject monster = colliders[i].gameObject.GetComponent<MonsterObject>();
            if (monster != null)
            {
                monster.Wound(this.atk);
                break;
            }
        }
    }


    public void ShootEvent()
    {
        //���߼��
        //ǰ������Ҫ�п����
        // ���߷�����ת����45��
        Vector3 direction = Quaternion.Euler(45, 0, 0) * gunPoint.forward;
        RaycastHit[] hits = Physics.RaycastAll(new Ray(gunPoint.position, direction), distance,
            1 << LayerMask.NameToLayer("Monster"));

        //������Ч
        GameDataMgr.Instance.PlaySound("Music/Gun");

        // ���λ��
        start = gunPoint;
        // ����LineRenderer����λ��
        line.SetPosition(0, start.position);

        if (hits.Length > 0)
        {
            // �����������Ŀ�꣬�յ�Ϊ���е�
            line.SetPosition(1, hits[0].point);
        }
        else
        {
            // �������δ����Ŀ�꣬�յ�Ϊ���߷����������λ��
            line.SetPosition(1, start.position + direction * distance);
        }

        // ����LineRenderer
        line.enabled = true;

        // ����Э����1������LineRenderer
        StartCoroutine(HideLineRendererAfterDelay(0.1f));

        for (int i = 0; i < hits.Length; i++)
        {
            //�õ���ײ���Ķ����ϵĹ���ű� ��������
            MonsterObject monster = hits[i].collider.gameObject.GetComponent<MonsterObject>();
            if (monster != null)
            {
                GameObject effObj = Instantiate(Resources.Load<GameObject>(GameDataMgr.Instance.nowSelRole.hitEff));
                effObj.transform.position = hits[i].point;
                effObj.transform.rotation = Quaternion.LookRotation(hits[i].normal);
                Destroy(effObj, 1);
                monster.Wound(this.atk);
                break;
            }
        }
    }

    IEnumerator HideLineRendererAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        line.enabled = false;
    }

    public void UpdateMoney()
    {
        //��ӵĸ��½����� ��ҵ�����
        UIManager.Instance.GetPanel<GamePanel>().UpdateMoney(money);
    }

    public void AddMoney(int money)
    {
        //��Ǯ
        this.money += money;
        UpdateMoney();
    }
}
