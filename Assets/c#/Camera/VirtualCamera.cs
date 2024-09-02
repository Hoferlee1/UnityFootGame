using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    static private GameObject _instance;
    static public GameObject Instance
    {
        get { return _instance; }
    }
    // 无奈写个单例的相机吧
    void Start()
    {
        if (_instance != null)
        {
            // 类里的静态单例已经有了，摧毁现在这个
            Destroy(_instance);
            return;
        }
        else
        {
            _instance = gameObject;
            DontDestroyOnLoad(this);
            EventCenter.Instance.AddListener("进入游戏场景绑定虚拟相机", onLoadGameScene);
        }


    }

    /// <summary>
    /// 参数随便传一个即可，切换场景的时候调用的事件，用于给虚拟相机绑定游戏角色
    /// </summary>
    /// <param name="i"></param>
    private void onLoadGameScene(object i)
    {
        //查询主角物体然后绑定之。
        GameObject obj = GameObject.FindWithTag("Player");
        gameObject.GetComponent<CinemachineVirtualCamera>().Follow = obj.transform;
    }
}
