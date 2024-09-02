using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveBankPanel : BasePanel
{
    override public void Start()
    {
        base.Start();
        // TODO:打开这个存档页面的时候，应该读取本地的文件，根据每个存档的信息去更改每个slot上面的
        // txt信息。




    }
    public override void OnClick(string name)
    {
        base.OnClick(name);
        switch (name)
        {
            case "X":
                // 解开主页面的block然后回收存档页面
                MainPanel mainpanel  = UIManager.Instance.GetPanel<MainPanel>("UI/主菜单panel/MainPanel");
                mainpanel.ShowMe();
                UIManager.Instance.HidePanel("UI/主菜单panel/SaveBankPanel");
                break;
            case "SaveSlot1":
                

                //  TODO: 只能读取，不可以主动存档，只有游戏关卡自动结束的时候存档。
                //  弹出是否覆盖。然后对于后面点了是的时候，得回来更新这个对应Slot上的文本。
                //  最近游玩的存档是1号，选择后立刻保存到本地。

                // 先判断是否是空档，因为我们只进行读取，所以这一步判断很重要
                if (Setting.Instance.IsSlotSaved[1])
                {
                    // COPYTODO : 每个按钮的右值不同。 

                    //不是空档，里面有旧档，直接问是否读取即可。
                    Debug.Log("debug确认界面下面开始show了");
                    UIManager.Instance.ShowPanel<ConfirmSavePanel>("UI/主菜单panel/ConfirmSavePanel", UIManager.UI_Layer.Top,(obj)=>
                    {
                        //UIManager.Instance.GetPanel<ConfirmSavePanel>("UI/主菜单panel/ConfirmSavePanel").slot = 1;
                        // 上面这句话为什么要改成下面这句呢？因为本身lambda函数在showpanel里面就是后处理，先后处理了再放入的paneldic中
                        // 如果你写成上面那样，他还没加入dic呢，你就在后处理函数里尝试从dic中get到这个panel，问题是加入dic是后处理的下一句话
                        // 而show的时候我们正好能直接拿到panel本身，所以直接处理panel即可，不用再get了。obj这个就是panel本身。
                        obj.slot = 1;
                    });

                    // 下面这句话必须等加载完成才能调用，所以放上面了
                    //UIManager.Instance.GetPanel<ConfirmSavePanel>("UI/主菜单panel/ConfirmSavePanel").slot = 1;

                }
                else
                {
                    // 是空档
                    // 判断是否第一次玩，第一次玩，选择了空档，原地建档即可，并跳转到后续教程关
                    if (!Setting.Instance.IsPlayed)
                    {
                        Debug.Log("第一次玩，选择了空挡");
                        // TODO ：修改Slot上的文本，但是这件事应该推迟到下个确认panel在做
                        // 修改Setting中的SlotSaved

                        UIManager.Instance.ShowPanel<ConfirmSavePanel>("UI/主菜单panel/ConfirmSavePanel", UIManager.UI_Layer.Top, (obj) =>
                        {
                            // COPYTODO : 每个按钮的右值不同。 
                            obj.slot = 1;
                        });
                        //UIManager.Instance.GetPanel<ConfirmSavePanel>("UI/主菜单panel/ConfirmSavePanel").slot = 1;

                        // TODO: 接下来跳转到关卡选择界面

                    }
                    else
                    {
                        // 不是第一次玩，但是选择到了空档，说明这里是从存档界面进来的，无事发生即可。
                        Debug.Log("这是个空档，无法读取");
                    }


                }



                break;

        }
    }

    public override void HideMe()
    {
        base.HideMe();
       


    }

}
