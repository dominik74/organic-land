using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectController : MonoBehaviour {

    private Camera cam;

    private Selector lastSelected;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

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

        if(Input.GetMouseButtonDown(0))
        {
            if(lastSelected != null)
            {
                Debug.Log(lastSelected.transform, lastSelected.transform);
                Pickable pickable = lastSelected.transform.parent.GetComponent<Pickable>();
                IObjectController iObjectController = lastSelected.transform.parent.GetComponent<IObjectController>();

                Selector selector = lastSelected.GetComponent<Selector>();
                selector.Press();

                if (pickable)
                    pickable.Pickup();
                else if (iObjectController != null)
                    iObjectController.Interact();
            }

        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(lastSelected != null)
            {
                Selector selector = lastSelected.GetComponent<Selector>();
                selector.UnPress();
            }
        }
    }

}
