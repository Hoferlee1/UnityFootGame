using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


/// <summary>
/// ��ʱ���������ˣ��������ع�����
/// </summary>
public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private float PosX;
    private float PosY;
    private float mapYup;
    private float mapYdown;
    private float mapXleft;
    private float mapXright;
    private float HP;
    public float FullHP = 200;

    /// <summary>
    /// ����Ѫ��Ϊ�ջ��߽�������ʱ���޸Ĵ���
    /// </summary>
    public bool IsGameFinished;


    [Header("��������")]
    private List<GameObject> Styles;
    // ��ǰʹ�õļ����±�
    public int CurrentIndexOfStyles;
    public bool canOperate;
    public bool canBeDamaged;

    void Start()
    {
        //��ʼ��Ѫ��
        HP = FullHP;
        //��ʼ����Ϸ״̬����Ϸ����ʱ��Ϊtrue������Ѫ��Ϊ�ջ��߽�������ʱ���޸Ĵ���
        IsGameFinished = false;
        // speed = 12.0f;
        // TODO����ʱ���������ʼ��������ͼ��Ӧ�ø��ݲ�ͬ�ؿ���ʼ�����ĸ�ֵ
        // ��Ϊÿ����ͼ���и��Եı�Ե
        mapYup = 20.05f;
        mapYdown = -20.7f;
        mapXright = 37.2f;
        mapXleft = -37.6f;

        // �����˽ű������������ϵ
        Styles = new List<GameObject>
        {
            transform.GetChild(0).gameObject,
            transform.GetChild(1).gameObject
            //transform.GetChild(2).gameObject
        };
        CurrentIndexOfStyles = 0;
        Styles[CurrentIndexOfStyles].SetActive(true);

        canOperate = true;
        canBeDamaged = false;
    }
    private void Update()
    {
        if (canOperate)
        {
            CheckMapBorder();
            CheckInput();
        }

    }

    private void CheckMapBorder()
    {
        //Debug.Log("�����ƶ�");
        //Vertical and Horizontal
        PosX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        PosY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        PosX = transform.position.x + PosX;
        PosY = transform.position.y + PosY;
        // TODO:����Ƿ�Խλ��
        // ��pivot���ż�ľ��룺9.4f
        // ��pivot�����ȸ��ľ��룺7.2f
        // ��������ľ��룺�Ҳ�1.95f ��ࣺ1.64f;
        if (PosY+9.4f > mapYup)
        {
            PosY = mapYup - 9.4f;
        }
        if (PosY < mapYdown) // -7.2f 
        {
            PosY = mapYdown;
        }
        if (PosX- 1.64f < mapXleft)
        {
            PosX = mapXleft+ 1.64f;
        }
        if (PosX+ 1.95f > mapXright)
        {
            PosX = mapXright- 1.95f;
        }
        transform.position = new Vector3(PosX, PosY, 0.0f);
    }

    private void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            // �л�����style1  �л���ʲô�������Ӧ���ɡ�
            SwitchStyle(0);
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            // �л�����
            SwitchStyle(1);

        }


    }

    private void SwitchStyle(int index)
    {
        Styles[CurrentIndexOfStyles].SetActive(false);
        Styles[index].SetActive(true);
        CurrentIndexOfStyles = index;
    }




    #region Ѫ�����

    public void TakeDamage(object damage)
    {
        HP -= (float)damage;
        // TODO:  ��Ѫ����UIѪ��ʱ�䣬�ж����HPС�ڵ���0�˵���ʧ�ܽ��档
        // ���������һ��panel��
        // TODO: �ո������������ϽǷ������˵���
        EventCenter.Instance.EventTrigger("Ѫ������01", HP/FullHP);

        if(HP < 0 && !IsGameFinished)
        {
            // 1.ֹͣ��Ϸ��2.�������㻭��
            IsGameFinished = true;
            EventCenter.Instance.EventTrigger("ֹͣ��Ϸ", null);
            UIManager.Instance.ShowPanel<DefeatPanel>("UI/��Ϸ��panel/DefeatPanel", UIManager.UI_Layer.Mid);
        }


    }
    public void TakeRecover(object damage)
    {

        float p = (float)damage;
        if (HP + p > 200)
        {
            HP = 200;
        }
        else
        {
            HP += p;
        }
        EventCenter.Instance.EventTrigger("Ѫ������01", HP / FullHP);


    }
    #endregion

    private void OnEnable()
    {
        //Debug.Log("Playerע��ֹͣ��Ϸ");
        EventCenter.Instance.AddListener("ֹͣ��Ϸ", StopGame);
        EventCenter.Instance.AddListener("������Ϸ", Continue);


    }

    private void OnDisable()
    {
        
        EventCenter.Instance.RemoveListener("ֹͣ��Ϸ", StopGame);
        EventCenter.Instance.RemoveListener("������Ϸ", Continue);


    }

    private void Continue(object i)
    {
        Debug.Log("Player������Ϸ");

        canOperate = true;
    }
    private void StopGame(object i)
    {
        canOperate = false;
        Debug.Log("�ѽ����ƶ�ָ�������������������������������������������������");

    }

}
