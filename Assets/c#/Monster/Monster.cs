using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Monster : MonoBehaviour
{
    protected Player playerScript;
    public float speed = 2f;//speed 在下面做随机了
    public bool canMove;
    public bool canAttack;
    protected int YBias;
    public float damage = 8f;
    public float DamageRange = 3f;
    private float AttackTimer;
    public float AttackCoolDownTime = 3f;

    virtual public void Start()
    {
        AttackTimer = 0f;
        canAttack = true;
        // TODO: 这个后面应该改一下，看一个全局的暂停状态，如果暂停了刷出来的是不允许移动的。
        canMove = true;
        playerScript = FindObjectOfType<Player>();
        // 获得一个Y的偏移随机数，追击的时候加上这个偏移。
        YBias = Random.Range(0, 10);
        speed = Random.Range(1, 5);
    }

    virtual public void Update()
    {
        if (canMove)
        {
            //追击主角。注意追击的时候跨越了Z轴....所以Z轴处理一下
            transform.position = Vector2.MoveTowards(transform.position, playerScript.transform.position + new Vector3(0, YBias, 0), speed * Time.deltaTime);
            if (playerScript.canBeDamaged && canAttack)
            {
                AttackToPlayer();

            }
        }
    }

    private void StopGame(object i)
    {
        canMove = false;
    }

    private void Continue(object i) { 
        canMove = true;
        Debug.Log("Monster继续游戏");

    }

    virtual protected void OnEnable()
    {
        //Debug.Log("Monster注册停止游戏");
        EventCenter.Instance.AddListener("停止游戏", StopGame);
        EventCenter.Instance.AddListener("继续游戏", Continue);

    }


    virtual protected void OnDisable()
    {
        transform.position = new Vector2(100,100);
        EventCenter.Instance.RemoveListener("停止游戏", StopGame);
        EventCenter.Instance.RemoveListener("继续游戏", Continue);


    }

    /// <summary>
    /// 检测是否可以攻击主角，每帧进行
    /// </summary>
    virtual public void AttackToPlayer()
    {
        Vector2 player = new Vector2(playerScript.transform.position.x, playerScript.transform.position.y);
        if(Vector2.Distance(new Vector2(transform.position.x, transform.position.y), player) < DamageRange)
        {
            //造成伤害，触发伤害事件，未来触发伤害音效。
            canAttack = false;
            // 每次发动攻击的时候都上个冷却。
            AttackTimer = AttackCoolDownTime;
            StartCoroutine(AttackCoolDown());
            Debug.Log("造成8伤害");
            playerScript.TakeDamage(damage);

        }
    }

    virtual protected IEnumerator AttackCoolDown()
    {
        while (true)
        {
            AttackTimer -= Time.deltaTime;
            if(AttackTimer <= 0)
            {
                //Debug.Log("小怪攻击冷却结束");
                break;
            }
            yield return null;
        }
        canAttack= true;
    }


}
