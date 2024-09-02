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
        Debug.Log("关卡UI总数"+uiDic.Count);
        //根据当前Setting中的进度，Active对应的button。
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
                // 返回主菜单 切换场景
                MonoMgr.Instance.StartSingleCoroutine(GotoMainMenu());
                break;
            case "1":
                // TODO :打开角色书UI并提示按空格继续，现在缺这个文字提示。 如果继续了的话，在那个页面上考虑加载对应关卡 
                // 明天想这里的程序逻辑，又涉及到加载了。。
                HideMe();
                UIManager.Instance.ShowPanel<PlayerSetPanel>("UI/游戏内panel/PlayerSetPanel", UIManager.UI_Layer.Top);

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
        Debug.Log("变暗动画结束，call event");

        //以下代码等完全变暗的动画结束后，开始加载场景
        //加载完场景后 删除之前的场景，然后show出来要显示的UI，
        //然后先解禁UI+变亮

        UIManager.Instance.LoadingText.gameObject.SetActive(true);
        SceneMgr.Instance.LoadSceneAsyn("MainMenu", SceneManager.GetActiveScene(), () =>
        {
            //必须等加载完了，显示出一个游戏内的基本UI来，再UnFade
            UIManager.Instance.ShowPanel<MainPanel>("UI/主菜单panel/MainPanel", UIManager.UI_Layer.Bot);
            UIManager.Instance.UnFadeCanvas();
            UIManager.Instance.LoadingText.gameObject.SetActive(false);
        });
        //到这里又一个易错的地方，看到异步加载的东西，思考哪些可以并行着做，哪些必须放在Load之后做，后者得写进一个函数里然后传递给LoadScene。
        //下面这个关闭主页的panel可以并行着做
        UIManager.Instance.HidePanel("UI/游戏内panel/MissionPanel");


    }

}
