using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPanel : BasePanel
{
    /// <summary>
    /// 是否玩过了？用于第一次开始游戏时显示存档，这个变量后续要存到注册表
    /// </summary>


    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 这里直接在Switch里面加入点击按钮后想直接的函数即可，因为OnClick是那个被AddListener的事件，它会根据button的名字来自动去Switch里面执行对应的函数。
    /// </summary>
    /// <param name="name"></param>
    public override void OnClick(string name)
    {
        base.OnClick(name);
        //Debug.Log("mainpanel已调用" + name);
        switch (name){
            case "存档Button":
                //打开存档panel并block自己的页面
                HideMe();
                UIManager.Instance.ShowPanel<SaveBankPanel>("UI/主菜单panel/SaveBankPanel", UIManager.UI_Layer.Mid);
                break;
            case "开始Button":
                StartGame();
                break;
            case "角色Button":
                break;
            case "设置Button":
                break;
            case "退出Button":
                //打开QuitPanel并block自己的页面
                HideMe();
                UIManager.Instance.ShowPanel<QuitPanel>("UI/主菜单panel/QuitPanel", UIManager.UI_Layer.Top);

                break;
        }


    }

    /// <summary>
    /// 用于注册本类的OnClick点击事件
    /// </summary>
    private void StartGame()  
    {
        //  TODO:开始游戏
        // 如果已经玩过这个游戏一次了
        if(Setting.Instance.IsPlayed)
        {
            //TODO:  载入最近的存档设置并跳到关卡选择画面
            Setting.Instance.Load(Setting.Instance.LastPlaySlot.ToString());
            MonoMgr.Instance.StartSingleCoroutine(GoToMissionMap());
        }
        else
        {
            // 第一次来这个游戏，那就得切换存档界面了。
            // 打开存档panel并block自己的页面
            HideMe();
            UIManager.Instance.ShowPanel<SaveBankPanel>("UI/主菜单panel/SaveBankPanel", UIManager.UI_Layer.Mid);
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
        Debug.Log("变暗动画结束，call event");

        //以下代码等完全变暗的动画结束后，开始加载场景
        //加载完场景后 删除之前的场景，然后show出来要显示的UI，
        //然后先解禁UI+变亮

        UIManager.Instance.LoadingText.gameObject.SetActive(true);
        SceneMgr.Instance.LoadSceneAsyn("GamePlayIntro", SceneManager.GetActiveScene(), () =>
        {
            //必须等加载完了，显示出一个游戏内的基本UI来，再UnFade
            UIManager.Instance.ShowPanel<MissionPanel>("UI/游戏内panel/MissionPanel", UIManager.UI_Layer.Mid);
            UIManager.Instance.UnFadeCanvas();
            UIManager.Instance.LoadingText.gameObject.SetActive(false);
        });
        //到这里又一个易错的地方，看到异步加载的东西，思考哪些可以并行着做，哪些必须放在Load之后做，后者得写进一个函数里然后传递给LoadScene。
        //下面这个关闭主页的panel可以并行着做
        UIManager.Instance.HidePanel("UI/主菜单panel/MainPanel");

    }


}
