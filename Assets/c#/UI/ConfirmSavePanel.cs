using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConfirmSavePanel : BasePanel
{
    /// <summary>
    /// 只有点开存档界面的slot才会有这个界面，所以这项记录刚才是哪个slot？
    /// </summary>
    public int slot = 0;


    public override void OnClick(string name)
    {
        base.OnClick(name);

        switch (name)
        {
            case "是的":
                // 永远是读取功能
                // 如果是第一次玩，进教程关。
                // TODO: 记得判断逻辑这里改回来，加个！号，我们要测试下面那个能否正常进入...所以暂时去了。
                if (!Setting.Instance.IsPlayed) 
                {
                    // TODO：根据slot值修改上个panel中的文本TXT。这里铁第一次玩，所以修改为时间+进度0/30即可。
                    // 启用变暗动画，变暗动画结束后 用协程判断是否完全变暗了，完全变暗后再加载场景。
                    // 启动变暗动画放到下面协程里了
                    // 关闭当前panel这句话绝对不能先写，这是一个Destory过程。
                    // 因为我们现在是依赖这句话执行完了再开启协程的。。。但是视觉上又会很别扭，怎么办呢？
                    // UIManager.Instance.HidePanel("UI/主菜单panel/ConfirmSavePanel");
                    Debug.Log("第一次玩，调用切换场景");
                    // 方便多次游玩教程关，暂时把下面这句话注释了
                    // Setting.Instance.IsPlayed = true;
                    MonoMgr.Instance.StartSingleCoroutine(GotoGamePlayIntro());
                    UIManager.Instance.HidePanel("UI/主菜单panel/ConfirmSavePanel");
                    //StartCoroutine(GotoGamePlayIntro());

                }
                else
                {
                    // 不是第一次玩，那就是加载老存档Setting.Load
                    // LoadScene，Load完成后进关卡选择画面 
                    // TODO:协程方法还没写。
                    Debug.Log("老存档，进关卡选择画面");
                    Setting.Instance.Load(Setting.Instance.LastPlaySlot.ToString());
                    Setting.Instance.LastPlaySlot = slot;
                    MonoMgr.Instance.StartSingleCoroutine(GoToMissionMap());
                    UIManager.Instance.HidePanel("UI/主菜单panel/ConfirmSavePanel");

                }
                break;
            case "不":
                // 关闭这一页，解开上一页。
                // 先show上一层再hide自己。
                UIManager.Instance.GetPanel<SaveBankPanel>("UI/主菜单panel/SaveBankPanel").ShowMe();
                UIManager.Instance.HidePanel("UI/主菜单panel/ConfirmSavePanel");
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
        Debug.Log("变暗动画结束，call event");

        //以下代码等完全变暗的动画结束后，开始加载场景
        //加载完场景后 删除之前的场景，然后show出来要显示的UI，
        //然后先解禁UI+变亮

        UIManager.Instance.LoadingText.gameObject.SetActive(true);
        SceneMgr.Instance.LoadSceneAsyn("GamePlayIntro", SceneManager.GetActiveScene(), () =>
        {
            //必须等加载完了，显示出一个游戏内的基本UI来，再UnFade
            UIManager.Instance.ShowPanel<IntroGamePanel>("UI/游戏内panel/IntroGamePanel", UIManager.UI_Layer.Bot);
            //EventCenter.Instance.EventTrigger("进入游戏场景绑定虚拟相机", null);
            UIManager.Instance.ShowPanel<TeachPanel>("UI/游戏内panel/TeachPanel", UIManager.UI_Layer.Mid);
            UIManager.Instance.UnFadeCanvas();
            UIManager.Instance.LoadingText.gameObject.SetActive(false);
        });
        //到这里又一个易错的地方，看到异步加载的东西，思考哪些可以并行着做，哪些必须放在Load之后做，后者得写进一个函数里然后传递给LoadScene。
        //下面这个关闭主页的panel可以并行着做
        UIManager.Instance.HidePanel("UI/主菜单panel/SaveBankPanel");
        UIManager.Instance.HidePanel("UI/主菜单panel/MainPanel");
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
        UIManager.Instance.HidePanel("UI/主菜单panel/SaveBankPanel");
        UIManager.Instance.HidePanel("UI/主菜单panel/MainPanel");

    }


}
