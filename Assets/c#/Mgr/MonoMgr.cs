using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MonoMgr : Singleton<MonoMgr>
{
    private MonoController singleMono;

    /// <summary>
    /// ��Ҫ���ⲿ���������������
    /// </summary>
    public MonoMgr()
    {
        // ������������ԭ���ǣ����ǵù���һ��singleMono���Ҳ��������أ�
        // ����ű�ֱ�Ӹ��������̬���ﲻ�����ˣ� ���� ��ΪMono������new...
        // singleMono = new MonoController();
        // d���������᲻�ᵼ���л�������ʱ��ʧ���󣿣�
        GameObject obj = new GameObject("MonoMgrObj");
        singleMono = obj.AddComponent<MonoController>();
        singleMono.NotDestoryMono(obj);//�������仰����Ȼ�л������ر���
        
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
