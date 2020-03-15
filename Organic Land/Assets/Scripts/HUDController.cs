using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HUDController : MonoBehaviour {

    public GameObject selectedObjectName;
    private Text selectedObjectNameText;

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

    public void DisplayObjectName(string objName)
    {
        if (objName == "")
            selectedObjectName.SetActive(false);
        else
        {
            selectedObjectNameText.text = objName;
            selectedObjectName.SetActive(true);
        }
    }

}
