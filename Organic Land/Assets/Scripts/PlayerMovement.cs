using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float movementSpeed = 7f;
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 moveX = cameraTransform.right * x;
        Vector3 moveZ = cameraTransform.forward * z;

        Vector3 movement = moveX + moveZ;

        transform.position += new Vector3(movement.x, 0, movement.z) * movementSpeed * Time.fixedDeltaTime;

        if (Input.GetKeyDown(KeyCode.Q))
            Rotate(45f);
        else if (Input.GetKeyDown(KeyCode.E))
            Rotate(-45f);
    }

    void Rotate(float amount)
    {
        transform.Rotate(0, amount, 0, Space.World);
    }

}
