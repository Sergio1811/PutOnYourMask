using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

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
        if (WhatAmIClicking() != null)
            Destroy(WhatAmIClicking());
    }

    public void DragAndDrop()
    {

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
