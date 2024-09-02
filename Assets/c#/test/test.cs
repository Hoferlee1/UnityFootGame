using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


public class test:MonoBehaviour
{
    public GameObject son;
    public GameObject father;

    public void onClick()
    {
        //son.transform.SetParent(father.transform);
        //GameObject a = Instantiate(son);
        //a.transform.SetParent(father.transform);
        UIManager.Instance.ShowPanel<IntroGamePanel>("UI/ÓÎÏ·ÄÚpanel/IntroGamePanel", UIManager.UI_Layer.Bot);

    }
}
