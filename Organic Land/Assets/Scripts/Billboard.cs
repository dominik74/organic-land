﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    private Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.rotation = cam.rotation;
    }

}
