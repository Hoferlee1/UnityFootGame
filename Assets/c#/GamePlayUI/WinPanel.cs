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
        KilledEnemy = SearchChildUI<TextMeshProUGUI>("已消灭");
        DestroyedBuilding = SearchChildUI<TextMeshProUGUI>("已摧毁");
        CostTime = SearchChildUI<TextMeshProUGUI>("用时");
        Score = SearchChildUI<TextMeshProUGUI>("得分");

        // 记录得分
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
            // TODO：返回主菜单,要切场景了。
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
        Debug.Log("变暗动画结束，call event");

        //以下代码等完全变暗的动画结束后，开始加载场景
        //加载完场景后 删除之前的场景，然后show出来要显示的UI，
        //然后先解禁UI+变亮

        UIManager.Instance.LoadingText.gameObject.SetActive(true);
        SceneMgr.Instance.LoadSceneAsyn("MainMenu", SceneManager.GetActiveScene(), () =>
        {
            //必须等加载完了，显示出一个游戏内的基本UI来，再UnFade
            //回主菜单的时候不需要再加载一次了。
            //UIManager.Instance.ShowPanel<IntroGamePanel>("UI/主菜单panel/MainPanel", UIManager.UI_Layer.Bot);
            UIManager.Instance.UnFadeCanvas();
            UIManager.Instance.LoadingText.gameObject.SetActive(false);
        });
        //到这里又一个易错的地方，看到异步加载的东西，思考哪些可以并行着做，哪些必须放在Load之后做，后者得写进一个函数里然后传递给LoadScene。
        //下面这个关闭主页的panel可以并行着做
        UIManager.Instance.HidePanel("UI/游戏内panel/WinPanel");
        UIManager.Instance.HidePanel("UI/游戏内panel/IntroGamePanel");
        EventCenter.Instance.CheckCount();


    }




}
