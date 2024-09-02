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
    [Header("动画左右切换")]
    [SerializeField] private GameObject animLeft;
    [SerializeField] private GameObject animRight;
    [Header("动画触发的物体")]
    [SerializeField] private GameObject animObj;
    [SerializeField] private GameObject player;

    private bool canOperate;

    private void Start()
    {
        canOperate = true;
        son1 = transform.GetChild(0).gameObject;
        son2 = transform.GetChild(1).gameObject;

        //检测激活的物体是否正确1。
        if((son1.activeSelf && son2.activeSelf) || (!son1.activeSelf && !son2.activeSelf))
        {
            Debug.LogError("两个Style均为空or均为激活");
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
        //animObj获取动画组件方便后续加动画
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
            //Debug.Log("已禁用输入，无法收到移动和攻击指令");
        }


    }

    private void CheckAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            //触发攻击动画，其余逻辑在事件帧里。
            canOperate = false;
            playerscript.canOperate = false;

            anim.SetTrigger("Attack");

        }
    }


    private void CheckSwitch()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //切换成另一个儿子激活。
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
                // CurrentUsingIndex = 1 当前开启的是第二个形态，那就切回第一个。
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
        EventCenter.Instance.AddListener("鞋子攻击结束", FinishAttack);
        EventCenter.Instance.AddListener("停止游戏", StopGame);
        //Debug.Log("SwitchFoot注册停止游戏");

        EventCenter.Instance.AddListener("继续游戏", Continue);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener("鞋子攻击结束", FinishAttack);
        EventCenter.Instance.RemoveListener("停止游戏", StopGame);
        EventCenter.Instance.RemoveListener("继续游戏", Continue);


    }

    private void Continue(object i)
    {
        Debug.Log("SwitchFoot继续游戏");
        canOperate = true;
    }

    private void StopGame(object i)
    {
        Debug.Log("已停止Switch的操作！！！！！！！！！！！！！！！！！！！！！！！！！！");
        canOperate = false;
    }


    private void FinishAttack(object i)
    {
        canOperate = true;
    }




}
