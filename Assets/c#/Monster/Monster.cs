using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Monster : MonoBehaviour
{
    protected Player playerScript;
    public float speed = 2f;//speed �������������
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
        // TODO: �������Ӧ�ø�һ�£���һ��ȫ�ֵ���ͣ״̬�������ͣ��ˢ�������ǲ������ƶ��ġ�
        canMove = true;
        playerScript = FindObjectOfType<Player>();
        // ���һ��Y��ƫ���������׷����ʱ��������ƫ�ơ�
        YBias = Random.Range(0, 10);
        speed = Random.Range(1, 5);
    }

    virtual public void Update()
    {
        if (canMove)
        {
            //׷�����ǡ�ע��׷����ʱ���Խ��Z��....����Z�ᴦ��һ��
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
        Debug.Log("Monster������Ϸ");

    }

    virtual protected void OnEnable()
    {
        //Debug.Log("Monsterע��ֹͣ��Ϸ");
        EventCenter.Instance.AddListener("ֹͣ��Ϸ", StopGame);
        EventCenter.Instance.AddListener("������Ϸ", Continue);

    }


    virtual protected void OnDisable()
    {
        transform.position = new Vector2(100,100);
        EventCenter.Instance.RemoveListener("ֹͣ��Ϸ", StopGame);
        EventCenter.Instance.RemoveListener("������Ϸ", Continue);


    }

    /// <summary>
    /// ����Ƿ���Թ������ǣ�ÿ֡����
    /// </summary>
    virtual public void AttackToPlayer()
    {
        Vector2 player = new Vector2(playerScript.transform.position.x, playerScript.transform.position.y);
        if(Vector2.Distance(new Vector2(transform.position.x, transform.position.y), player) < DamageRange)
        {
            //����˺��������˺��¼���δ�������˺���Ч��
            canAttack = false;
            // ÿ�η���������ʱ���ϸ���ȴ��
            AttackTimer = AttackCoolDownTime;
            StartCoroutine(AttackCoolDown());
            Debug.Log("���8�˺�");
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
                //Debug.Log("С�ֹ�����ȴ����");
                break;
            }
            yield return null;
        }
        canAttack= true;
    }


}
