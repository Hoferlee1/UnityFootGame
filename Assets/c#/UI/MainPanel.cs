using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPanel : BasePanel
{
    /// <summary>
    /// �Ƿ�����ˣ����ڵ�һ�ο�ʼ��Ϸʱ��ʾ�浵�������������Ҫ�浽ע���
    /// </summary>


    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// ����ֱ����Switch�����������ť����ֱ�ӵĺ������ɣ���ΪOnClick���Ǹ���AddListener���¼����������button���������Զ�ȥSwitch����ִ�ж�Ӧ�ĺ�����
    /// </summary>
    /// <param name="name"></param>
    public override void OnClick(string name)
    {
        base.OnClick(name);
        //Debug.Log("mainpanel�ѵ���" + name);
        switch (name){
            case "�浵Button":
                //�򿪴浵panel��block�Լ���ҳ��
                HideMe();
                UIManager.Instance.ShowPanel<SaveBankPanel>("UI/���˵�panel/SaveBankPanel", UIManager.UI_Layer.Mid);
                break;
            case "��ʼButton":
                StartGame();
                break;
            case "��ɫButton":
                break;
            case "����Button":
                break;
            case "�˳�Button":
                //��QuitPanel��block�Լ���ҳ��
                HideMe();
                UIManager.Instance.ShowPanel<QuitPanel>("UI/���˵�panel/QuitPanel", UIManager.UI_Layer.Top);

                break;
        }


    }

    /// <summary>
    /// ����ע�᱾���OnClick����¼�
    /// </summary>
    private void StartGame()  
    {
        //  TODO:��ʼ��Ϸ
        // ����Ѿ���������Ϸһ����
        if(Setting.Instance.IsPlayed)
        {
            //TODO:  ��������Ĵ浵���ò������ؿ�ѡ����
            Setting.Instance.Load(Setting.Instance.LastPlaySlot.ToString());
            MonoMgr.Instance.StartSingleCoroutine(GoToMissionMap());
        }
        else
        {
            // ��һ���������Ϸ���Ǿ͵��л��浵�����ˡ�
            // �򿪴浵panel��block�Լ���ҳ��
            HideMe();
            UIManager.Instance.ShowPanel<SaveBankPanel>("UI/���˵�panel/SaveBankPanel", UIManager.UI_Layer.Mid);
        }




    }

    private IEnumerator GoToMissionMap()
    {
        Image black = UIManager.Instance.Black.GetComponent<Image>();
        UIManager.Instance.FadeCanvas();
        while (true)
        {
            float alpha = black.color.a;
            //Debug.Log(alpha);
            if (alpha >= 0.99f)
                break;
            yield return null;
        }
        Debug.Log("�䰵����������call event");

        //���´������ȫ�䰵�Ķ��������󣬿�ʼ���س���
        //�����곡���� ɾ��֮ǰ�ĳ�����Ȼ��show����Ҫ��ʾ��UI��
        //Ȼ���Ƚ��UI+����

        UIManager.Instance.LoadingText.gameObject.SetActive(true);
        SceneMgr.Instance.LoadSceneAsyn("GamePlayIntro", SceneManager.GetActiveScene(), () =>
        {
            //����ȼ������ˣ���ʾ��һ����Ϸ�ڵĻ���UI������UnFade
            UIManager.Instance.ShowPanel<MissionPanel>("UI/��Ϸ��panel/MissionPanel", UIManager.UI_Layer.Mid);
            UIManager.Instance.UnFadeCanvas();
            UIManager.Instance.LoadingText.gameObject.SetActive(false);
        });
        //��������һ���״�ĵط��������첽���صĶ�����˼����Щ���Բ�����������Щ�������Load֮���������ߵ�д��һ��������Ȼ�󴫵ݸ�LoadScene��
        //��������ر���ҳ��panel���Բ�������
        UIManager.Instance.HidePanel("UI/���˵�panel/MainPanel");

    }


}
