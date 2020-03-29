using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public delegate void MyActionHandler();
    public static event MyActionHandler OnStatsUpdated;

    public static void StatsUpdated()
    {
        if (OnStatsUpdated != null)
            OnStatsUpdated();
    }

}
