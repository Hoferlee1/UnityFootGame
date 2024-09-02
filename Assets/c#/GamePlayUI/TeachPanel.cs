using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachPanel : BasePanel
{
    public List<GameObject> TeachingPages = new List<GameObject>();
    private int PageIndex;
    Player playerScript;
    Pool pool;
    
    public override void Start()
    {
        base.Start();
        PageIndex = 0;
        playerScript = GameObject.Find("Player").GetComponent<Player>();

        pool = FindAnyObjectByType<Pool>();
        // ��ͣ��Ϸ���ơ�
        EventCenter.Instance.EventTrigger("ֹͣ��Ϸ", 1);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //���·�ҳ
            if(PageIndex < TeachingPages.Count - 1)
            {
                //�رյ�ǰҳ������һҳ
                TeachingPages[PageIndex].SetActive(false);
                PageIndex++;
                TeachingPages[PageIndex].SetActive(true);
            }
            else
            {
                //��ǰ�����һҳ�ˣ���ʱ�����������ʽ��ʼ��Ϸ�ˡ�
                //�ر����ҳ��
                // TODO:����һЩ��Ϸ���ݣ�Ŀǰ������������ƶ������ܼ���һ�����ƹ�����ƶ���
                //playerScript.canOperate = true;
                //pool.canRespawn= true;
                EventCenter.Instance.EventTrigger("��ʼ������", null);
                EventCenter.Instance.EventTrigger("������Ϸ", null);
                pool.StartCreateMonster();
                UIManager.Instance.HidePanel("UI/��Ϸ��panel/TeachPanel");

            }


        }
        else if(Input.GetMouseButtonDown(1))
        {
            //���Ϸ�ҳ
            if (PageIndex > 0)
            {
                TeachingPages[PageIndex].SetActive(false);
                PageIndex--;
                TeachingPages[PageIndex].SetActive(true);
            }


        }

    }
}
