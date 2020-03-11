using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectController : MonoBehaviour {

    private Camera cam;

    private Selector lastSelected;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            Selector selector = hit.transform.GetComponent<Selector>();

            if (lastSelected != null)
            {
                if(selector != null)
                {
                    if (lastSelected != selector)
                    {
                        lastSelected.Deselect();
                        lastSelected = selector;
                        lastSelected.Select();
                    }
                }
                else
                {
                    lastSelected.Deselect();
                    lastSelected = null;
                }

            }
            else if (selector != null)
            {
                lastSelected = selector;
                selector.Select();
            }
        }
    }

}
