using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetPanel : BasePanel
{

    private void Update()
    {
        //TODO: �����Ƿ��пո����룬�еĻ����ض�Ӧ�ؿ���
    }
    public override void OnClick(string name)
    {
        base.OnClick(name);
        switch (name)
        {
            case "X":

                // �⿪��ͼѡ������blockȻ����մ浵ҳ��
                UIManager.Instance.GetPanel<MissionPanel>("UI/��Ϸ��panel/MissionPanel").ShowMe();

                UIManager.Instance.HidePanel("UI/��Ϸ��panel/PlayerSetPanel");

                break;
            case "1":
                // ���๦�ܵİ�ť������
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
