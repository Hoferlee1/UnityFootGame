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
        EventCenter.Instance.AddListener("Ѫ������01", OnHurt);
    }
    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener("Ѫ������01", OnHurt);
    }

    /// <summary>
    /// Ѫ���½���ʱ������¼����ĵ������Ȼ����ǰ����õ�ǰѪ���ٷֱȴ�������
    /// </summary>
    /// <param name="i"></param>
    private void OnHurt(object i)
    {
        bloodUI.value = Convert.ToSingle(i);

    }

}
