using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainStart : MonoBehaviour
{

    void Start()
    {
        Setting.Instance.PrintLod();
        // һ������Ϸ��������� �������˵�panel 
        UIManager.Instance.SetCanvasRenderCamera(GameObject.Find("Main Camera").GetComponent<Camera>());
        UIManager.Instance.ShowPanel<MainPanel>("UI/���˵�panel/MainPanel", UIManager.UI_Layer.Bot);
        //test
        //UIManager.Instance.ShowPanel<MainPanel>("UI/testUI/MainPanel2", UIManager.UI_Layer.Bot);

        // ���������Ӧ�ü��������ִ������Ĵ��룬���ǲ�û��������������Ϊ����Ҫִ�е���������
        // ��ö�д��unityaction�ﴫ�ݸ�ShowPanel

    }





}
