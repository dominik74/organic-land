using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour {

    public float movementSpeed = 7f;
    public float stopPointMinimumDistance = 0.2f;
    private Transform cameraTransform;
    private Camera cam;

    private bool isMoving;

    public delegate void PickupCallbackDelegate();
    public PickupCallbackDelegate pickupCallbackDelegate;

    private Coroutine lastMoveToCroutine;

    #region SINGLETON
    private static PlayerMovement instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        cam = Camera.main;
    }

    private void Update()
    {
        if (Time.timeScale == 0)
            return;

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if (x != 0 || z != 0)
            isMoving = false;

        Vector3 moveX = cameraTransform.right * x;
        Vector3 moveZ = cameraTransform.forward * z;

        Vector3 movement = moveX + moveZ;

        transform.position += new Vector3(movement.x, 0, movement.z) * movementSpeed * Time.fixedDeltaTime;

        if(Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Ground"))
                {
                    if (lastMoveToCroutine != null)
                        StopCoroutine(lastMoveToCroutine);
                    StopCoroutine("MoveTo");
                    StartCoroutine("MoveTo", hit.point);
                }
            }
        }

    }

    public static void MoveToTarget(Vector3 targetPos, float pickupDistance, PickupCallbackDelegate pickup = null)
    {
        if(instance.lastMoveToCroutine != null)
            instance.StopCoroutine(instance.lastMoveToCroutine);
        instance.StopCoroutine("MoveTo");
        instance.lastMoveToCroutine = instance.StartCoroutine(instance.MoveToComplex(targetPos, pickupDistance, pickup));
    }

    IEnumerator MoveTo(Vector3 targetPos)
    {
        isMoving = true;
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

    IEnumerator MoveToComplex(Vector3 targetPos, float pickupDistance, PickupCallbackDelegate pickup)
    {
        bool reachedTarget = false;
        isMoving = true;
        while (isMoving)
        {
            Vector3 dir = (targetPos - transform.position).normalized;
            dir.y = 0;
            transform.position += dir * movementSpeed * Time.fixedDeltaTime;

            if ((targetPos - transform.position).sqrMagnitude <= pickupDistance * pickupDistance)
            {
                isMoving = false;
                reachedTarget = true;
            }

            yield return null;
        }
        if (pickup != null && reachedTarget)
            pickup();
    }

}
