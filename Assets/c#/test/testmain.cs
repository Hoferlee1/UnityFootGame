using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class testmain : MonoBehaviour
{
    // Start is called before the first frame update


    private void Update()
    {
        Debug.Log(UIManager.Instance.canvasgroup.alpha);

    }
    public void f(int a)
    {
        print(a);
    }
}
