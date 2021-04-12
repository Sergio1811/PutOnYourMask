using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCamera : MonoBehaviour
{
    Vector3 touchStart;
    Camera camara;
    // Start is called before the first frame update
    void Start()
    {
        camara = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            touchStart = camara.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - camara.ScreenToWorldPoint(Input.mousePosition);
           camara.gameObject.transform.position += direction;
        }

    }
}
