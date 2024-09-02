using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoController : MonoBehaviour
{
    private event UnityAction hanshu;

    public void NotDestoryMono(GameObject obj)
    {
        DontDestroyOnLoad(obj);
    }
    private void Update()
    {
        if(hanshu!=null) 
        {
            hanshu();
        }

    }

    public void AddUpdateHanshu(UnityAction f)
    {
        hanshu += f;
    }

    public void RemoveUpdateHanshu(UnityAction f)
    {
        hanshu -= f;
    }



}
