using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovement : MonoBehaviour
{
    public RectTransform map;
    public float minX;
    public float maxX;
    bool right = false;
    Vector3 lastMousePos;
    public float forceMovement;

    void Start()
    {
        lastMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount>0 && Input.GetTouch(0).deltaPosition!=null)
        {
            float distance = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position).x - Camera.main.ScreenToViewportPoint(Input.GetTouch(0).deltaPosition).x;
            
            if (Mathf.Abs(distance)>0.1f)
            {
                map.anchoredPosition3D = new Vector3(map.anchoredPosition3D.x + distance, map.anchoredPosition3D.y, map.anchoredPosition3D.z);
            }
            if (map.anchoredPosition3D.x > maxX)
            {
                map.anchoredPosition3D = new Vector3(maxX, map.anchoredPosition3D.y, map.anchoredPosition3D.z);
            }
            else if (map.anchoredPosition3D.x < minX)
            {
                map.anchoredPosition3D = new Vector3(minX, map.anchoredPosition3D.y, map.anchoredPosition3D.z);
            }
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            float distance = lastMousePos.x - currentMousePos.x;

            if (distance != 0)
            {
                map.anchoredPosition3D = new Vector3(map.anchoredPosition3D.x + distance * forceMovement, map.anchoredPosition3D.y, map.anchoredPosition3D.z);

                if (map.anchoredPosition3D.x > maxX)
                {
                    map.anchoredPosition3D = new Vector3(maxX, map.anchoredPosition3D.y, map.anchoredPosition3D.z);
                }
                else if (map.anchoredPosition3D.x < minX)
                {
                    map.anchoredPosition3D = new Vector3(minX, map.anchoredPosition3D.y, map.anchoredPosition3D.z);
                }
            }

        }
        lastMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

    }
}
