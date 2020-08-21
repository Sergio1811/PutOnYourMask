using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    #region DragAndDropParameters
    Vector3 dragAndDropScreenSpace;
    Vector3 dragAndDropOffset;
    float dragSpeed = 2;
    bool dragAndDropAllowed = false;
    #endregion

    public GameObject dragSpherePrueba; //borrar si no es necesaria

    public static InputManager GetInstance()
    {
        if (instance == null)
        {
            instance = new InputManager();
        }
        return instance;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DragAndDrop(dragSpherePrueba); //borrar si no es necesario
        if (WhatAmIClicking() != null && WhatAmIClicking() != dragSpherePrueba)
            Destroy(WhatAmIClicking());
    }

    public void DragAndDrop(GameObject objectToDrag)
    {

#if UNITY_EDITOR || UNITY_STANDALONE

        if (Input.GetMouseButton(0)) //If clicked
        {

            if (Input.GetMouseButtonDown(0)) //OnMouseDown take parameters and allow movement
            {
                if (WhatAmIClicking() == objectToDrag) //Object to drag is correct
                {
                    dragAndDropScreenSpace = Camera.main.WorldToScreenPoint(objectToDrag.transform.position); //take screenpos of the object
                    dragAndDropOffset = objectToDrag.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, dragAndDropScreenSpace.z)); //Calculate offset to have a good visual perception of the drag
                    dragAndDropAllowed = true;
                }
            }

            if (dragAndDropAllowed) //On Mouse Moving and clicked
            {
                Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dragAndDropScreenSpace.z); //position in screen of the object
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + dragAndDropOffset; //calculate position in world with the offset
                objectToDrag.transform.position = currentPosition;
            }

            if (Input.GetMouseButtonUp(0)) //mouse Up movement not allowed
            {
                dragAndDropAllowed = false;
            }
        }
#endif

#if UNITY_ANDROID //same as above but in mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    if (WhatAmIClicking() == objectToDrag)
                    {
                        dragAndDropScreenSpace = Camera.main.WorldToScreenPoint(objectToDrag.transform.position);
                        dragAndDropOffset = objectToDrag.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, dragAndDropScreenSpace.z));

                        dragAndDropAllowed = true;
                    }

                    break;

                case TouchPhase.Moved:
                    if (dragAndDropAllowed)
                    {
                        Vector3 currentScreenSpace = new Vector3(touch.position.x, touch.position.y, dragAndDropScreenSpace.z);
                        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + dragAndDropOffset;
                        objectToDrag.transform.position = currentPosition;
                    }
                    break;

                case TouchPhase.Stationary:
                    break;

                case TouchPhase.Ended:

                    dragAndDropAllowed = false;

                    break;

                case TouchPhase.Canceled:
                    break;

                default:
                    break;
            }
        }
#endif
    }

    public GameObject WhatAmIClicking()
    {

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButton(0))//Cambiar si es necesario
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                return hit.collider.gameObject;
            }
        }
#endif

#if UNITY_ANDROID
        if (Input.touches.Length > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    // Construct a ray from the current touch coordinates
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        return hit.collider.gameObject;
                    }

                }
            }
        }
#endif
        return null;
    }
}
