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
            case "X返回主菜单":
                MonoMgr.Instance.StartSingleCoroutine(GotoMain());
                break;
        }



    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space) && !isSpaced)
        {
            isSpaced = true;
            //  TODO: 按下空格后重新初始化本关。现在先写成返回主菜单
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
        Debug.Log("变暗动画结束，call event");

        //以下代码等完全变暗的动画结束后，开始加载场景
        //加载完场景后 删除之前的场景，然后show出来要显示的UI，
        //然后先解禁UI+变亮
        UIManager.Instance.HidePanel("UI/游戏内panel/IntroGamePanel");
        UIManager.Instance.HidePanel("UI/游戏内panel/DefeatPanel");
        UIManager.Instance.LoadingText.gameObject.SetActive(true);
        SceneMgr.Instance.LoadSceneAsyn("GamePlayIntro", SceneManager.GetActiveScene(), () =>
        {
            //必须等加载完了，显示出一个游戏内的基本UI来，再UnFade
            UIManager.Instance.ShowPanel<IntroGamePanel>("UI/游戏内panel/IntroGamePanel", UIManager.UI_Layer.Bot);
            //EventCenter.Instance.EventTrigger("进入游戏场景绑定虚拟相机", null);
            //UIManager.Instance.ShowPanel<TeachPanel>("UI/游戏内panel/TeachPanel", UIManager.UI_Layer.Mid);
            UIManager.Instance.UnFadeCanvas();
            UIManager.Instance.LoadingText.gameObject.SetActive(false);
            // 重新开始游戏时，允许刷兵
            Pool pool = FindAnyObjectByType<Pool>();
            pool.StartCreateMonster();
            EventCenter.Instance.EventTrigger("继续游戏", null);
            EventCenter.Instance.CheckCount();
        });
        //到这里又一个易错的地方，看到异步加载的东西，思考哪些可以并行着做，哪些必须放在Load之后做，后者得写进一个函数里然后传递给LoadScene。
        //下面这个关闭主页的panel可以并行着做

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
        UIManager.Instance.HidePanel("UI/游戏内panel/DefeatPanel");
        UIManager.Instance.HidePanel("UI/游戏内panel/IntroGamePanel");
        EventCenter.Instance.CheckCount();



    }

}
