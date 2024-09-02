using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : Singleton<Setting>
{
    public List<bool> IsSlotSaved;
    /// <summary>
    /// 该参数在教程关结束后，才会被设置成true。
    /// </summary>
    public bool IsPlayed;

    /// <summary>
    /// 当前能玩的最远的关卡。默认为1
    /// </summary>
    public int MissionProgress;


    public int LastPlaySlot;


    public Setting()
    {
        // 每次进游戏先Debuglog，进而先加载最近的档位
        LastPlaySlot = PlayerPrefs.GetInt("LastPlaySlot", 0);
        if (LastPlaySlot != 0)
        {
            // 不为0说明不是第一次玩 自动加载上次的存档
            Load(LastPlaySlot.ToString());


        }
        else
        {
            // 第一次玩这里不该加载任何存档，但是得初始化Slot
            IsSlotSaved = new List<bool>();
            for (int i = 0; i < 11; ++i)
            {
                IsSlotSaved.Add(false);
            }
            IsPlayed = false;
            MissionProgress = 1;

            Debug.Log("第一次玩，Slot初始化完成");


        }

    }


    public void PrintLod()
    {
        Debug.Log("初始化并加载设置");
    }

    public void Save(string slot)
    {
        //存储List
        for (int i = 0; i < 11; ++i)
        {
            if (IsSlotSaved[i])
            {
                PlayerPrefs.SetInt(slot + "IsSlotSaved_" + i, 1);
            }
            else
            {
                PlayerPrefs.SetInt(slot + "IsSlotSaved_" + i, 0);
            }
        }


        //存储bool
        if (IsPlayed)
        {
            // 1代表存过了
            PlayerPrefs.SetInt(slot + "IsPlayed", 1);
        }
        else
        {
            PlayerPrefs.SetInt(slot + "IsPlayed", 0);
        }
        //存储关卡进度
        PlayerPrefs.SetInt(slot + "MissionProgress", MissionProgress);

        //TODO：音量等...设置

        PlayerPrefs.SetInt("LastPlaySlot", LastPlaySlot);
        //存储到本地注册表中
        PlayerPrefs.Save();
    }


    public void Load(string slot)
    {

        //加载List
        for (int i = 0; i < 11; ++i)
        {
            // 默认是0
            int temp = PlayerPrefs.GetInt(slot + "IsSlotSaved_" + i, 0);
            if (temp == 1)
            {
                // 1代表存过了
                IsSlotSaved[i] = true;
            }
            else
            {  // 0代表没存
                IsSlotSaved[i] = false;
            }
        }



        //加载bool
        int t = PlayerPrefs.GetInt(slot + "IsPlayed", 0);
        if (t == 0)
        {
            //没玩过
            IsPlayed = false;
        }
        else
        {
            IsPlayed= true; 
        }
        //加载关卡进度， 默认是第一关1
        MissionProgress = PlayerPrefs.GetInt(slot + "MissionProgress", 1);

        //TODO：音量等...设置记载


    }



    // TODO：是否使用了存档的统计数组从本地加载，这里先用new
    //IsSlotSaved= new List<bool>();
    //for(int i = 0; i < 11; ++i)
    //{
    //    IsSlotSaved.Add(false);
    //}

    // TODO： 记录是否是第一次玩，后续记得改成从本地获取数据
    //IsPlayed = true;
}
