using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResMgr : Singleton<ResMgr>
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    /// <param name="pathName">资源所在的路径名</param>
    /// <returns></returns>
    public T Load<T>(string pathName)where T : Object
    {
        T obj = Resources.Load<T>(pathName);
        //如果是GameObject类型直接实例化一个，然后返回。
        if(obj is GameObject)
        {
            return GameObject.Instantiate(obj);
        }
        //其他音频等类型返回资源即可。

        return obj;
    }


    /// <summary>
    /// 重点是最后要传一个func函数，这个函数接收一个T类型也就是UnityAction类型的参数。
    /// 为什么非得传一个后处理函数呢？因为异步加载做不到这样直接处理。而且不同资源处理方法也不同。
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

        //加一句这个是为了等obj加载完成再继续执行下面的asset判断。主线程在这不停的每轮等待obj完成，完成不了就一直循环。第二个线程开在了上面那句异步加载。
        yield return obj;
        if (obj.asset!=null)
        {
            Debug.Log("异步加载完成");
        }
        else
        {
            Debug.Log("异步加载资源失败！");
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

        //加一句这个是为了等obj加载完成再继续执行下面的asset判断。主线程在这不停的每轮等待obj完成，完成不了就一直循环。第二个线程开在了上面那句异步加载。
        yield return obj;
        if (obj.asset != null)
        {
            Debug.Log("异步加载完成"+ pathName);
        }
        else
        {
            Debug.LogError("异步加载资源失败！"+ pathName);
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
