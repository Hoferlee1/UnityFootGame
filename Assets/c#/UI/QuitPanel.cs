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
            case "�ǵ�":
                Application.Quit();
                break;
            case "��":
                // �ر���һҳ���⿪��һҳ��
                // ��show��һ����hide�Լ���
                UIManager.Instance.GetPanel<MainPanel>("UI/���˵�panel/MainPanel").ShowMe();
                UIManager.Instance.HidePanel("UI/���˵�panel/QuitPanel");
                break;
        }
    }





}
