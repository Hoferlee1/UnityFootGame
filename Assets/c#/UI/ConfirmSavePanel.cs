using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConfirmSavePanel : BasePanel
{
    /// <summary>
    /// ֻ�е㿪�浵�����slot�Ż���������棬���������¼�ղ����ĸ�slot��
    /// </summary>
    public int slot = 0;


    public override void OnClick(string name)
    {
        base.OnClick(name);

        switch (name)
        {
            case "�ǵ�":
                // ��Զ�Ƕ�ȡ����
                // ����ǵ�һ���棬���̳̹ء�
                // TODO: �ǵ��ж��߼�����Ļ������Ӹ����ţ�����Ҫ���������Ǹ��ܷ���������...������ʱȥ�ˡ�
                if (!Setting.Instance.IsPlayed) 
                {
                    // TODO������slotֵ�޸��ϸ�panel�е��ı�TXT����������һ���棬�����޸�Ϊʱ��+����0/30���ɡ�
                    // ���ñ䰵�������䰵���������� ��Э���ж��Ƿ���ȫ�䰵�ˣ���ȫ�䰵���ټ��س�����
                    // �����䰵�����ŵ�����Э������
                    // �رյ�ǰpanel��仰���Բ�����д������һ��Destory���̡�
                    // ��Ϊ����������������仰ִ�������ٿ���Э�̵ġ����������Ӿ����ֻ�ܱ�Ť����ô���أ�
                    // UIManager.Instance.HidePanel("UI/���˵�panel/ConfirmSavePanel");
                    Debug.Log("��һ���棬�����л�����");
                    // ����������̳̹أ���ʱ��������仰ע����
                    // Setting.Instance.IsPlayed = true;
                    MonoMgr.Instance.StartSingleCoroutine(GotoGamePlayIntro());
                    UIManager.Instance.HidePanel("UI/���˵�panel/ConfirmSavePanel");
                    //StartCoroutine(GotoGamePlayIntro());

                }
                else
                {
                    // ���ǵ�һ���棬�Ǿ��Ǽ����ϴ浵Setting.Load
                    // LoadScene��Load��ɺ���ؿ�ѡ���� 
                    // TODO:Э�̷�����ûд��
                    Debug.Log("�ϴ浵�����ؿ�ѡ����");
                    Setting.Instance.Load(Setting.Instance.LastPlaySlot.ToString());
                    Setting.Instance.LastPlaySlot = slot;
                    MonoMgr.Instance.StartSingleCoroutine(GoToMissionMap());
                    UIManager.Instance.HidePanel("UI/���˵�panel/ConfirmSavePanel");

                }
                break;
            case "��":
                // �ر���һҳ���⿪��һҳ��
                // ��show��һ����hide�Լ���
                UIManager.Instance.GetPanel<SaveBankPanel>("UI/���˵�panel/SaveBankPanel").ShowMe();
                UIManager.Instance.HidePanel("UI/���˵�panel/ConfirmSavePanel");
                break;
        }



    }

    private IEnumerator GotoGamePlayIntro()
    {
        Image black = UIManager.Instance.Black.GetComponent<Image>();
        UIManager.Instance.FadeCanvas();

        Setting.Instance.LastPlaySlot = slot;
        Setting.Instance.IsSlotSaved[slot] = true;
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
            UIManager.Instance.ShowPanel<IntroGamePanel>("UI/��Ϸ��panel/IntroGamePanel", UIManager.UI_Layer.Bot);
            //EventCenter.Instance.EventTrigger("������Ϸ�������������", null);
            UIManager.Instance.ShowPanel<TeachPanel>("UI/��Ϸ��panel/TeachPanel", UIManager.UI_Layer.Mid);
            UIManager.Instance.UnFadeCanvas();
            UIManager.Instance.LoadingText.gameObject.SetActive(false);
        });
        //��������һ���״�ĵط��������첽���صĶ�����˼����Щ���Բ�����������Щ�������Load֮���������ߵ�д��һ��������Ȼ�󴫵ݸ�LoadScene��
        //��������ر���ҳ��panel���Բ�������
        UIManager.Instance.HidePanel("UI/���˵�panel/SaveBankPanel");
        UIManager.Instance.HidePanel("UI/���˵�panel/MainPanel");
        EventCenter.Instance.CheckCount();

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
        UIManager.Instance.HidePanel("UI/���˵�panel/SaveBankPanel");
        UIManager.Instance.HidePanel("UI/���˵�panel/MainPanel");

    }


}
