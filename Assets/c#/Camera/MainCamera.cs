using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    static private GameObject _instance;
    static public GameObject Instance
    {
        get { return _instance; }
    }
    
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            // 类里的静态单例已经有了，摧毁现在这个
            Destroy(gameObject);

        }
        else
        {
            _instance = gameObject;
            DontDestroyOnLoad(this);

        }

    }


}
