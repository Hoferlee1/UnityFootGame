using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainStart : MonoBehaviour
{

    void Start()
    {
        Setting.Instance.PrintLod();
        // 一进入游戏设置相机并 加载主菜单panel 
        UIManager.Instance.SetCanvasRenderCamera(GameObject.Find("Main Camera").GetComponent<Camera>());
        UIManager.Instance.ShowPanel<MainPanel>("UI/主菜单panel/MainPanel", UIManager.UI_Layer.Bot);
        //test
        //UIManager.Instance.ShowPanel<MainPanel>("UI/testUI/MainPanel2", UIManager.UI_Layer.Bot);

        // 理论上这个应该加载完后，再执行下面的代码，但是并没有这样。。。因为后面要执行的所有内容
        // 最好都写在unityaction里传递给ShowPanel

    }





}
