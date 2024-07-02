using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterObject : MonoBehaviour
{
    //�������
    private Animator animator;
    //λ����� Ѱ·���
    private NavMeshAgent agent;
    //һЩ���������
    private MonsterInfo monsterInfo;

    //��ǰѪ��
    private int hp;
    //�����Ƿ�����
    public bool isDead = false;

    //��һ�ι�����ʱ��
    private float frontTime = 0;

    // Start is called before the first frame update
    void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
    }

    //��ʼ��
    public void InitInfo(MonsterInfo info)
    {
        monsterInfo = info;
        //״̬������
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(info.animator);
        //Ҫ��ĵ�ǰѪ��
        hp = info.hp;
        //�ٶȺͼ��ٶȸ�ֵ ֮���Ը�ֵһ�� ��ϣ��û�� ���Եļ����˶� ����һ�������˶�
        agent.speed = agent.acceleration = info.moveSpeed;
        //��ת�ٶ�
        agent.angularSpeed = info.roundSpeed;
    }

    //����
    public void Wound(int dmg)
    {
        hp -= dmg;
        //�������˶���
        animator.SetTrigger("Wound");

        if(hp <= 0)
        {
            Dead();
        }
        else
        {
            //������Ч
            GameDataMgr.Instance.PlaySound("Music/Wound");
        }
    }

    //����
    public void Dead()
    {
        isDead = true;
        //ֹͣ�ƶ�
        agent.isStopped = true;
        //������������
        animator.SetBool("Dead", true);
        //������Ч
        GameDataMgr.Instance.PlaySound("Music/dead");
        //��Ǯ

    }

    //��������������Ϻ� ����õ��¼�����
    public void DeadEvent()
    {
        //��������������Ϻ��Ƴ�����
        GameLevelMgr.Instance.ChangeMonsterNum(-1);
        Destroy(gameObject);

        if(GameLevelMgr.Instance.CheckOver())
        {
            GameOverPanel panel = UIManager.Instance.ShowPanel<GameOverPanel>();
            panel.InitInfo(GameLevelMgr.Instance.player.money, true);
        }
    }

    //���������ƶ�
    public void BornOver()
    {
        //���������� ���ù��ﳯĿ����ƶ�����Ѱ·���
        agent.SetDestination(MainTowerObject.Instance.transform.position);

        //�����ƶ�����
        animator.SetBool("Run", true);
    }

    // Update is called once per frame
    void Update()
    {
        //���ʲôʱ��ͣ��������
        if (isDead) return;
        //�����ٶ� ��������������ʲô
        animator.SetBool("Run",agent.velocity != Vector3.zero);
        //����Ŀ���ﵽ�ƶ�����ʱ �͹���
        if(Vector3.Distance(this.transform.position,MainTowerObject.Instance.transform.position)< 5
            && Time.time - frontTime >= monsterInfo.atkOffset)
        {
            //��¼��ι���ʱ��ʱ��
            frontTime = Time.time;
            animator.SetTrigger("Atk");
        }
    }

    //�˺����
    public void AtkEvent()
    {
        //��Χ��� �����˺��ж�
        Collider[] colliders =  Physics.OverlapSphere(this.transform.position + this.transform.forward
            + this.transform.up, 1, 1 << LayerMask.NameToLayer("MainTower"));

        //������Ч
        GameDataMgr.Instance.PlaySound("Music/Eat");

        for (int i = 0; i < colliders.Length; i++)
        {
            if(MainTowerObject.Instance.gameObject == colliders[i].gameObject)
            {
                //�ܵ��˺�
                MainTowerObject.Instance.Wound(monsterInfo.atk);
            }
        }
    }
}
