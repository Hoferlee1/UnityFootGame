using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    // 每个房子下面子物体有三个，分别代表了三种状态。每一时刻只能显示其中一种状态。
    public enum HouseExtent{
        Perfect,
        HalfDes,
        TotalDes
    }
    private GameObject son0;
    private GameObject son1;
    private GameObject son2;



    public HouseExtent currentExtent;
    private BoxCollider2D box;
    
    void Start()
    {
        currentExtent = HouseExtent.Perfect;
        box = GetComponent<BoxCollider2D>();
        son0 = transform.GetChild(0).gameObject;
        son1 = transform.GetChild(1).gameObject;
        son2 = transform.GetChild(2).gameObject;
        if (son0.activeSelf && son1.activeSelf && son2.activeSelf)
        {
            Debug.LogError("building里面三个外观全开启了，请只开启第一个完整外观。");
        }
        son0.SetActive(true);
        son1.SetActive(false);
        son2.SetActive(false);
    }

    public void CloseColliderBox()
    {
        box.enabled = false;
    }
    /// <summary>
    /// 变换到指定外观
    /// </summary>
    /// <param name="t"></param>
    public void ChangeSpiriteTo(Building.HouseExtent t)
    {
        if(t == HouseExtent.HalfDes)
        {
            son0.SetActive(false);
            son1.SetActive(true);
            son2.SetActive(false);

        }
        else if(t == HouseExtent.TotalDes)
        {
            son0.SetActive(false);
            son1.SetActive(false);
            son2.SetActive(true);
        }
    }

    public void StartHpMonster()
    {
        StartCoroutine(CreateHpMonster());
    }

    IEnumerator CreateHpMonster()
    {
        yield return new WaitForSeconds(2);
        // TODO:随机产生1-3只HP怪物
        int num = Random.Range(1, 4);
        for(int i = 0;i< num;i++)
        {

            if (Pool.Instance.dic["HpMonster"].CountInactive == 0 && Pool.Instance.dic["HpMonster"].CountAll >= Pool.Instance.maxSizeHpMonster)
            {
                // Pool不可以产生；
                Debug.Log("没有产生");
            }
            else
            {
                GameObject obj = Pool.Instance.dic["HpMonster"].Get();
                obj.transform.position = transform.position;
                obj.transform.SetParent(Pool.Instance.MonsterCheck.transform);
            }

        }


    }

}
