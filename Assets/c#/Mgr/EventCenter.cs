using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCenter : Singleton<EventCenter>
{
    private Dictionary<string, UnityAction<object>> EventDic = new Dictionary<string, UnityAction<object>>();
    private Dictionary<string, int> DicCount = new Dictionary<string, int>();


    /// <summary>
    /// ���û�о͵�һ�Σ����Բ�����ô��������ӽ�ȥ�����Ǹ��˾��û��Ƿֿ��ȽϺã�Ŀǰ�����غ��ˡ�
    /// </summary>
    /// <param name="EventName"></param>
    /// <param name="func"></param>
    public void AddListener(string EventName, UnityAction<object> func)
    {
        //Debug.Log(EventName + "��ע����");
        if(EventDic.ContainsKey(EventName))
        {
            //Debug.Log(EventName + "ע��");
            EventDic[EventName] += func;
            DicCount[EventName] += 1;
        }
        else
        {
            //Debug.Log(EventName + "��һ��ע��");

            EventDic.Add(EventName, func);
            DicCount.Add(EventName, 1);
        }
    }

    public void EventTrigger(string EventName, object canshu)
    {
        if(EventDic.ContainsKey((string)EventName))
        {
            Debug.Log($"ע��{EventName}�¼�Ŀǰ����ô�����" + DicCount[EventName]);
            EventDic[EventName].Invoke(canshu);
            
        }
        else
        {

            Debug.Log("�޴��¼�"+EventName);
        }
    }


    public void RemoveListener(string EventName, UnityAction<object> func) 
    {
        if(EventDic.ContainsKey(EventName))
        {
            EventDic[EventName] -= func;
            DicCount[EventName] -= 1;
            Debug.Log($"{EventName}�¼��Ƴ�һ�������ڻ�ʣ�£�{DicCount[EventName]}");
        }
    }

    public void CheckCount()
    {
        foreach (var i in DicCount.Keys)
        {
            if (DicCount[i] != 0)
            {
                Debug.Log($"{i}�¼������ĺ�������0������Ϊ{DicCount[i]}");
            }
        }
    }

    public void Clear()
    {
        EventDic.Clear();
    }



}
