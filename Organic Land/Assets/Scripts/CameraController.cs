using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private CameraFollow cameraFollow;

    private void Start()
    {
        cameraFollow = GetComponent<CameraFollow>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Rotate(45f);
        else if (Input.GetKeyDown(KeyCode.E))
            Rotate(-45f);
    }

    void Rotate(float amount)
    {
        cameraFollow.offset = Quaternion.Euler(0, amount, 0) * cameraFollow.offset;
        transform.Rotate(0, amount, 0, Space.World);
    }

}
