using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : Singleton<Setting>
{
    public List<bool> IsSlotSaved;
    /// <summary>
    /// �ò����ڽ̳̹ؽ����󣬲Żᱻ���ó�true��
    /// </summary>
    public bool IsPlayed;

    /// <summary>
    /// ��ǰ�������Զ�Ĺؿ���Ĭ��Ϊ1
    /// </summary>
    public int MissionProgress;


    public int LastPlaySlot;


    public Setting()
    {
        // ÿ�ν���Ϸ��Debuglog�������ȼ�������ĵ�λ
        LastPlaySlot = PlayerPrefs.GetInt("LastPlaySlot", 0);
        if (LastPlaySlot != 0)
        {
            // ��Ϊ0˵�����ǵ�һ���� �Զ������ϴεĴ浵
            Load(LastPlaySlot.ToString());


        }
        else
        {
            // ��һ�������ﲻ�ü����κδ浵�����ǵó�ʼ��Slot
            IsSlotSaved = new List<bool>();
            for (int i = 0; i < 11; ++i)
            {
                IsSlotSaved.Add(false);
            }
            IsPlayed = false;
            MissionProgress = 1;

            Debug.Log("��һ���棬Slot��ʼ�����");


        }

    }


    public void PrintLod()
    {
        Debug.Log("��ʼ������������");
    }

    public void Save(string slot)
    {
        //�洢List
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


        //�洢bool
        if (IsPlayed)
        {
            // 1��������
            PlayerPrefs.SetInt(slot + "IsPlayed", 1);
        }
        else
        {
            PlayerPrefs.SetInt(slot + "IsPlayed", 0);
        }
        //�洢�ؿ�����
        PlayerPrefs.SetInt(slot + "MissionProgress", MissionProgress);

        //TODO��������...����

        PlayerPrefs.SetInt("LastPlaySlot", LastPlaySlot);
        //�洢������ע�����
        PlayerPrefs.Save();
    }


    public void Load(string slot)
    {

        //����List
        for (int i = 0; i < 11; ++i)
        {
            // Ĭ����0
            int temp = PlayerPrefs.GetInt(slot + "IsSlotSaved_" + i, 0);
            if (temp == 1)
            {
                // 1��������
                IsSlotSaved[i] = true;
            }
            else
            {  // 0����û��
                IsSlotSaved[i] = false;
            }
        }



        //����bool
        int t = PlayerPrefs.GetInt(slot + "IsPlayed", 0);
        if (t == 0)
        {
            //û���
            IsPlayed = false;
        }
        else
        {
            IsPlayed= true; 
        }
        //���عؿ����ȣ� Ĭ���ǵ�һ��1
        MissionProgress = PlayerPrefs.GetInt(slot + "MissionProgress", 1);

        //TODO��������...���ü���


    }



    // TODO���Ƿ�ʹ���˴浵��ͳ������ӱ��ؼ��أ���������new
    //IsSlotSaved= new List<bool>();
    //for(int i = 0; i < 11; ++i)
    //{
    //    IsSlotSaved.Add(false);
    //}

    // TODO�� ��¼�Ƿ��ǵ�һ���棬�����ǵøĳɴӱ��ػ�ȡ����
    //IsPlayed = true;
}
