using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResMgr : Singleton<ResMgr>
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">��Դ����</typeparam>
    /// <param name="pathName">��Դ���ڵ�·����</param>
    /// <returns></returns>
    public T Load<T>(string pathName)where T : Object
    {
        T obj = Resources.Load<T>(pathName);
        //�����GameObject����ֱ��ʵ����һ����Ȼ�󷵻ء�
        if(obj is GameObject)
        {
            return GameObject.Instantiate(obj);
        }
        //������Ƶ�����ͷ�����Դ���ɡ�

        return obj;
    }


    /// <summary>
    /// �ص������Ҫ��һ��func�����������������һ��T����Ҳ����UnityAction���͵Ĳ�����
    /// Ϊʲô�ǵô�һ���������أ���Ϊ�첽��������������ֱ�Ӵ������Ҳ�ͬ��Դ������Ҳ��ͬ��
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="pathName"></param>
    /// <param name="func"></param>
    public void LoadAsyn<T>(string pathName, UnityAction<T> func) where T : Object
    {
        MonoMgr.Instance.StartSingleCoroutine(RealLoadAsyn<T>(pathName, func));
    }

    IEnumerator RealLoadAsyn<T>(string pathName, UnityAction<T> func)where T : Object
    {
        ResourceRequest obj = Resources.LoadAsync<T>(pathName);

        //��һ�������Ϊ�˵�obj��������ټ���ִ�������asset�жϡ����߳����ⲻͣ��ÿ�ֵȴ�obj��ɣ���ɲ��˾�һֱѭ�����ڶ����߳̿����������Ǿ��첽���ء�
        yield return obj;
        if (obj.asset!=null)
        {
            Debug.Log("�첽�������");
        }
        else
        {
            Debug.Log("�첽������Դʧ�ܣ�");
        }
        if (obj.asset is GameObject)
        {
            func(GameObject.Instantiate(obj.asset) as T);
        }
        else
        {
            func(obj.asset as T);
        }

    }


    public void UILoadAsyn<T>(string pathName, UnityAction<T> func) where T : Object
    {
        MonoMgr.Instance.StartSingleCoroutine(UIRealLoadAsyn<T>(pathName, func));
    }

    IEnumerator UIRealLoadAsyn<T>(string pathName, UnityAction<T> func) where T : Object
    {
        ResourceRequest obj = Resources.LoadAsync<T>(pathName);

        //��һ�������Ϊ�˵�obj��������ټ���ִ�������asset�жϡ����߳����ⲻͣ��ÿ�ֵȴ�obj��ɣ���ɲ��˾�һֱѭ�����ڶ����߳̿����������Ǿ��첽���ء�
        yield return obj;
        if (obj.asset != null)
        {
            Debug.Log("�첽�������"+ pathName);
        }
        else
        {
            Debug.LogError("�첽������Դʧ�ܣ�"+ pathName);
        }
        if (obj.asset is GameObject)
        {
            func(GameObject.Instantiate(obj.asset, UIManager.Instance.canvas as Transform) as T);
        }
        else
        {
            func(obj.asset as T);
        }

    }





}
