using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetProcess : MonoBehaviour
{
    private Slider processUI;
    private Player playerScript;
    private void Start()
    {
        processUI = GetComponent<Slider>();
        processUI.value= 0;
        playerScript = FindAnyObjectByType<Player>();
    }
    private void OnEnable()
    {
        EventCenter.Instance.AddListener("关卡进度", OnUpdateProcess);
    }
    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener("关卡进度", OnUpdateProcess);
    }

    /// <summary>
    /// 击杀敌人的时候，or判定敌人死亡的时候trigger此事件，然后提前计算好当前消灭目标百分比传进来。
    /// </summary>
    /// <param name="i"></param>
    private void OnUpdateProcess(object i)
    {
        processUI.value = Convert.ToSingle(i);
        // 同步更新到一个Player脚本上，因为这个脚本大家更容易获取。
        if (processUI.value >= 1 && !playerScript.IsGameFinished)
        {
            playerScript.IsGameFinished = true;
            //TODO: 判定过关，1.停止游戏，2.弹出结算画面
            Debug.Log("关卡完成！" + processUI.value);
            EventCenter.Instance.EventTrigger("停止游戏", null);
            UIManager.Instance.ShowPanel<WinPanel>("UI/游戏内panel/WinPanel",UIManager.UI_Layer.Mid);
        }

    }
}
