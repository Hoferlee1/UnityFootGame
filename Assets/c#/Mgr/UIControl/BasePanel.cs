using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// �����࣬���԰�UI���ּ����������UI��ť���󣬿�����ʾ�Լ������������Լ���
/// </summary>
public class BasePanel : MonoBehaviour
{
    public Dictionary<string, List<UIBehaviour>> uiDic = new Dictionary<string, List<UIBehaviour>>();

    private CanvasGroup canvasGroup;


    virtual protected void Awake()
    {


        //ÿ�μ����Զ���ȡ���������UI�ӿؼ������뵽Dic�С�
        FindChildUi<Button>();
        FindChildUi<Image>();
        FindChildUi<Text>();
        FindChildUi<TextMeshProUGUI>();
        FindChildUi<Toggle>();
        FindChildUi<Slider>();
        FindChildUi<ScrollRect>();

    }
    virtual public void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    /// <summary>
    /// UI�����е�����ShowPanel��ʱ�򣬻��Զ���������ShowMe����
    /// </summary>
    virtual public void ShowMe()
    {
        canvasGroup.interactable = true;

    }

    /// <summary>
    /// ĿǰHide�����þ��ǿ�ʼblock���ð�ť��
    /// </summary>
    virtual public void HideMe() 
    {
        // ������block��ʲôʱ��⿪�أ��ر������ʱ��ǵý⿪
        // ͨ�����ƴ�Panel�ϵ�canvasgroup�رմ�Panel
        canvasGroup.interactable = false;
    }

    /// <summary>
    /// �����Զ���Ӱ�ť��Ӧ�ģ������ֶ����¼��ˣ��̳еľ�������panel����Ҫ�������������д��
    /// </summary>
    virtual public void OnClick(string name)
    {
        //Debug.Log("basepanel�ѵ���");
    }




    /// <summary>
    /// ����ӿڣ������ڸ���UI���ֲ���panel�µ���UI
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T SearchChildUI<T>(string name)where T : UIBehaviour
    {
        if (uiDic.ContainsKey(name))
        {
            for(int i = 0; i < uiDic[name].Count; ++i)
            {
                if (uiDic[name][i] is T)
                {
                    //Debug.Log("�ҵ�UI");
                    return uiDic[name][i] as T;
                }
            }
        }

        Debug.Log("���޴���UI" + name);
        return null;

    }


    /// <summary>
    /// ˽�к�����ֻ���ڼ��ص�ʱ���Զ��ҳ����е���UI����uiDic�ֵ��С�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private void FindChildUi<T>() where T: UIBehaviour
    {
        T[] temp = gameObject.GetComponentsInChildren<T>(true); // true���ڿ����ϱ����õ������壬���������������Ƕ�����������塣
        
        for(int i = 0; i<temp.Length; i++)
        {
            string name = temp[i].gameObject.name;
            if(uiDic.ContainsKey(name)) 
            {
                uiDic[name].Add(temp[i]);
            }
            else
            {
                uiDic.Add(name, new List<UIBehaviour>() { temp[i] });
            }
            //�Զ���button����¼� �������������ֵ�bug������ֻ��¼���һ����why��
            if (temp[i] is Button)
            {
                //Debug.Log(name);
                (temp[i] as Button).onClick.AddListener(() =>
                {
                    OnClick(name);
                });

            }

        }
    }


}
