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
        // 暂停游戏机制。
        EventCenter.Instance.EventTrigger("停止游戏", 1);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //往下翻页
            if(PageIndex < TeachingPages.Count - 1)
            {
                //关闭当前页，打开下一页
                TeachingPages[PageIndex].SetActive(false);
                PageIndex++;
                TeachingPages[PageIndex].SetActive(true);
            }
            else
            {
                //当前是最后一页了，此时点了左键就正式开始游戏了。
                //关闭这个页面
                // TODO:解锁一些游戏内容，目前限制了人物的移动，可能加入一个限制怪物的移动。
                //playerScript.canOperate = true;
                //pool.canRespawn= true;
                EventCenter.Instance.EventTrigger("初始化本关", null);
                EventCenter.Instance.EventTrigger("继续游戏", null);
                pool.StartCreateMonster();
                UIManager.Instance.HidePanel("UI/游戏内panel/TeachPanel");

            }


        }
        else if(Input.GetMouseButtonDown(1))
        {
            //往上翻页
            if (PageIndex > 0)
            {
                TeachingPages[PageIndex].SetActive(false);
                PageIndex--;
                TeachingPages[PageIndex].SetActive(true);
            }


        }

    }
}
