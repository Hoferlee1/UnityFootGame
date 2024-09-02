using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneMgr : Singleton<SceneMgr>
{
    /// <summary>
    /// ͬ�����س�������
    /// </summary>
    /// <param name="Scenename">�³�������</param>
    /// <param name="func">�����л���ɺ���Ҫִ�е�func</param>
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
    /// Э�����ú����ͺ������һ��IEЭ�̺�������
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
            //TODO: �¼����ĵĴ���д��������Ȼ�ˣ����Ҫ������UIȥע��һ��
            EventCenter.Instance.EventTrigger("�����л�������", op.progress);
            if (op.progress >= 0.9f)
            {
                //Debug.Log("����break");
                op.allowSceneActivation = true;
                break;
            }

            yield return null;

        }

        Debug.Log(Scenename + "�����Ѽ��");
        yield return null;


        // Ϊʲôע�����أ���Ϊ���ּ��س�����ģʽ�£��³���������֮���ϳ����Զ���ж���ˡ�
        //AsyncOperation op2 = SceneManager.UnloadSceneAsync(LastScene);

        //// ��ж��������ִ�С�
        //while (!op2.isDone)
        //{
        //    yield return null;
        //}

        //������ɺ�ִ�к���func
        if (func != null)
        {
            func();
        }


    }


}
