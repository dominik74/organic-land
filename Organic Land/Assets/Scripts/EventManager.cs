using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public delegate void ActionHandler();
    public static event ActionHandler OnTimeUpdated;

    public static void TimeUpdated()
    {
        if (OnTimeUpdated != null)
            OnTimeUpdated();
        Debug.Log("call");
    }

}
