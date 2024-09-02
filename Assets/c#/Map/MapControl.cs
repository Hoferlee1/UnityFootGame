using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{

    public int  KilledEnemy;
    public int DestoryedBuilding;
    public float CostTime;
    public int Score;

    int AllBuildingNum;
    float CurrentDestory;
    private void Awake()
    {
        // 总建筑数量决定了进度条是否涨跌
        // 而这个数量取决于子建筑数量，所以子物体不要放无关的东西。(这个是统计的直接子物体)
        AllBuildingNum = transform.childCount;
        //Debug.Log("总建筑数量" + AllBuildingNum);
        CurrentDestory = 0;
    }

    /// <summary>
    /// 摧毁建筑的时候调用这个函数，负责更新建筑进度
    /// </summary>
    /// <param name="i"></param>
    private void OnDamageBuilding(object i)
    {
        CurrentDestory = CurrentDestory + 1;
        // 只要摧毁80%的建筑即可过关。
        //Debug.Log("当前摧毁建筑数量"+ CurrentDestory);
        EventCenter.Instance.EventTrigger("关卡进度", CurrentDestory / (AllBuildingNum * 0.8f));
        //Debug.Log("当前进度：" + CurrentDestory / (AllBuildingNum * 0.8f));

    }
    private void OnInitGame(object i)
    {
        KilledEnemy = 0;
        DestoryedBuilding = 0;
        CostTime = 0;
        Score= 0;
    }
    private void Update()
    {
        CostTime += Time.deltaTime;
    }
    private void OnEnable()
    {
        EventCenter.Instance.AddListener("摧毁建筑", OnDamageBuilding);
        EventCenter.Instance.AddListener("初始化本关", OnInitGame);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener("摧毁建筑", OnDamageBuilding);
        EventCenter.Instance.RemoveListener("初始化本关", OnInitGame);
    }


}
