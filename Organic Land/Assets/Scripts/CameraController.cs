using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Vector3 maxOffset;
    public float maxRotation;
    public float yRotSpeed = 0.2f;

    private Vector3 minOffset;
    private float minRotation;
    private float zoomFactor = 4f;

    private float currentScroll;
    private Vector3 targetYrot;
    private float currentYrot;

    private float rotationMultiplier;
    private int rotationDir;

    private float distanceRotated;

    private CameraFollow cameraFollow;

    private void Start()
    {
        cameraFollow = GetComponent<CameraFollow>();
        minOffset = cameraFollow.offset;
        minRotation = transform.eulerAngles.x;
        rotationMultiplier = 1;

        // Set default camera offset
        currentScroll = 0.5f;
    }

    private void Update()
    {
        if (Time.timeScale == 0)
            return;

        if(!Settings.smoothRotation)
        {
            if (Input.GetKeyDown(KeyCode.Q))
                SetRotation(-1);
            else if (Input.GetKeyDown(KeyCode.E))
                SetRotation(1);

            UpdateRotation();
        }
        else
        {
            if (Input.GetKey(KeyCode.Q))
                SetRotation(-1);
            else if (Input.GetKey(KeyCode.E))
                SetRotation(1);
        }

        UpdateZoom();
            
    }

    void UpdateZoom()
    {
        float scrollData = Input.GetAxis("Mouse ScrollWheel");

        currentScroll -= scrollData;
        currentScroll = Mathf.Clamp01(currentScroll);

        currentYrot -= scrollData;
        currentYrot = Mathf.Clamp01(currentYrot);

        cameraFollow.offset = Vector3.Lerp(minOffset, maxOffset, currentScroll);
        transform.eulerAngles = Vector3.Lerp(new Vector3(minRotation, transform.eulerAngles.y, transform.eulerAngles.z), 
            new Vector3(maxRotation, transform.eulerAngles.y, transform.eulerAngles.z), currentScroll);
    }

    void UpdateRotation()
    {
        rotationMultiplier -= yRotSpeed * Time.deltaTime;
        if (rotationMultiplier <= 0)
            rotationMultiplier = 0;
        if (distanceRotated < 45)
        {
            Rotate(rotationDir * yRotSpeed * Time.deltaTime);
            distanceRotated += yRotSpeed * Time.deltaTime;
        }
    }

    void SetRotation(int amount)
    {
        if(Settings.smoothRotation)
        {
            Rotate(amount * yRotSpeed * Time.deltaTime);
            return;
        }

        rotationMultiplier = 1;
        rotationDir = amount;

        if(distanceRotated >= 45)
            distanceRotated = 0;

        targetYrot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + amount, transform.eulerAngles.z);
    }

    void Rotate(float amount)
    {
        cameraFollow.offset = Quaternion.Euler(0, amount, 0) * cameraFollow.offset;
        minOffset = Quaternion.Euler(0, amount, 0) * minOffset;
        maxOffset = Quaternion.Euler(0, amount, 0) * maxOffset;
        transform.Rotate(0, amount, 0, Space.World);
    }

}
