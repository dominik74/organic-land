using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if(selectedObjectName.activeSelf)
            selectedObjectName.transform.position = Input.mousePosition;
    }

    public void DisplayObjectName(string objName)
    {
        selectedObjectNameText.text = objName;
    }

}
