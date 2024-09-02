using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public enum UI_Layer
    {
        Bot,
        Mid,
        Top,
    }
    /// <summary>
    /// 注意这个Dic是panel名字和panel脚本对应的。为什么非得是脚本不是那个panel本身？因为脚本可以通过.gameObject的方式获取到本身。
    /// </summary>
    public Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();
    public CanvasGroup canvasgroup;
    public TextMeshProUGUI LoadingText;
    public Transform Black;

    private Transform bot;
    private Transform mid;
    private Transform top;
    //private Transform system;
    public RectTransform canvas;
    private Animator anim;
    //  暂时不知道Eventsystem干吗用的，先注释了不影响大局。


    public UIManager()
    {
        //  本地加载唯一的Canvas进来
        GameObject obj = ResMgr.Instance.Load<GameObject>("UI/Canvas");
        // 把这个canvas对应到我们这个单例类里来，然后永远不要销毁。
        canvas = obj.transform as RectTransform;
        GameObject.DontDestroyOnLoad(obj);
        //  obj = ResMgr.Instance.Load<GameObject>("UI/EventSystem");

        //  下面这个Find是查找儿子里符合名字的并返回对应的transform，所以这个canvas作为预制体，一开始里面就得事先从上到下按到最下面一层，中间层，最上面的层的方式创建好对应的3个子物体。
        bot = canvas.Find("bot");
        mid = canvas.Find("mid");
        top = canvas.Find("top");
        //system = canvas.Find("System");
        // Find是transform的方法，返回的也是transform
        // 获取黑色幕布
        Black = canvas.Find("Black");
        //Black.gameObject.SetActive(false);

        LoadingText = canvas.Find("Loading").GetComponent<TextMeshProUGUI>();
        LoadingText.gameObject.SetActive(false);
        canvasgroup = canvas.GetComponent<CanvasGroup>();
        anim = canvas.GetComponent<Animator>();
        EventCenter.Instance.AddListener("场景切换进度条", EventLoading);
    }
    /// <summary>
    /// 加载并显示一个Panel，然后要在外面调用的时候决定把面板放在具体的层上,所以传递第二个参数layer
    /// </summary>
    /// <param name="panelName">要加载的panel是什么？</param>
    /// <param name="layer">放哪一层上？</param>
    /// <param name="func">对panel进行一些处理</param>
    public void ShowPanel<T>(string panelName, UI_Layer layer, UnityAction<T> func = null) where T:BasePanel
    {
        if (panelDic.ContainsKey(panelName))
        {
            //panelDic[panelName].ShowMe();
            Debug.Log("UIpaneldic中已拥有此panel了" + panelName);
            //直接调用func，但是这里很容易用错
            if (func != null)
            {
                func(panelDic[panelName] as T);
            }

            return;
        }


        // 这里的lambda函数的obj，如果是GameObject类型，就会在场景里实例化出来，并使用一个GameObject类型，否则不场景实例化直接使用对应类型。
        // 也就是说正常情况下 obj是一个已经实例化了的那个被加载资源的GameObject物体。
        ResMgr.Instance.UILoadAsyn<GameObject>(panelName, (obj) =>   
        {

            // 能认为到这里异步资源一定加载完成了吗？
            Debug.Log(" ShowPanel加载完成"+ panelName);
            //加载进来后先找到传递的layer，一会儿要把obj的父类设置成father
            Transform father = bot;
            switch (layer)
            {
                case UI_Layer.Mid:
                    father = mid;
                    break;
                case UI_Layer.Top:
                    father = top;
                    break;
                    //这里省略一个system的暂时用不上
            }
            //这句话真的要加吗？SetParent false？ UI可以加的，因为UI本身就是做好的数值，肯定不希望它拖动的时候数值改变。
            // 未来可以加一个功能，就是你想让这个UI实例化到某个transform下面
            obj.transform.SetParent(father, false);
            //obj.transform.SetParent(father, true);


            //obj.transform.SetParent(father);

            //obj.transform.localScale = Vector3.one;

            // 这两句话使用存疑，等会儿细看
            //(obj.transform as RectTransform).offsetMax = Vector2.zero;
            //(obj.transform as RectTransform).offsetMin = Vector2.zero;

            //得到预设体身上的脚本，所以这就要求加载进来的预设体panel身上必须有对应的脚本，这个脚本
            //是我们自己做预制体的时候，自己拉上去的。

            T panel = obj.GetComponent<T>();

            if (func != null)
            {
                func.Invoke(panel);
            }
            //panel.ShowMe();

            panelDic.Add(panelName, panel);

            //Debug.Log(UIManager.Instance.panelDic.Count);


        });
    }


    /// <summary>
    /// 彻底Destory掉一个panel。
    /// </summary>
    /// <param name="name"></param>
    public void HidePanel(string name)
    {
        if (panelDic.ContainsKey(name))
        {
            //有这个hideme吗？
            //Debug.Log("找到要删除的panel了");

            panelDic[name].HideMe();
            //Debug.Log("Panel Destory之前");
            GameObject.Destroy(panelDic[name].gameObject);//这里取出来的是脚本，所以加个gameobject是回到面板本身
            //Debug.Log("Panel Destory之后");
            panelDic.Remove(name);
        }
        else
        {
            Debug.Log("未找到panel，无法删除");
        }
    }

    /// <summary>
    /// 泛型获取面板，根据名字和面板类型获取对应的面板。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T GetPanel<T>(string name)where T:BasePanel
    {
        //Debug.Log(panelDic.Count);
        if (panelDic.ContainsKey(name))
        {
            return panelDic[name] as T;
        }
        Debug.Log("未找到对应的Panel名称" + name +"接下来打印Dic中所有的panel" + "当前dic中的panel数量：" + panelDic.Count);
        foreach (var pname in panelDic.Keys)
        {
            Debug.Log(pname);
        }
        Debug.Log("打印结束");

        return null;
    }

    public Transform GetLayer(UI_Layer layer)
    {
        switch(layer)
        {
            case UI_Layer.Top:
                return this.top;
            case UI_Layer.Mid:
                return this.mid;
            case UI_Layer.Bot:
                return this.bot;


        }
        return null;
    }


    public void FadeCanvas()
    {
        // 禁止一切UI交互，屏幕开始变暗。
        canvasgroup.interactable = false;
        anim.SetBool("GoToAlpha0", true);
    }

    public void UnFadeCanvas()
    {
        // 允许UI交互，屏幕开始变亮。
        canvasgroup.interactable = true;
        anim.SetBool("GoToAlpha0", false);
    }


    /// <summary>
    /// 加载场景的时候progress<0.9f的时候，调用的函数，目的是为了加一些Loading上面的元素
    /// </summary>
    /// <param name="progress"></param>
    private void EventLoading(object progress)
    {
        // 目前使用起来只能这样强制转换
        //Debug.Log("转换类型");
        float pro = (float)(progress);
        //Debug.Log("进度"+pro);
        if (pro >= 0.9f)
        {
            LoadingText.text = "Loading " + 100 + "%";
        }
        else
        {
            LoadingText.text = "Loading " + pro * 100 + "%";
        }
    }


    // 切换Canvas的Render Camera  
    public void SetCanvasRenderCamera(Camera newCamera)
    {
        if (canvas.gameObject != null)
        {
            // 设置Canvas的Render Camera    SetRenderCamera(newCamera);
            canvas.GetComponent<Canvas>().worldCamera = newCamera;
            // 如果你还想让Canvas的Plane Distance与新的Camera匹配，可以额外设置  
            // 注意：这通常只在Canvas的Render Mode为Screen Space - Camera且Camera不是主相机时有用  1
            // mainCanvas.planeDistance = newCamera.nearClipPlane; // 谨慎使用，根据实际情况调整  
        }
    }

}
