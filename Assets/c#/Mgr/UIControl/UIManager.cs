using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public enum UI_Layer
    {
        Bot,
        Mid,
        Top,
    }
    /// <summary>
    /// ע�����Dic��panel���ֺ�panel�ű���Ӧ�ġ�Ϊʲô�ǵ��ǽű������Ǹ�panel������Ϊ�ű�����ͨ��.gameObject�ķ�ʽ��ȡ������
    /// </summary>
    public Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();
    public CanvasGroup canvasgroup;
    public TextMeshProUGUI LoadingText;
    public Transform Black;

    private Transform bot;
    private Transform mid;
    private Transform top;
    //private Transform system;
    public RectTransform canvas;
    private Animator anim;
    //  ��ʱ��֪��Eventsystem�����õģ���ע���˲�Ӱ���֡�


    public UIManager()
    {
        //  ���ؼ���Ψһ��Canvas����
        GameObject obj = ResMgr.Instance.Load<GameObject>("UI/Canvas");
        // �����canvas��Ӧ���������������������Ȼ����Զ��Ҫ���١�
        canvas = obj.transform as RectTransform;
        GameObject.DontDestroyOnLoad(obj);
        //  obj = ResMgr.Instance.Load<GameObject>("UI/EventSystem");

        //  �������Find�ǲ��Ҷ�����������ֵĲ����ض�Ӧ��transform���������canvas��ΪԤ���壬һ��ʼ����͵����ȴ��ϵ��°���������һ�㣬�м�㣬������Ĳ�ķ�ʽ�����ö�Ӧ��3�������塣
        bot = canvas.Find("bot");
        mid = canvas.Find("mid");
        top = canvas.Find("top");
        //system = canvas.Find("System");
        // Find��transform�ķ��������ص�Ҳ��transform
        // ��ȡ��ɫĻ��
        Black = canvas.Find("Black");
        //Black.gameObject.SetActive(false);

        LoadingText = canvas.Find("Loading").GetComponent<TextMeshProUGUI>();
        LoadingText.gameObject.SetActive(false);
        canvasgroup = canvas.GetComponent<CanvasGroup>();
        anim = canvas.GetComponent<Animator>();
        EventCenter.Instance.AddListener("�����л�������", EventLoading);
    }
    /// <summary>
    /// ���ز���ʾһ��Panel��Ȼ��Ҫ��������õ�ʱ������������ھ���Ĳ���,���Դ��ݵڶ�������layer
    /// </summary>
    /// <param name="panelName">Ҫ���ص�panel��ʲô��</param>
    /// <param name="layer">����һ���ϣ�</param>
    /// <param name="func">��panel����һЩ����</param>
    public void ShowPanel<T>(string panelName, UI_Layer layer, UnityAction<T> func = null) where T:BasePanel
    {
        if (panelDic.ContainsKey(panelName))
        {
            //panelDic[panelName].ShowMe();
            Debug.Log("UIpaneldic����ӵ�д�panel��" + panelName);
            //ֱ�ӵ���func����������������ô�
            if (func != null)
            {
                func(panelDic[panelName] as T);
            }

            return;
        }


        // �����lambda������obj�������GameObject���ͣ��ͻ��ڳ�����ʵ������������ʹ��һ��GameObject���ͣ����򲻳���ʵ����ֱ��ʹ�ö�Ӧ���͡�
        // Ҳ����˵��������� obj��һ���Ѿ�ʵ�����˵��Ǹ���������Դ��GameObject���塣
        ResMgr.Instance.UILoadAsyn<GameObject>(panelName, (obj) =>   
        {

            // ����Ϊ�������첽��Դһ�������������
            Debug.Log(" ShowPanel�������"+ panelName);
            //���ؽ��������ҵ����ݵ�layer��һ���Ҫ��obj�ĸ������ó�father
            Transform father = bot;
            switch (layer)
            {
                case UI_Layer.Mid:
                    father = mid;
                    break;
                case UI_Layer.Top:
                    father = top;
                    break;
                    //����ʡ��һ��system����ʱ�ò���
            }
            //��仰���Ҫ����SetParent false�� UI���Լӵģ���ΪUI����������õ���ֵ���϶���ϣ�����϶���ʱ����ֵ�ı䡣
            // δ�����Լ�һ�����ܣ��������������UIʵ������ĳ��transform����
            obj.transform.SetParent(father, false);
            //obj.transform.SetParent(father, true);


            //obj.transform.SetParent(father);

            //obj.transform.localScale = Vector3.one;

            // �����仰ʹ�ô��ɣ��Ȼ��ϸ��
            //(obj.transform as RectTransform).offsetMax = Vector2.zero;
            //(obj.transform as RectTransform).offsetMin = Vector2.zero;

            //�õ�Ԥ�������ϵĽű����������Ҫ����ؽ�����Ԥ����panel���ϱ����ж�Ӧ�Ľű�������ű�
            //�������Լ���Ԥ�����ʱ���Լ�����ȥ�ġ�

            T panel = obj.GetComponent<T>();

            if (func != null)
            {
                func.Invoke(panel);
            }
            //panel.ShowMe();

            panelDic.Add(panelName, panel);

            //Debug.Log(UIManager.Instance.panelDic.Count);


        });
    }


    /// <summary>
    /// ����Destory��һ��panel��
    /// </summary>
    /// <param name="name"></param>
    public void HidePanel(string name)
    {
        if (panelDic.ContainsKey(name))
        {
            //�����hideme��
            //Debug.Log("�ҵ�Ҫɾ����panel��");

            panelDic[name].HideMe();
            //Debug.Log("Panel Destory֮ǰ");
            GameObject.Destroy(panelDic[name].gameObject);//����ȡ�������ǽű������ԼӸ�gameobject�ǻص���屾��
            //Debug.Log("Panel Destory֮��");
            panelDic.Remove(name);
        }
        else
        {
            Debug.Log("δ�ҵ�panel���޷�ɾ��");
        }
    }

    /// <summary>
    /// ���ͻ�ȡ��壬�������ֺ�������ͻ�ȡ��Ӧ����塣
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T GetPanel<T>(string name)where T:BasePanel
    {
        //Debug.Log(panelDic.Count);
        if (panelDic.ContainsKey(name))
        {
            return panelDic[name] as T;
        }
        Debug.Log("δ�ҵ���Ӧ��Panel����" + name +"��������ӡDic�����е�panel" + "��ǰdic�е�panel������" + panelDic.Count);
        foreach (var pname in panelDic.Keys)
        {
            Debug.Log(pname);
        }
        Debug.Log("��ӡ����");

        return null;
    }

    public Transform GetLayer(UI_Layer layer)
    {
        switch(layer)
        {
            case UI_Layer.Top:
                return this.top;
            case UI_Layer.Mid:
                return this.mid;
            case UI_Layer.Bot:
                return this.bot;


        }
        return null;
    }


    public void FadeCanvas()
    {
        // ��ֹһ��UI��������Ļ��ʼ�䰵��
        canvasgroup.interactable = false;
        anim.SetBool("GoToAlpha0", true);
    }

    public void UnFadeCanvas()
    {
        // ����UI��������Ļ��ʼ������
        canvasgroup.interactable = true;
        anim.SetBool("GoToAlpha0", false);
    }


    /// <summary>
    /// ���س�����ʱ��progress<0.9f��ʱ�򣬵��õĺ�����Ŀ����Ϊ�˼�һЩLoading�����Ԫ��
    /// </summary>
    /// <param name="progress"></param>
    private void EventLoading(object progress)
    {
        // Ŀǰʹ������ֻ������ǿ��ת��
        //Debug.Log("ת������");
        float pro = (float)(progress);
        //Debug.Log("����"+pro);
        if (pro >= 0.9f)
        {
            LoadingText.text = "Loading " + 100 + "%";
        }
        else
        {
            LoadingText.text = "Loading " + pro * 100 + "%";
        }
    }


    // �л�Canvas��Render Camera  
    public void SetCanvasRenderCamera(Camera newCamera)
    {
        if (canvas.gameObject != null)
        {
            // ����Canvas��Render Camera    SetRenderCamera(newCamera);
            canvas.GetComponent<Canvas>().worldCamera = newCamera;
            // ����㻹����Canvas��Plane Distance���µ�Cameraƥ�䣬���Զ�������  
            // ע�⣺��ͨ��ֻ��Canvas��Render ModeΪScreen Space - Camera��Camera���������ʱ����  1
            // mainCanvas.planeDistance = newCamera.nearClipPlane; // ����ʹ�ã�����ʵ���������  
        }
    }

}
