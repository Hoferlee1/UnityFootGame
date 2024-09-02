using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ������������и����⣬�������û���õ����࣬�ܶණ����û�о�����ʼ���ģ�������Ϸ�ڵ�Object��һ��������ֱ�Ӿ�����
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
