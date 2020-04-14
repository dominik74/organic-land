using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingSystem : MonoBehaviour {

    public bool isBuilding;
    public GameObject buildPreviewTemplate;
    public GameObject objectTemplate;
    private ObjectData objectData;

    private Camera cam;
    private GameObject preview;

    //TMP
    private Dictionary<string, System.Type> objectScripts = new Dictionary<string, System.Type>()
    {
        { "Furnace", typeof(Furnace) }
    };

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

    public void PlaceBuilding(ObjectData data, Vector3 pos) // Used by commands (EXPERIMENTAL)
    {
        if(data != null)
        {
            GameObject obj = Instantiate(objectTemplate);

            InitializePlacedObject(obj, data, pos);
        }
    }

    void PlaceBuilding()
    {
        if(objectData != null)
        {
            GameObject obj = Instantiate(objectTemplate);

            InitializePlacedObject(obj, objectData, preview.transform.position);

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

    void InitializePlacedObject(GameObject obj, ObjectData data, Vector3 pos)
    {
        obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = data.sprite;
        obj.transform.GetChild(0).GetComponent<SpriteRenderer>().color = data.color;

        //Object script = data.scriptHandler;

        if (data.usesGUI)
        {
            obj.transform.gameObject.AddComponent<GUITrigger>().guiName = data.scriptName;
            //AddComponentAsString(data.scriptName, obj.transform.GetChild(0).gameObject);
        }

        obj.name = data.name;
        obj.transform.position = pos;
    }

    void AddComponentAsString(string name, GameObject objectToAddComponentTo)
    {
        System.Type componentType;

        if(objectScripts.TryGetValue(name, out componentType))
        {
            objectToAddComponentTo.AddComponent(componentType);
        }
    }

}
