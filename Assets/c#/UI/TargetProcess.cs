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
        EventCenter.Instance.AddListener("�ؿ�����", OnUpdateProcess);
    }
    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener("�ؿ�����", OnUpdateProcess);
    }

    /// <summary>
    /// ��ɱ���˵�ʱ��or�ж�����������ʱ��trigger���¼���Ȼ����ǰ����õ�ǰ����Ŀ��ٷֱȴ�������
    /// </summary>
    /// <param name="i"></param>
    private void OnUpdateProcess(object i)
    {
        processUI.value = Convert.ToSingle(i);
        // ͬ�����µ�һ��Player�ű��ϣ���Ϊ����ű���Ҹ����׻�ȡ��
        if (processUI.value >= 1 && !playerScript.IsGameFinished)
        {
            playerScript.IsGameFinished = true;
            //TODO: �ж����أ�1.ֹͣ��Ϸ��2.�������㻭��
            Debug.Log("�ؿ���ɣ�" + processUI.value);
            EventCenter.Instance.EventTrigger("ֹͣ��Ϸ", null);
            UIManager.Instance.ShowPanel<WinPanel>("UI/��Ϸ��panel/WinPanel",UIManager.UI_Layer.Mid);
        }

    }
}
