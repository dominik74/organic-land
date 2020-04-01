using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public delegate void MyActionHandler();
    public static event MyActionHandler OnStatsUpdated;

    public static MyActionHandler OnItemAdded;

    public static void StatsUpdated()
    {
        if (OnStatsUpdated != null)
            OnStatsUpdated();
    }

    public static void ItemAdded()
    {
        if (OnItemAdded != null)
            OnItemAdded();
    }

}
