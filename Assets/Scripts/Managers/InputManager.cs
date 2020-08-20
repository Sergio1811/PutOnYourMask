using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    float deltaX, deltaY;
    float dragSpeed = 2;
    bool dragAndDropAllowed = false;

    public GameObject dragSpherePrueba;

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
        DragAndDrop(dragSpherePrueba);
    }

    public void DragAndDrop(GameObject objectToDrag)
    {

#if UNITY_EDITOR || UNITY_STANDALONE

        if (Input.GetMouseButton(0))
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                if (WhatAmIClicking() == objectToDrag)
                {
                    deltaX = touchPos.x - objectToDrag.transform.position.x;
                    deltaY = touchPos.y - objectToDrag.transform.position.y;

                    dragAndDropAllowed = true;
                }
            }

            if (WhatAmIClicking() == objectToDrag && dragAndDropAllowed)
            {
                objectToDrag.transform.position = Vector3.MoveTowards(objectToDrag.transform.position, new Vector2(touchPos.x - deltaX, touchPos.y - deltaY), dragSpeed * Time.deltaTime);
            }


            if (Input.GetMouseButtonUp(0))
            {
                dragAndDropAllowed = false;
            }
        }
#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    if (WhatAmIClicking() == objectToDrag)
                    {
                        deltaX = touchPos.x - objectToDrag.transform.position.x;
                        deltaY = touchPos.y - objectToDrag.transform.position.y;

                        dragAndDropAllowed = true;
                    }

                    break;

                case TouchPhase.Moved:
                    if (WhatAmIClicking() == objectToDrag && dragAndDropAllowed)
                    {
                        objectToDrag.transform.position = Vector3.MoveTowards(objectToDrag.transform.position, new Vector2(touchPos.x - deltaX, touchPos.y - deltaY), dragSpeed * Time.deltaTime);
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
        if (Input.GetMouseButtonUp(0))
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
