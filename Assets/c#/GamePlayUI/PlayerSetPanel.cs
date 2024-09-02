using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetPanel : BasePanel
{

    private void Update()
    {
        //TODO: 监听是否有空格输入，有的话加载对应关卡。
    }
    public override void OnClick(string name)
    {
        base.OnClick(name);
        switch (name)
        {
            case "X":

                // 解开地图选择界面的block然后回收存档页面
                UIManager.Instance.GetPanel<MissionPanel>("UI/游戏内panel/MissionPanel").ShowMe();

                UIManager.Instance.HidePanel("UI/游戏内panel/PlayerSetPanel");

                break;
            case "1":
                // 其余功能的按钮待定。
                break;
            case "2":
                break;
            case "3":
                break;
            case "4":
                break;
        }


    }
}
