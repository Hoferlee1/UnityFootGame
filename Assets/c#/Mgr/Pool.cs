using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    public GameObject Monster;
    public GameObject HpMonster;

    public Dictionary<string, ObjectPool<GameObject>> dic;
    public int RespwanCount = 4;
    public int maxSizeNormalMonster = 128;
    public int maxSizeHpMonster = 30;
    public bool canRespawn;
    public GameObject MonsterCheck;
    public int RespawnDeltaTime = 5;
    ObjectPool<GameObject> temp;
    ObjectPool<GameObject> temp2;
    private Coroutine CreateMonsterCoro;

    private static Pool instance;

    public static Pool Instance
    {
        get 
        { 
            return instance; 
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        canRespawn = true;
    }
    private void Start()
    {
        dic = new Dictionary<string, ObjectPool<GameObject>>();
        temp = new ObjectPool<GameObject>(MonsterCreateFunc, actionOnGet, actionOnRelease, actionOnDestory, true, 100, maxSizeNormalMonster); 
        dic.Add("Monster", temp);
        temp2 = new ObjectPool<GameObject>(HpMonsterCreateFunc, actionOnGet, actionOnRelease, actionOnDestory, true, 20, maxSizeHpMonster);
        dic.Add("HpMonster", temp2);

    }

    public void StartCreateMonster()
    {
        Debug.Log("开始出兵");
        CreateMonsterCoro = StartCoroutine(RespawnMonster());
        if (CreateMonsterCoro == null)
        {
            Debug.LogError("StartCreateMonster协程启动失败！");
        }
    }



    IEnumerator RespawnMonster()
    {
        while (true)
        {
            if (canRespawn)
            {
                for (int i = 0; i < RespwanCount; ++i)
                {
                    // 随机挑选N次。
                    int pos = Random.Range(0, transform.childCount);

                    if (dic["Monster"].CountInactive == 0 && dic["Monster"].CountAll >= maxSizeNormalMonster)
                    {
                        // 池子没有能用的了
                        //Debug.Log("没有能用的"+ dic["Monster"].CountInactive);
                    }
                    else
                    {
                        // 随机挑选一个点。刷一个怪
                        //Debug.Log("刷怪");

                        GameObject obj = dic["Monster"].Get();
                        obj.transform.position = transform.GetChild(pos).position;
                        obj.transform.SetParent(MonsterCheck.transform);
                        //Debug.Log("get到对象了正在出兵");
                        EventCenter.Instance.EventTrigger("更改目前敌人数量", dic["Monster"].CountActive);
                    }


                }
            }
            yield return new WaitForSeconds(RespawnDeltaTime);
        }

    }

    #region 初始化pool用
    GameObject MonsterCreateFunc()
    {
        GameObject obj = Instantiate(Monster);
        return obj;
    }
    GameObject HpMonsterCreateFunc()
    {
        GameObject obj = Instantiate(HpMonster);
        return obj;
    }

    void actionOnGet(GameObject obj)
    {
        obj.SetActive(true);
    }

    void actionOnRelease(GameObject obj)
    {
        obj.SetActive(false);

    }

    void actionOnDestory(GameObject obj)
    {
        Destroy(obj);
    }
    #endregion


    /// <summary>
    /// 失败和胜利界面弹出的时候，都要执行这个方法，以停止游戏内的一切行为。
    /// </summary>
    public void StopCreate(object i)
    {
        if(i!=null)
        {
            canRespawn = false;
            Debug.Log("协程内false，暂停游戏出兵");
        }
        else
        {
            Debug.Log("刷兵协程关闭，彻底停止出兵");
            StopCoroutine(CreateMonsterCoro);
            canRespawn = false;
        }
    }

    private void Continue(object i)
    {
        canRespawn = true;
        Debug.Log("pool继续游戏");

    }


    private void OnEnable()
    {
        //Debug.Log("Pool注册停止游戏");

        EventCenter.Instance.AddListener("停止游戏", StopCreate);  
        EventCenter.Instance.AddListener("继续游戏", Continue);

    }

    private void OnDisable()
    {
        
        EventCenter.Instance.RemoveListener("停止游戏", StopCreate);
        EventCenter.Instance.RemoveListener("继续游戏", Continue);

    }

}
