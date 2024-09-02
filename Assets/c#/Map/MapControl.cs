using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{

    public int  KilledEnemy;
    public int DestoryedBuilding;
    public float CostTime;
    public int Score;

    int AllBuildingNum;
    float CurrentDestory;
    private void Awake()
    {
        // �ܽ������������˽������Ƿ��ǵ�
        // ���������ȡ�����ӽ������������������岻Ҫ���޹صĶ�����(�����ͳ�Ƶ�ֱ��������)
        AllBuildingNum = transform.childCount;
        //Debug.Log("�ܽ�������" + AllBuildingNum);
        CurrentDestory = 0;
    }

    /// <summary>
    /// �ݻٽ�����ʱ��������������������½�������
    /// </summary>
    /// <param name="i"></param>
    private void OnDamageBuilding(object i)
    {
        CurrentDestory = CurrentDestory + 1;
        // ֻҪ�ݻ�80%�Ľ������ɹ��ء�
        //Debug.Log("��ǰ�ݻٽ�������"+ CurrentDestory);
        EventCenter.Instance.EventTrigger("�ؿ�����", CurrentDestory / (AllBuildingNum * 0.8f));
        //Debug.Log("��ǰ���ȣ�" + CurrentDestory / (AllBuildingNum * 0.8f));

    }
    private void OnInitGame(object i)
    {
        KilledEnemy = 0;
        DestoryedBuilding = 0;
        CostTime = 0;
        Score= 0;
    }
    private void Update()
    {
        CostTime += Time.deltaTime;
    }
    private void OnEnable()
    {
        EventCenter.Instance.AddListener("�ݻٽ���", OnDamageBuilding);
        EventCenter.Instance.AddListener("��ʼ������", OnInitGame);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener("�ݻٽ���", OnDamageBuilding);
        EventCenter.Instance.RemoveListener("��ʼ������", OnInitGame);
    }


}
