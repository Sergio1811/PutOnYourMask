using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovement : MonoBehaviour
{
    public RectTransform map;
    public RectTransform waypoints;
    public float minX;
    public float maxX;
    bool right = false;
    Vector3 lastMousePos;
    Vector3 lastTouchPos;
    public float forceMovement;
    float distance;
    void Start()
    {
        lastMousePos = Camera.main.ScreenToViewportPoint(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.touchCount > 0 )
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                lastTouchPos = Input.GetTouch(0).position;
            }

            distance = Camera.main.ScreenToViewportPoint(lastTouchPos).x - Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position).x;

            lastTouchPos = Input.GetTouch(0).position;

            if (distance != 0)
            {
                //distance = Mathf.Abs(distance);
                map.anchoredPosition3D = new Vector3(map.anchoredPosition3D.x - distance * forceMovement, map.anchoredPosition3D.y, map.anchoredPosition3D.z);
                waypoints.anchoredPosition3D = new Vector3(waypoints.anchoredPosition3D.x - distance * forceMovement, waypoints.anchoredPosition3D.y, waypoints.anchoredPosition3D.z);

                //clamps
                if (map.anchoredPosition3D.x > maxX)
                {
                    map.anchoredPosition3D = new Vector3(maxX, map.anchoredPosition3D.y, map.anchoredPosition3D.z);
                    waypoints.anchoredPosition3D = new Vector3(maxX, waypoints.anchoredPosition3D.y, waypoints.anchoredPosition3D.z);
                }
                else if (map.anchoredPosition3D.x < minX)
                {
                    map.anchoredPosition3D = new Vector3(minX, map.anchoredPosition3D.y, map.anchoredPosition3D.z);
                    waypoints.anchoredPosition3D = new Vector3(minX, waypoints.anchoredPosition3D.y, waypoints.anchoredPosition3D.z);
                }
            }
        }

        /*
        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            float distance = lastMousePos.x - currentMousePos.x;

            if (distance != 0)
            {
                map.anchoredPosition3D = new Vector3(map.anchoredPosition3D.x + distance * -forceMovement, map.anchoredPosition3D.y, map.anchoredPosition3D.z);
                waypoints.anchoredPosition3D = new Vector3(waypoints.anchoredPosition3D.x + distance * -forceMovement, waypoints.anchoredPosition3D.y, waypoints.anchoredPosition3D.z);

                if (map.anchoredPosition3D.x > maxX)
                {
                    map.anchoredPosition3D = new Vector3(maxX, map.anchoredPosition3D.y, map.anchoredPosition3D.z);
                    waypoints.anchoredPosition3D = new Vector3(maxX, waypoints.anchoredPosition3D.y, waypoints.anchoredPosition3D.z);
                }
                else if (map.anchoredPosition3D.x < minX)
                {
                    map.anchoredPosition3D = new Vector3(minX, map.anchoredPosition3D.y, map.anchoredPosition3D.z);
                    waypoints.anchoredPosition3D = new Vector3(minX, waypoints.anchoredPosition3D.y, waypoints.anchoredPosition3D.z);
                }
            }

        }

        lastMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);*/

    }
}
