using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float movementSpeed = 7f;
    public float stopPointMinimumDistance = 0.2f;
    private Transform cameraTransform;
    private Camera cam;

    private bool isMoving;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        cam = Camera.main;
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if (x != 0 || z != 0)
            isMoving = false;

        Vector3 moveX = cameraTransform.right * x;
        Vector3 moveZ = cameraTransform.forward * z;

        Vector3 movement = moveX + moveZ;

        transform.position += new Vector3(movement.x, 0, movement.z) * movementSpeed * Time.fixedDeltaTime;

        if (Input.GetKeyDown(KeyCode.Q))
            Rotate(-45f);
        else if (Input.GetKeyDown(KeyCode.E))
            Rotate(45f);


        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Ground"))
                {
                    isMoving = true;
                    StopCoroutine("MoveTo");
                    StartCoroutine("MoveTo", hit.point);
                }
            }
        }

    }

    void Rotate(float amount)
    {
        transform.Rotate(0, amount, 0, Space.World);
    }

    IEnumerator MoveTo(Vector3 targetPos)
    {
        while (isMoving)
        {
            Vector3 dir = (targetPos - transform.position).normalized;
            dir.y = 0;
            transform.position += dir * movementSpeed * Time.fixedDeltaTime;

            if ((targetPos - transform.position).sqrMagnitude <= stopPointMinimumDistance * stopPointMinimumDistance)
                isMoving = false;

            yield return null;
        }
    }

}
