using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public GameObject player;
    public static GameObject playerUnit;

    public static PlayerManager instance;
    private void Awake()
    {
        instance = this;
        playerUnit = player;
    }



}
