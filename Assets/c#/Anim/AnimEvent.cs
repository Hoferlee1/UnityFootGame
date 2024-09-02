using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Building;

/// <summary>
/// 本类用于管理所有的动画事件，使用时把此脚本对象拖动到对应的动画物体下面
/// 用于Animation中加Add Event
/// </summary>
public class AnimEvent : MonoBehaviour
{

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Allfoot;
    [SerializeField] private GameObject Shadow;
    [SerializeField] private GameObject footPrintLeft;
    [SerializeField] private GameObject footPrintRight;
    private int CurrentUsingIndex;
    private Player playerScript;
    private MapControl map;
    public Pool pool;

    public float recoverHP = 5;


    private void Start()
    {
        CurrentUsingIndex = Shadow.GetComponent<SwitchFoot>().CurrentUsingIndex;
        playerScript = Player.GetComponent<Player>();
        pool = FindAnyObjectByType<Pool>();
        map = FindAnyObjectByType<MapControl>();
    }

    /// <summary>
    /// Foot攻击动画发生的时候调用
    /// </summary>
    public void OnFootAttack()
    {


        // TODO：按下空格后发动技能 1.产生脚印，2.产生技能效果。  先用实例化的方式写， 后期再用对象池的方式改过来。。。=
        // 脚印位置出现在Player相同的position下。
        Vector3 temp = new Vector3(Player.transform.position.x + 0.13f, Player.transform.position.y + 4.27f, 22f);
        GameObject obj = null;
        if (CurrentUsingIndex == 0)
        {
            obj = GameObject.Instantiate(footPrintLeft, temp, Quaternion.identity);
        }
        else
        {
            obj = GameObject.Instantiate(footPrintRight, temp, Quaternion.identity);
        }
        obj.transform.SetParent(Allfoot.transform);

        //TODO:技能效果，检测一个范围，杀死敌人，摧毁房子。
        Vector2 center = new Vector2(Shadow.transform.position.x, Shadow.transform.position.y);

        //先检测layer8的怪物
        Collider2D[] colliders = Physics2D.OverlapAreaAll( center+new Vector2(-2.58f, 0.6f), center + new Vector2(2.6f, 9.88f), 1<<8);
        foreach (Collider2D collider in colliders)
        {
            // 用对象池回收。
            if (collider.CompareTag("NormalM"))
            {
                pool.dic["Monster"].Release(collider.gameObject);
                EventCenter.Instance.EventTrigger("更改目前敌人数量", pool.dic["Monster"].CountActive);
                map.KilledEnemy += 1;
            }else if (collider.CompareTag("HPM"))
            {
                // 回血哥布林不计入右上的最大怪物数量计算了，数量太少
                // TODO: 触发给主角回血的事件。


                playerScript.TakeRecover(recoverHP);
                pool.dic["HpMonster"].Release(collider.gameObject);
                map.KilledEnemy += 1;

            }


        }

        //TODO:再检测layer7的房子，
        colliders = Physics2D.OverlapAreaAll(center + new Vector2(-2.58f, 0.6f), center + new Vector2(2.6f, 9.88f), 1 << 7);
        foreach (Collider2D collider in colliders)
        {
            // TODO:Demo版不做对象池了，这里直接套用就好啦。
            Building build = collider.GetComponent<Building>();
            switch (build.currentExtent)
            {
                case Building.HouseExtent.Perfect:
                    build.currentExtent = Building.HouseExtent.HalfDes;
                    build.ChangeSpiriteTo(build.currentExtent);
                    build.StartHpMonster();  // 房子2s后可以出回血兵
                    break;
                case Building.HouseExtent.HalfDes:
                    EventCenter.Instance.EventTrigger("摧毁建筑", null); //统计进度以及成就，以及关卡结算用的。
                    build.CloseColliderBox();
                    build.currentExtent = Building.HouseExtent.TotalDes;
                    build.ChangeSpiriteTo(build.currentExtent);
                    map.DestoryedBuilding += 1;

                    break;

            }




        }
        // 攻击动作后变为可被攻击 在这里改，这样可以优先杀怪
        playerScript.canBeDamaged = true;

    }


    public void FinishAttack()
    {
        // TODO这里做个判断，如果TM游戏已经结束了，那就不要恢复了。。。。
        if (!playerScript.IsGameFinished)
        {
            playerScript.canOperate = true;
            Debug.Log("恢复player的控制+恢复Switchfoot");
            EventCenter.Instance.EventTrigger("鞋子攻击结束", null);
        }


    }

    public void OnFinishCanBeDamaged()
    {
        //攻击动作后变为不可被攻击
        playerScript.canBeDamaged = false;
    }


}
