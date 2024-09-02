using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitPanel : BasePanel
{
    public override void OnClick(string name)
    {
        base.OnClick(name);
        switch (name)
        {
            case "是的":
                Application.Quit();
                break;
            case "不":
                // 关闭这一页，解开上一页。
                // 先show上一层再hide自己。
                UIManager.Instance.GetPanel<MainPanel>("UI/主菜单panel/MainPanel").ShowMe();
                UIManager.Instance.HidePanel("UI/主菜单panel/QuitPanel");
                break;
        }
    }





}
