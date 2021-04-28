using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharRotation : MonoBehaviour
{
    public GameObject character;
    Vector3 lastMousePos;
    Vector3 lastTouchPos;
    public float forceMovement;
    float distance;
    Quaternion initialRot = new Quaternion(0,180,0,0);

    void Update()
    {
        if (Input.touchCount > 0)
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
                character.transform.Rotate(Vector3.up, distance * forceMovement );
            }
        }
        
        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            float distance = lastMousePos.x - currentMousePos.x;

            if (distance != 0)
            {
                //distance = Mathf.Abs(distance);
                character.transform.Rotate(Vector3.up, distance * forceMovement * Time.deltaTime);
            }
        }

    }

    public void ResetRot()
    {
        this.transform.rotation = initialRot;
    }
}
