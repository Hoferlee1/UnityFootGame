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
        Debug.Log("��ʼ����");
        CreateMonsterCoro = StartCoroutine(RespawnMonster());
        if (CreateMonsterCoro == null)
        {
            Debug.LogError("StartCreateMonsterЭ������ʧ�ܣ�");
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
                    // �����ѡN�Ρ�
                    int pos = Random.Range(0, transform.childCount);

                    if (dic["Monster"].CountInactive == 0 && dic["Monster"].CountAll >= maxSizeNormalMonster)
                    {
                        // ����û�����õ���
                        //Debug.Log("û�����õ�"+ dic["Monster"].CountInactive);
                    }
                    else
                    {
                        // �����ѡһ���㡣ˢһ����
                        //Debug.Log("ˢ��");

                        GameObject obj = dic["Monster"].Get();
                        obj.transform.position = transform.GetChild(pos).position;
                        obj.transform.SetParent(MonsterCheck.transform);
                        //Debug.Log("get�����������ڳ���");
                        EventCenter.Instance.EventTrigger("����Ŀǰ��������", dic["Monster"].CountActive);
                    }


                }
            }
            yield return new WaitForSeconds(RespawnDeltaTime);
        }

    }

    #region ��ʼ��pool��
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
    /// ʧ�ܺ�ʤ�����浯����ʱ�򣬶�Ҫִ�������������ֹͣ��Ϸ�ڵ�һ����Ϊ��
    /// </summary>
    public void StopCreate(object i)
    {
        if(i!=null)
        {
            canRespawn = false;
            Debug.Log("Э����false����ͣ��Ϸ����");
        }
        else
        {
            Debug.Log("ˢ��Э�̹رգ�����ֹͣ����");
            StopCoroutine(CreateMonsterCoro);
            canRespawn = false;
        }
    }

    private void Continue(object i)
    {
        canRespawn = true;
        Debug.Log("pool������Ϸ");

    }


    private void OnEnable()
    {
        //Debug.Log("Poolע��ֹͣ��Ϸ");

        EventCenter.Instance.AddListener("ֹͣ��Ϸ", StopCreate);  
        EventCenter.Instance.AddListener("������Ϸ", Continue);

    }

    private void OnDisable()
    {
        
        EventCenter.Instance.RemoveListener("ֹͣ��Ϸ", StopCreate);
        EventCenter.Instance.RemoveListener("������Ϸ", Continue);

    }

}
