using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveBankPanel : BasePanel
{
    override public void Start()
    {
        base.Start();
        // TODO:������浵ҳ���ʱ��Ӧ�ö�ȡ���ص��ļ�������ÿ���浵����Ϣȥ����ÿ��slot�����
        // txt��Ϣ��




    }
    public override void OnClick(string name)
    {
        base.OnClick(name);
        switch (name)
        {
            case "X":
                // �⿪��ҳ���blockȻ����մ浵ҳ��
                MainPanel mainpanel  = UIManager.Instance.GetPanel<MainPanel>("UI/���˵�panel/MainPanel");
                mainpanel.ShowMe();
                UIManager.Instance.HidePanel("UI/���˵�panel/SaveBankPanel");
                break;
            case "SaveSlot1":
                

                //  TODO: ֻ�ܶ�ȡ�������������浵��ֻ����Ϸ�ؿ��Զ�������ʱ��浵��
                //  �����Ƿ񸲸ǡ�Ȼ����ں�������ǵ�ʱ�򣬵û������������ӦSlot�ϵ��ı���
                //  �������Ĵ浵��1�ţ�ѡ������̱��浽���ء�

                // ���ж��Ƿ��ǿյ�����Ϊ����ֻ���ж�ȡ��������һ���жϺ���Ҫ
                if (Setting.Instance.IsSlotSaved[1])
                {
                    // COPYTODO : ÿ����ť����ֵ��ͬ�� 

                    //���ǿյ��������оɵ���ֱ�����Ƿ��ȡ���ɡ�
                    Debug.Log("debugȷ�Ͻ������濪ʼshow��");
                    UIManager.Instance.ShowPanel<ConfirmSavePanel>("UI/���˵�panel/ConfirmSavePanel", UIManager.UI_Layer.Top,(obj)=>
                    {
                        //UIManager.Instance.GetPanel<ConfirmSavePanel>("UI/���˵�panel/ConfirmSavePanel").slot = 1;
                        // ������仰ΪʲôҪ�ĳ���������أ���Ϊ����lambda������showpanel������Ǻ����Ⱥ������ٷ����paneldic��
                        // �����д����������������û����dic�أ�����ں������ﳢ�Դ�dic��get�����panel�������Ǽ���dic�Ǻ������һ�仰
                        // ��show��ʱ������������ֱ���õ�panel��������ֱ�Ӵ���panel���ɣ�������get�ˡ�obj�������panel����
                        obj.slot = 1;
                    });

                    // ������仰����ȼ�����ɲ��ܵ��ã����Է�������
                    //UIManager.Instance.GetPanel<ConfirmSavePanel>("UI/���˵�panel/ConfirmSavePanel").slot = 1;

                }
                else
                {
                    // �ǿյ�
                    // �ж��Ƿ��һ���棬��һ���棬ѡ���˿յ���ԭ�ؽ������ɣ�����ת�������̳̹�
                    if (!Setting.Instance.IsPlayed)
                    {
                        Debug.Log("��һ���棬ѡ���˿յ�");
                        // TODO ���޸�Slot�ϵ��ı������������Ӧ���Ƴٵ��¸�ȷ��panel����
                        // �޸�Setting�е�SlotSaved

                        UIManager.Instance.ShowPanel<ConfirmSavePanel>("UI/���˵�panel/ConfirmSavePanel", UIManager.UI_Layer.Top, (obj) =>
                        {
                            // COPYTODO : ÿ����ť����ֵ��ͬ�� 
                            obj.slot = 1;
                        });
                        //UIManager.Instance.GetPanel<ConfirmSavePanel>("UI/���˵�panel/ConfirmSavePanel").slot = 1;

                        // TODO: ��������ת���ؿ�ѡ�����

                    }
                    else
                    {
                        // ���ǵ�һ���棬����ѡ���˿յ���˵�������ǴӴ浵��������ģ����·������ɡ�
                        Debug.Log("���Ǹ��յ����޷���ȡ");
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
