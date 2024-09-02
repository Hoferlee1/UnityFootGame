using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Building;

/// <summary>
/// �������ڹ������еĶ����¼���ʹ��ʱ�Ѵ˽ű������϶�����Ӧ�Ķ�����������
/// ����Animation�м�Add Event
/// </summary>
public class AnimEvent : MonoBehaviour
{

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Allfoot;
    [SerializeField] private GameObject Shadow;
    [SerializeField] private GameObject footPrintLeft;
    [SerializeField] private GameObject footPrintRight;
    private int CurrentUsingIndex;
    private Player playerScript;
    private MapControl map;
    public Pool pool;

    public float recoverHP = 5;


    private void Start()
    {
        CurrentUsingIndex = Shadow.GetComponent<SwitchFoot>().CurrentUsingIndex;
        playerScript = Player.GetComponent<Player>();
        pool = FindAnyObjectByType<Pool>();
        map = FindAnyObjectByType<MapControl>();
    }

    /// <summary>
    /// Foot��������������ʱ�����
    /// </summary>
    public void OnFootAttack()
    {


        // TODO�����¿ո�󷢶����� 1.������ӡ��2.��������Ч����  ����ʵ�����ķ�ʽд�� �������ö���صķ�ʽ�Ĺ���������=
        // ��ӡλ�ó�����Player��ͬ��position�¡�
        Vector3 temp = new Vector3(Player.transform.position.x + 0.13f, Player.transform.position.y + 4.27f, 22f);
        GameObject obj = null;
        if (CurrentUsingIndex == 0)
        {
            obj = GameObject.Instantiate(footPrintLeft, temp, Quaternion.identity);
        }
        else
        {
            obj = GameObject.Instantiate(footPrintRight, temp, Quaternion.identity);
        }
        obj.transform.SetParent(Allfoot.transform);

        //TODO:����Ч�������һ����Χ��ɱ�����ˣ��ݻٷ��ӡ�
        Vector2 center = new Vector2(Shadow.transform.position.x, Shadow.transform.position.y);

        //�ȼ��layer8�Ĺ���
        Collider2D[] colliders = Physics2D.OverlapAreaAll( center+new Vector2(-2.58f, 0.6f), center + new Vector2(2.6f, 9.88f), 1<<8);
        foreach (Collider2D collider in colliders)
        {
            // �ö���ػ��ա�
            if (collider.CompareTag("NormalM"))
            {
                pool.dic["Monster"].Release(collider.gameObject);
                EventCenter.Instance.EventTrigger("����Ŀǰ��������", pool.dic["Monster"].CountActive);
                map.KilledEnemy += 1;
            }else if (collider.CompareTag("HPM"))
            {
                // ��Ѫ�粼�ֲ��������ϵ����������������ˣ�����̫��
                // TODO: ���������ǻ�Ѫ���¼���


                playerScript.TakeRecover(recoverHP);
                pool.dic["HpMonster"].Release(collider.gameObject);
                map.KilledEnemy += 1;

            }


        }

        //TODO:�ټ��layer7�ķ��ӣ�
        colliders = Physics2D.OverlapAreaAll(center + new Vector2(-2.58f, 0.6f), center + new Vector2(2.6f, 9.88f), 1 << 7);
        foreach (Collider2D collider in colliders)
        {
            // TODO:Demo�治��������ˣ�����ֱ�����þͺ�����
            Building build = collider.GetComponent<Building>();
            switch (build.currentExtent)
            {
                case Building.HouseExtent.Perfect:
                    build.currentExtent = Building.HouseExtent.HalfDes;
                    build.ChangeSpiriteTo(build.currentExtent);
                    build.StartHpMonster();  // ����2s����Գ���Ѫ��
                    break;
                case Building.HouseExtent.HalfDes:
                    EventCenter.Instance.EventTrigger("�ݻٽ���", null); //ͳ�ƽ����Լ��ɾͣ��Լ��ؿ������õġ�
                    build.CloseColliderBox();
                    build.currentExtent = Building.HouseExtent.TotalDes;
                    build.ChangeSpiriteTo(build.currentExtent);
                    map.DestoryedBuilding += 1;

                    break;

            }




        }
        // �����������Ϊ�ɱ����� ������ģ�������������ɱ��
        playerScript.canBeDamaged = true;

    }


    public void FinishAttack()
    {
        // TODO���������жϣ����TM��Ϸ�Ѿ������ˣ��ǾͲ�Ҫ�ָ��ˡ�������
        if (!playerScript.IsGameFinished)
        {
            playerScript.canOperate = true;
            Debug.Log("�ָ�player�Ŀ���+�ָ�Switchfoot");
            EventCenter.Instance.EventTrigger("Ь�ӹ�������", null);
        }


    }

    public void OnFinishCanBeDamaged()
    {
        //�����������Ϊ���ɱ�����
        playerScript.canBeDamaged = false;
    }


}
