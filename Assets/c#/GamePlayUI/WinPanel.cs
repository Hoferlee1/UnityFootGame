using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPanel : BasePanel
{
    private bool Isspaced;
    private MapControl mapcontrolScript;
    private TextMeshProUGUI KilledEnemy;
    private TextMeshProUGUI DestroyedBuilding;
    private TextMeshProUGUI CostTime;
    private TextMeshProUGUI Score;


    public override void OnClick(string name)
    {
        base.OnClick(name);


    }


    public override void Start()
    {
        base.Start();
        Isspaced = false;
        mapcontrolScript = FindAnyObjectByType<MapControl>();
        KilledEnemy = SearchChildUI<TextMeshProUGUI>("������");
        DestroyedBuilding = SearchChildUI<TextMeshProUGUI>("�Ѵݻ�");
        CostTime = SearchChildUI<TextMeshProUGUI>("��ʱ");
        Score = SearchChildUI<TextMeshProUGUI>("�÷�");

        // ��¼�÷�
        KilledEnemy.text = mapcontrolScript.KilledEnemy.ToString();
        DestroyedBuilding.text = mapcontrolScript.DestoryedBuilding.ToString();
        //CostTime.text = 
        int min = (int)mapcontrolScript.CostTime / 60;
        int second = (int)mapcontrolScript.CostTime % 60;
        CostTime.text = $"{min}m{second}s";

        int score = 10 * mapcontrolScript.KilledEnemy + 20 * mapcontrolScript.DestoryedBuilding;
        Score.text = score.ToString();

    }

    private void Update()
    {
        
        if (!Isspaced && Input.GetKeyDown(KeyCode.Space))
        {
            Isspaced = true;
            // TODO���������˵�,Ҫ�г����ˡ�
            MonoMgr.Instance.StartSingleCoroutine(GotoMain());
        }

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
        UIManager.Instance.HidePanel("UI/��Ϸ��panel/WinPanel");
        UIManager.Instance.HidePanel("UI/��Ϸ��panel/IntroGamePanel");
        EventCenter.Instance.CheckCount();


    }




}
