using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


/// <summary>
/// 暂时当单例用了，后面再重构好了
/// </summary>
public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private float PosX;
    private float PosY;
    private float mapYup;
    private float mapYdown;
    private float mapXleft;
    private float mapXright;
    private float HP;
    public float FullHP = 200;

    /// <summary>
    /// 仅在血条为空或者进度满的时候修改此项
    /// </summary>
    public bool IsGameFinished;


    [Header("子物体们")]
    private List<GameObject> Styles;
    // 当前使用的技能下标
    public int CurrentIndexOfStyles;
    public bool canOperate;
    public bool canBeDamaged;

    void Start()
    {
        //初始化血量
        HP = FullHP;
        //初始化游戏状态，游戏结束时改为true，仅在血条为空或者进度满的时候修改此项
        IsGameFinished = false;
        // speed = 12.0f;
        // TODO：暂时先用这个初始化，换地图后，应该根据不同关卡初始化这四个值
        // 因为每个地图都有各自的边缘
        mapYup = 20.05f;
        mapYdown = -20.7f;
        mapXright = 37.2f;
        mapXleft = -37.6f;

        // 建立此脚本与子物体的联系
        Styles = new List<GameObject>
        {
            transform.GetChild(0).gameObject,
            transform.GetChild(1).gameObject
            //transform.GetChild(2).gameObject
        };
        CurrentIndexOfStyles = 0;
        Styles[CurrentIndexOfStyles].SetActive(true);

        canOperate = true;
        canBeDamaged = false;
    }
    private void Update()
    {
        if (canOperate)
        {
            CheckMapBorder();
            CheckInput();
        }

    }

    private void CheckMapBorder()
    {
        //Debug.Log("可以移动");
        //Vertical and Horizontal
        PosX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        PosY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        PosX = transform.position.x + PosX;
        PosY = transform.position.y + PosY;
        // TODO:检测是否越位。
        // 脚pivot到脚尖的距离：9.4f
        // 脚pivot到大腿根的距离：7.2f
        // 左右两侧的距离：右侧1.95f 左侧：1.64f;
        if (PosY+9.4f > mapYup)
        {
            PosY = mapYup - 9.4f;
        }
        if (PosY < mapYdown) // -7.2f 
        {
            PosY = mapYdown;
        }
        if (PosX- 1.64f < mapXleft)
        {
            PosX = mapXleft+ 1.64f;
        }
        if (PosX+ 1.95f > mapXright)
        {
            PosX = mapXright- 1.95f;
        }
        transform.position = new Vector3(PosX, PosY, 0.0f);
    }

    private void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            // 切换到脚style1  切换到什么和外面对应即可。
            SwitchStyle(0);
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            // 切换到手
            SwitchStyle(1);

        }


    }

    private void SwitchStyle(int index)
    {
        Styles[CurrentIndexOfStyles].SetActive(false);
        Styles[index].SetActive(true);
        CurrentIndexOfStyles = index;
    }




    #region 血量相关

    public void TakeDamage(object damage)
    {
        HP -= (float)damage;
        // TODO:  掉血触发UI血条时间，判断如果HP小于等于0了弹出失败界面。
        // 书是破损的一个panel。
        // TODO: 空格继续，点击右上角返回主菜单。
        EventCenter.Instance.EventTrigger("血条更新01", HP/FullHP);

        if(HP < 0 && !IsGameFinished)
        {
            // 1.停止游戏。2.弹出结算画面
            IsGameFinished = true;
            EventCenter.Instance.EventTrigger("停止游戏", null);
            UIManager.Instance.ShowPanel<DefeatPanel>("UI/游戏内panel/DefeatPanel", UIManager.UI_Layer.Mid);
        }


    }
    public void TakeRecover(object damage)
    {

        float p = (float)damage;
        if (HP + p > 200)
        {
            HP = 200;
        }
        else
        {
            HP += p;
        }
        EventCenter.Instance.EventTrigger("血条更新01", HP / FullHP);


    }
    #endregion

    private void OnEnable()
    {
        //Debug.Log("Player注册停止游戏");
        EventCenter.Instance.AddListener("停止游戏", StopGame);
        EventCenter.Instance.AddListener("继续游戏", Continue);


    }

    private void OnDisable()
    {
        
        EventCenter.Instance.RemoveListener("停止游戏", StopGame);
        EventCenter.Instance.RemoveListener("继续游戏", Continue);


    }

    private void Continue(object i)
    {
        Debug.Log("Player继续游戏");

        canOperate = true;
    }
    private void StopGame(object i)
    {
        canOperate = false;
        Debug.Log("已禁用移动指令！！！！！！！！！！！！！！！！！！！！！！！！！");

    }

}
