using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class VersionToText : MonoBehaviour {

    private void Start()
    {
        GetComponent<Text>().text = string.Format("version {0}", Application.version);
    }

}
