using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroGamePanel : BasePanel
{
    private TextMeshProUGUI Count;
    public override void Start()
    {
        base.Start();
        //���Ҳ�����
        Count = SearchChildUI<TextMeshProUGUI>("Ŀǰ��������");
        //TODO:��ʵѪ��Ҳ���Զ�������ű����������������ˡ�





    }
    public override void OnClick(string name)
    {
        base.OnClick(name);
        // TODO:׼�������panelҳ�������ֽ̡̳�������






    }

    private void OnEnable()
    {
        EventCenter.Instance.AddListener("����Ŀǰ��������", OnKillEnemy);
    }
    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener("����Ŀǰ��������", OnKillEnemy);
    }

    private void OnKillEnemy(object num)
    {
        //����һ�£���ȷ���ĸ���ʹ������
        //Count.GetComponent<TextMeshProUGUI>().text = num.ToString();
        //Debug.Log("�������ݸ���Ϊ��"+ num.ToString());
        Count.text = num.ToString();
    }

}
