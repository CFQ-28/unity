using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerObject : MonoBehaviour
{
    private Animator animator;

    //1.玩家属性的初始化
    //玩家攻击力
    private int atk;
    //玩家拥有的钱
    public int money;
    //旋转的速度
    private float roundSpeed = 50;

    private float distance = 10f;

    //开火点
    public Transform gunPoint;

    public LineRenderer line;
    public Transform start;
    public Transform end;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        line = this.GetComponent<LineRenderer>();
        // 设置LineRenderer的顶点数
        line.positionCount = 2;
        line.startColor = Color.red;  // 设置起点颜色
        line.endColor = Color.red;  // 设置终点颜色
        line.startWidth = 0.05f;  // 设置起点宽度
        line.endWidth = 0.05f;  // 设置终点宽度
    }

    /// <summary>
    /// 初始化玩家基础属性
    /// </summary>
    /// <param name="atk"></param>
    /// <param name="money"></param>
    public void InitPlayerInfo(int atk, int money)
    {
        this.atk = atk;
        this.money = money;
        //更新金币界面
        UpdateMoney();
    }

    // Update is called once per frame
    void Update()
    {
        //2.移动变化 动作变化
        animator.SetFloat("VSpeed", Input.GetAxis("Vertical"));
        animator.SetFloat("HSpeed", Input.GetAxis("Horizontal"));
        //旋转
        this.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * roundSpeed * Time.deltaTime);

        //下蹲
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetLayerWeight(1, 1);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetLayerWeight(1, 0);
        }

        //滚动
        if (Input.GetKeyDown(KeyCode.R))
            animator.SetTrigger("Roll");

        //开火
        if (Input.GetMouseButtonDown(0))
            animator.SetTrigger("Fire");
    }

    /// <summary>
    /// 专门用于处理刀武器攻击动作的伤害检测事件
    /// </summary>
    public void KnifeEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position + this.transform.forward
            + this.transform.up, 1, 1 << LayerMask.NameToLayer("Monster"));

        //播放音效
        GameDataMgr.Instance.PlaySound("Music/Knife");

        for (int i = 0; i < colliders.Length; i++)
        {
            //得到碰撞到的对象上的怪物脚本 让其受伤
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
        //射线检测
        //前提是需要有开火点
        // 射线方向，旋转向下45度
        Vector3 direction = Quaternion.Euler(45, 0, 0) * gunPoint.forward;
        RaycastHit[] hits = Physics.RaycastAll(new Ray(gunPoint.position, direction), distance,
            1 << LayerMask.NameToLayer("Monster"));

        //播放音效
        GameDataMgr.Instance.PlaySound("Music/Gun");

        // 起点位置
        start = gunPoint;
        // 更新LineRenderer顶点位置
        line.SetPosition(0, start.position);

        if (hits.Length > 0)
        {
            // 如果射线命中目标，终点为命中点
            line.SetPosition(1, hits[0].point);
        }
        else
        {
            // 如果射线未命中目标，终点为射线方向的最大距离位置
            line.SetPosition(1, start.position + direction * distance);
        }

        // 启用LineRenderer
        line.enabled = true;

        // 启动协程在1秒后禁用LineRenderer
        StartCoroutine(HideLineRendererAfterDelay(0.1f));

        for (int i = 0; i < hits.Length; i++)
        {
            //得到碰撞到的对象上的怪物脚本 让其受伤
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
        //间接的更新界面上 金币的数量
        UIManager.Instance.GetPanel<GamePanel>().UpdateMoney(money);
    }

    public void AddMoney(int money)
    {
        //加钱
        this.money += money;
        UpdateMoney();
    }
}
