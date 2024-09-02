using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 面板基类，可以按UI名字检索到具体的UI按钮对象，可以显示自己，可以隐藏自己。
/// </summary>
public class BasePanel : MonoBehaviour
{
    public Dictionary<string, List<UIBehaviour>> uiDic = new Dictionary<string, List<UIBehaviour>>();

    private CanvasGroup canvasGroup;


    virtual protected void Awake()
    {


        //每次激活自动获取下面的所有UI子控件，加入到Dic中。
        FindChildUi<Button>();
        FindChildUi<Image>();
        FindChildUi<Text>();
        FindChildUi<TextMeshProUGUI>();
        FindChildUi<Toggle>();
        FindChildUi<Slider>();
        FindChildUi<ScrollRect>();

    }
    virtual public void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    /// <summary>
    /// UI管理中当调用ShowPanel的时候，会自动调用面板的ShowMe函数
    /// </summary>
    virtual public void ShowMe()
    {
        canvasGroup.interactable = true;

    }

    /// <summary>
    /// 目前Hide的作用就是开始block禁用按钮。
    /// </summary>
    virtual public void HideMe() 
    {
        // 问题是block了什么时候解开呢？关闭这个的时候记得解开
        // 通过控制此Panel上的canvasgroup关闭此Panel
        canvasGroup.interactable = false;
    }

    /// <summary>
    /// 用于自动添加按钮对应的，不用手动加事件了，继承的具体子类panel里需要在这个方法里重写。
    /// </summary>
    virtual public void OnClick(string name)
    {
        //Debug.Log("basepanel已调用");
    }




    /// <summary>
    /// 对外接口，可用于根据UI名字查找panel下的子UI
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T SearchChildUI<T>(string name)where T : UIBehaviour
    {
        if (uiDic.ContainsKey(name))
        {
            for(int i = 0; i < uiDic[name].Count; ++i)
            {
                if (uiDic[name][i] is T)
                {
                    //Debug.Log("找到UI");
                    return uiDic[name][i] as T;
                }
            }
        }

        Debug.Log("查无此子UI" + name);
        return null;

    }


    /// <summary>
    /// 私有函数，只用于加载的时候自动找出所有的子UI存于uiDic字典中。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private void FindChildUi<T>() where T: UIBehaviour
    {
        T[] temp = gameObject.GetComponentsInChildren<T>(true); // true用于考虑上被禁用的子物体，另外这个函数可以嵌套搜索子物体。
        
        for(int i = 0; i<temp.Length; i++)
        {
            string name = temp[i].gameObject.name;
            if(uiDic.ContainsKey(name)) 
            {
                uiDic[name].Add(temp[i]);
            }
            else
            {
                uiDic.Add(name, new List<UIBehaviour>() { temp[i] });
            }
            //自动给button添加事件 这里遇到了名字的bug，名字只记录最后一个，why？
            if (temp[i] is Button)
            {
                //Debug.Log(name);
                (temp[i] as Button).onClick.AddListener(() =>
                {
                    OnClick(name);
                });

            }

        }
    }


}
