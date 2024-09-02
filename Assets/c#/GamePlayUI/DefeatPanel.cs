using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatPanel : BasePanel
{
    private bool isSpaced;

    public override void Start()
    {
        base.Start();
        isSpaced = false;
    }


    public override void OnClick(string name)
    {
        base.OnClick(name);
        switch(name)
        {
            case "X�������˵�":
                MonoMgr.Instance.StartSingleCoroutine(GotoMain());
                break;
        }



    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space) && !isSpaced)
        {
            isSpaced = true;
            //  TODO: ���¿ո�����³�ʼ�����ء�������д�ɷ������˵�
            MonoMgr.Instance.StartSingleCoroutine(GotoMain());

        }
    }

    private IEnumerator GotoGamePlayIntro()
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
        UIManager.Instance.HidePanel("UI/��Ϸ��panel/IntroGamePanel");
        UIManager.Instance.HidePanel("UI/��Ϸ��panel/DefeatPanel");
        UIManager.Instance.LoadingText.gameObject.SetActive(true);
        SceneMgr.Instance.LoadSceneAsyn("GamePlayIntro", SceneManager.GetActiveScene(), () =>
        {
            //����ȼ������ˣ���ʾ��һ����Ϸ�ڵĻ���UI������UnFade
            UIManager.Instance.ShowPanel<IntroGamePanel>("UI/��Ϸ��panel/IntroGamePanel", UIManager.UI_Layer.Bot);
            //EventCenter.Instance.EventTrigger("������Ϸ�������������", null);
            //UIManager.Instance.ShowPanel<TeachPanel>("UI/��Ϸ��panel/TeachPanel", UIManager.UI_Layer.Mid);
            UIManager.Instance.UnFadeCanvas();
            UIManager.Instance.LoadingText.gameObject.SetActive(false);
            // ���¿�ʼ��Ϸʱ������ˢ��
            Pool pool = FindAnyObjectByType<Pool>();
            pool.StartCreateMonster();
            EventCenter.Instance.EventTrigger("������Ϸ", null);
            EventCenter.Instance.CheckCount();
        });
        //��������һ���״�ĵط��������첽���صĶ�����˼����Щ���Բ�����������Щ�������Load֮���������ߵ�д��һ��������Ȼ�󴫵ݸ�LoadScene��
        //��������ر���ҳ��panel���Բ�������

    }

    private IEnumerator GotoMain()
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
            //�����˵���ʱ����Ҫ�ټ���һ���ˡ�
            //UIManager.Instance.ShowPanel<IntroGamePanel>("UI/���˵�panel/MainPanel", UIManager.UI_Layer.Bot);
            UIManager.Instance.UnFadeCanvas();
            UIManager.Instance.LoadingText.gameObject.SetActive(false);
        });
        //��������һ���״�ĵط��������첽���صĶ�����˼����Щ���Բ�����������Щ�������Load֮���������ߵ�д��һ��������Ȼ�󴫵ݸ�LoadScene��
        //��������ر���ҳ��panel���Բ�������
        UIManager.Instance.HidePanel("UI/��Ϸ��panel/DefeatPanel");
        UIManager.Instance.HidePanel("UI/��Ϸ��panel/IntroGamePanel");
        EventCenter.Instance.CheckCount();



    }

}
