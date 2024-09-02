using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blood : MonoBehaviour
{
    private Slider bloodUI;
    private void Start()
    {
        bloodUI= GetComponent<Slider>();
        bloodUI.value= 1;
    }
    private void OnEnable()
    {
        EventCenter.Instance.AddListener("血条更新01", OnHurt);
    }
    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener("血条更新01", OnHurt);
    }

    /// <summary>
    /// 血量下降的时候调用事件中心的这个，然后提前计算好当前血量百分比传进来。
    /// </summary>
    /// <param name="i"></param>
    private void OnHurt(object i)
    {
        bloodUI.value = Convert.ToSingle(i);

    }

}
