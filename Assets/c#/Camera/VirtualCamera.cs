using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    static private GameObject _instance;
    static public GameObject Instance
    {
        get { return _instance; }
    }
    // ����д�������������
    void Start()
    {
        if (_instance != null)
        {
            // ����ľ�̬�����Ѿ����ˣ��ݻ��������
            Destroy(_instance);
            return;
        }
        else
        {
            _instance = gameObject;
            DontDestroyOnLoad(this);
            EventCenter.Instance.AddListener("������Ϸ�������������", onLoadGameScene);
        }


    }

    /// <summary>
    /// ������㴫һ�����ɣ��л�������ʱ����õ��¼������ڸ������������Ϸ��ɫ
    /// </summary>
    /// <param name="i"></param>
    private void onLoadGameScene(object i)
    {
        //��ѯ��������Ȼ���֮��
        GameObject obj = GameObject.FindWithTag("Player");
        gameObject.GetComponent<CinemachineVirtualCamera>().Follow = obj.transform;
    }
}
