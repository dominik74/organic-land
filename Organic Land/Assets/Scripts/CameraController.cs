using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Vector3 maxOffset;
    public float maxRotation;

    private Vector3 minOffset;
    private float minRotation;
    private float zoomFactor = 4f;

    private float currentScroll;

    private CameraFollow cameraFollow;

    private void Start()
    {
        cameraFollow = GetComponent<CameraFollow>();
        minOffset = cameraFollow.offset;
        minRotation = transform.eulerAngles.x;
    }

    private void Update()
    {
        if (Time.timeScale == 0)
            return;

        if (Input.GetKeyDown(KeyCode.Q))
            Rotate(-45f);
        else if (Input.GetKeyDown(KeyCode.E))
            Rotate(45f);

        float scrollData = Input.GetAxis("Mouse ScrollWheel");

        currentScroll -= scrollData;
        currentScroll = Mathf.Clamp01(currentScroll);

        cameraFollow.offset = Vector3.Lerp(minOffset, maxOffset, currentScroll);
        transform.eulerAngles = Vector3.Lerp(new Vector3(minRotation, transform.eulerAngles.y, transform.eulerAngles.z), new Vector3(maxRotation, transform.eulerAngles.y, transform.eulerAngles.z), currentScroll);
    }

    void Rotate(float amount)
    {
        cameraFollow.offset = Quaternion.Euler(0, amount, 0) * cameraFollow.offset;
        minOffset = Quaternion.Euler(0, amount, 0) * minOffset;
        maxOffset = Quaternion.Euler(0, amount, 0) * maxOffset;
        transform.Rotate(0, amount, 0, Space.World);
    }

}
