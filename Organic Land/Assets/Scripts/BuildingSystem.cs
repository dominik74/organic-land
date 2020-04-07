using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour {

    public bool isBuilding;
    public GameObject buildPreviewTemplate;
    public ObjectData objectData;

    private Camera cam;
    private GameObject preview;

    public static BuildingSystem instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cam = Camera.main;
        CachePreview();
    }

    private void Update()
    {
        if (isBuilding)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                PlacePreview(hit.point);
            }
        }
        else
            preview.SetActive(false);
    }

    public void StartBuilding(bool activate, ObjectData data = null)
    {
        if (data != null)
            objectData = data;
        isBuilding = activate;
    }

    void CachePreview()
    {
        preview = Instantiate(buildPreviewTemplate);
        preview.SetActive(false);
    }

    void PlacePreview(Vector3 targetPos)
    {
        preview.transform.position = targetPos;
        preview.SetActive(true);
        InitializePreview(objectData);
    }

    void InitializePreview(ObjectData data)
    {
        preview.name = string.Format("{0} (preview)", data.name);
        preview.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = data.sprite;
        preview.transform.GetChild(0).GetComponent<SpriteRenderer>().color = data.color;
    }

}
