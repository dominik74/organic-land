using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingSystem : MonoBehaviour {

    public bool isBuilding;
    public GameObject buildPreviewTemplate;
    public GameObject objectTemplate;
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
        objectTemplate = TerrainGenerator.instance.objectTemplate;
        CachePreview();
    }

    private void Update()
    {
        if (isBuilding)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                PlacePreview(hit.point);
            }

            if (Input.GetMouseButtonDown(0))
                PlaceBuilding();
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

    void PlaceBuilding()
    {
        if(objectData != null)
        {
            GameObject obj = Instantiate(objectTemplate);
            obj.name = objectData.name;
            obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = objectData.sprite;
            obj.transform.GetChild(0).GetComponent<SpriteRenderer>().color = objectData.color;
            obj.transform.position = preview.transform.position;

            InventorySystem.instance.RemoveSelectedItem();
            isBuilding = false;
        }
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
    }

}
