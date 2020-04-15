using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAfterDelay : MonoBehaviour {

    public delegate void MyAction();
    public static MyAction AfterFrame;

    static UpdateAfterDelay instance;
    private void Awake()
    {
        instance = this;
    }

    public static void ExecuteAfterFrame(MyAction handler)
    {
        AfterFrame = handler;
        instance.StartCoroutine("Delay");
    }

    IEnumerator Delay()
    {
        yield return null;
        if(AfterFrame != null)
            AfterFrame();
        AfterFrame = null;
    }

}
