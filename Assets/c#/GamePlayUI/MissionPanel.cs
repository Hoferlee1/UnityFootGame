using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionPanel : BasePanel
{

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("�ؿ�UI����"+uiDic.Count);
        //���ݵ�ǰSetting�еĽ��ȣ�Active��Ӧ��button��
        for (int i = 1; i <= Setting.Instance.MissionProgress; ++i)
        {
            SearchChildUI<Button>(i.ToString()).gameObject.SetActive(true);
        }

    }
    public override void OnClick(string name)
    {
        base.OnClick(name);

        switch (name)
        {
            case "ReturnToMain":
                // �������˵� �л�����
                MonoMgr.Instance.StartSingleCoroutine(GotoMainMenu());
                break;
            case "1":
                // TODO :�򿪽�ɫ��UI����ʾ���ո����������ȱ���������ʾ�� ��������˵Ļ������Ǹ�ҳ���Ͽ��Ǽ��ض�Ӧ�ؿ� 
                // ����������ĳ����߼������漰�������ˡ���
                HideMe();
                UIManager.Instance.ShowPanel<PlayerSetPanel>("UI/��Ϸ��panel/PlayerSetPanel", UIManager.UI_Layer.Top);

                break;
            case "2":



                break;
            case "3":



                break;
            //case "M4":
            //    break;
        }


    }


    private IEnumerator GotoMainMenu()
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
        SceneMgr.Instance.LoadSceneAsyn("MainMenu", SceneManager.GetActiveScene(), () =>
        {
            //����ȼ������ˣ���ʾ��һ����Ϸ�ڵĻ���UI������UnFade
            UIManager.Instance.ShowPanel<MainPanel>("UI/���˵�panel/MainPanel", UIManager.UI_Layer.Bot);
            UIManager.Instance.UnFadeCanvas();
            UIManager.Instance.LoadingText.gameObject.SetActive(false);
        });
        //��������һ���״�ĵط��������첽���صĶ�����˼����Щ���Բ�����������Щ�������Load֮���������ߵ�д��һ��������Ȼ�󴫵ݸ�LoadScene��
        //��������ر���ҳ��panel���Բ�������
        UIManager.Instance.HidePanel("UI/��Ϸ��panel/MissionPanel");


    }

}
