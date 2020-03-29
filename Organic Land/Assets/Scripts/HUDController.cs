using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HUDController : MonoBehaviour {

    public GameObject selectedObjectName;
    private Text selectedObjectNameText;

    private string objectDurabilityStatus;

    private string selectedObjName;

    public static HUDController instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        selectedObjectNameText = selectedObjectName.GetComponent<Text>();
        selectedObjectNameText.text = "";
    }

    private void Update()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            if (selectedObjectName.activeSelf)
                selectedObjectName.transform.position = Input.mousePosition;
        }
        else
        {
            selectedObjectName.SetActive(false);
        }
    }

    public void DisplayObjectLabel(string objectName)
    {
        selectedObjName = objectName;    
    }

    public void UpdateDurabilityStatus(float newStatus, float maxStatus)
    {
        if (newStatus != 0 && maxStatus != 0)
            objectDurabilityStatus = string.Format("({0}/{1})", newStatus, maxStatus);
        else
            objectDurabilityStatus = "";
    }

    public void DrawObjectLabel()
    {
        if (selectedObjName == "")
            selectedObjectName.SetActive(false);
        else
        {
            selectedObjectNameText.text = string.Format("{0} {1}", selectedObjName, objectDurabilityStatus);
            selectedObjectName.SetActive(true);
        }
    }

}
