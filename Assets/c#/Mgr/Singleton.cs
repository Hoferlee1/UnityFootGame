using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 这个类用起来有个问题，就是如果没人用单例类，很多东西是没有经过初始化的，不像游戏内的Object第一个场景里直接就有了
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> where T: new()
{
    private static T instance;
    public static T Instance
    {
        get
        { 
            if (instance == null)
            {
                instance = new T();
            }
            return instance; 
        }
    }

}
