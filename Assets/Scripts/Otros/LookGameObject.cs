using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookGameObject : MonoBehaviour
{

    public GameObject objectToLookAt;
    private Vector3 targetPoint;
    private Quaternion targetRotation;
    public float rotationSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetPoint = new Vector3(objectToLookAt.transform.position.x, transform.position.y, objectToLookAt.transform.position.z) - transform.position;
        targetRotation = Quaternion.LookRotation(targetPoint, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

    }
}
