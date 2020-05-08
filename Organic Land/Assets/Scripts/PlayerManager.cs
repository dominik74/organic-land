using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public GameObject player;
    public Vector3 spawnLocation;

    public static GameObject playerUnit;

    public static PlayerManager instance;
    private void Awake()
    {
        instance = this;
        playerUnit = player;

        InitializePlayerPosition();
    }

    private void InitializePlayerPosition()
    {
        playerUnit.transform.position = spawnLocation;
    }

}
