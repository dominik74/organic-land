using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAfterDelay : MonoBehaviour {

    public delegate void MyAction();

    static UpdateAfterDelay instance;
    private void Awake()
    {
        instance = this;
    }

    public static void ExecuteAfterFrame(MyAction handler)
    {
        instance.StartCoroutine("Delay", handler);
    }

    IEnumerator Delay(MyAction handler)
    {
        yield return null;
        if(handler != null)
            handler();
    }

}
