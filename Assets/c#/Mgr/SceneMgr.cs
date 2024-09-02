using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneMgr : Singleton<SceneMgr>
{
    /// <summary>
    /// 同步加载场景函数
    /// </summary>
    /// <param name="Scenename">新场景名字</param>
    /// <param name="func">场景切换完成后需要执行的func</param>
    public void LoadScene(string Scenename, UnityAction func)
    {

        SceneManager.LoadScene(Scenename);
        func();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Scenename"></param>
    /// <param name="func"></param>
    public void LoadSceneAsyn(string Scenename, Scene LastScene, UnityAction func = null)
    {
        MonoMgr.Instance.StartSingleCoroutine(RealLoadScene(Scenename, LastScene,func));
        


    }

    /// <summary>
    /// 协程启用函数和后处理放在一个IE协程函数里面
    /// </summary>
    /// <param name="Scenename"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    IEnumerator RealLoadScene(string Scenename, Scene LastScene, UnityAction func) 
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(Scenename);
        //int a = 1;
        //while (a == 1)
        op.allowSceneActivation = false;
        while (!op.isDone)
        {
            //TODO: 事件中心的代码写过来，当然了，这得要求外面UI去注册一个
            EventCenter.Instance.EventTrigger("场景切换进度条", op.progress);
            if (op.progress >= 0.9f)
            {
                //Debug.Log("可以break");
                op.allowSceneActivation = true;
                break;
            }

            yield return null;

        }

        Debug.Log(Scenename + "场景已激活！");
        yield return null;


        // 为什么注释了呢？因为这种加载场景的模式下，新场景激活完之后老场景自动就卸载了。
        //AsyncOperation op2 = SceneManager.UnloadSceneAsync(LastScene);

        //// 等卸载完了再执行。
        //while (!op2.isDone)
        //{
        //    yield return null;
        //}

        //加载完成后执行后处理func
        if (func != null)
        {
            func();
        }


    }


}
