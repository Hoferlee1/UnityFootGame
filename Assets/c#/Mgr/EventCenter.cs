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
    /// 如果没有就第一次，所以不管怎么样都能添加进去，但是个人觉得还是分开比较好，目前功能重合了。
    /// </summary>
    /// <param name="EventName"></param>
    /// <param name="func"></param>
    public void AddListener(string EventName, UnityAction<object> func)
    {
        //Debug.Log(EventName + "来注册了");
        if(EventDic.ContainsKey(EventName))
        {
            //Debug.Log(EventName + "注册");
            EventDic[EventName] += func;
            DicCount[EventName] += 1;
        }
        else
        {
            //Debug.Log(EventName + "第一次注册");

            EventDic.Add(EventName, func);
            DicCount.Add(EventName, 1);
        }
    }

    public void EventTrigger(string EventName, object canshu)
    {
        if(EventDic.ContainsKey((string)EventName))
        {
            Debug.Log($"注意{EventName}事件目前有这么多个：" + DicCount[EventName]);
            EventDic[EventName].Invoke(canshu);
            
        }
        else
        {

            Debug.Log("无此事件"+EventName);
        }
    }


    public void RemoveListener(string EventName, UnityAction<object> func) 
    {
        if(EventDic.ContainsKey(EventName))
        {
            EventDic[EventName] -= func;
            DicCount[EventName] -= 1;
            Debug.Log($"{EventName}事件移除一个，现在还剩下：{DicCount[EventName]}");
        }
    }

    public void CheckCount()
    {
        foreach (var i in DicCount.Keys)
        {
            if (DicCount[i] != 0)
            {
                Debug.Log($"{i}事件监听的函数不是0，数量为{DicCount[i]}");
            }
        }
    }

    public void Clear()
    {
        EventDic.Clear();
    }



}
