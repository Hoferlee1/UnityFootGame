using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MonoMgr : Singleton<MonoMgr>
{
    private MonoController singleMono;

    /// <summary>
    /// 不要在外部主动调用这个构造
    /// </summary>
    public MonoMgr()
    {
        // 这里造个物体的原因是，我们得构造一个singleMono。我不用物体呢？
        // 这个脚本直接跟在这个静态类里不就行了？ 不行 因为Mono不能用new...
        // singleMono = new MonoController();
        // d但是这样会不会导致切换场景的时候丢失对象？？
        GameObject obj = new GameObject("MonoMgrObj");
        singleMono = obj.AddComponent<MonoController>();
        singleMono.NotDestoryMono(obj);//必须加这句话，不然切换场景必报错。
        
    }

    public void AddUpdateHanshu(UnityAction f)
    {
        singleMono.AddUpdateHanshu(f);
    }

    public void RemoveUpdateHanshu(UnityAction f)
    {
        singleMono.RemoveUpdateHanshu(f);
    }

    public void StartSingleCoroutine(IEnumerator routine)
    {
        singleMono.StartCoroutine(routine);
    }


}
