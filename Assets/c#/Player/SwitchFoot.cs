using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchFoot : MonoBehaviour
{
    private GameObject son1;
    private GameObject son2;
    private Player playerscript;
    public int CurrentUsingIndex;
    private Animator anim;
    [Header("���������л�")]
    [SerializeField] private GameObject animLeft;
    [SerializeField] private GameObject animRight;
    [Header("��������������")]
    [SerializeField] private GameObject animObj;
    [SerializeField] private GameObject player;

    private bool canOperate;

    private void Start()
    {
        canOperate = true;
        son1 = transform.GetChild(0).gameObject;
        son2 = transform.GetChild(1).gameObject;

        //��⼤��������Ƿ���ȷ1��
        if((son1.activeSelf && son2.activeSelf) || (!son1.activeSelf && !son2.activeSelf))
        {
            Debug.LogError("����Style��Ϊ��or��Ϊ����");
        }
        if (son1.activeSelf)
        {
            animLeft.SetActive(true);
            CurrentUsingIndex = 0; 
            son2.SetActive(false);
        }
        else
        {
            animRight.SetActive(true);
            CurrentUsingIndex = 1;  
            son2.SetActive(true);
        }
        //animObj��ȡ���������������Ӷ���
        anim = animObj.GetComponent<Animator>();
        playerscript = player.GetComponent<Player>();
    }

    void Update()
    {
        if (canOperate)
        {
            CheckSwitch();
            CheckAttack();
        }
        else
        {
            //Debug.Log("�ѽ������룬�޷��յ��ƶ��͹���ָ��");
        }


    }

    private void CheckAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            //�������������������߼����¼�֡�
            canOperate = false;
            playerscript.canOperate = false;

            anim.SetTrigger("Attack");

        }
    }


    private void CheckSwitch()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //�л�����һ�����Ӽ��
            if (CurrentUsingIndex == 0)
            {
                son1.SetActive(false);
                son2.SetActive(true);
                animRight.SetActive(true);
                animLeft.SetActive(false);

                CurrentUsingIndex = 1;
            }
            else
            {
                // CurrentUsingIndex = 1 ��ǰ�������ǵڶ�����̬���Ǿ��лص�һ����
                son2.SetActive(false);
                son1.SetActive(true);
                animRight.SetActive(false);
                animLeft.SetActive(true);

                CurrentUsingIndex = 0;
            }

        }
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddListener("Ь�ӹ�������", FinishAttack);
        EventCenter.Instance.AddListener("ֹͣ��Ϸ", StopGame);
        //Debug.Log("SwitchFootע��ֹͣ��Ϸ");

        EventCenter.Instance.AddListener("������Ϸ", Continue);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener("Ь�ӹ�������", FinishAttack);
        EventCenter.Instance.RemoveListener("ֹͣ��Ϸ", StopGame);
        EventCenter.Instance.RemoveListener("������Ϸ", Continue);


    }

    private void Continue(object i)
    {
        Debug.Log("SwitchFoot������Ϸ");
        canOperate = true;
    }

    private void StopGame(object i)
    {
        Debug.Log("��ֹͣSwitch�Ĳ�������������������������������������������������������");
        canOperate = false;
    }


    private void FinishAttack(object i)
    {
        canOperate = true;
    }




}
