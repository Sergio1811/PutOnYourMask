using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLabMovement : MonoBehaviour
{
    [HideInInspector]public Animator animator;
    public Vector3 nextPoint;
    public float speed;
    private Vector3 targetPoint;
    private Quaternion targetRotation;
    public float rotationSpeed;
    Vector3 objectToLookAt;

    float CDToLookCamera;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Walking", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (nextPoint != Vector3.zero)
        {
            nextPoint.y = this.transform.position.y;

            if (Vector3.Distance(this.transform.position, nextPoint) > .25f)
            {
                if (!animator.GetBool("Run"))
                    animator.SetBool("Run", true);

                objectToLookAt = nextPoint;

                targetPoint = new Vector3(nextPoint.x, transform.position.y, nextPoint.z) - transform.position;
                targetRotation = Quaternion.LookRotation(targetPoint, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                this.transform.position = Vector3.MoveTowards(this.transform.position, nextPoint, speed * Time.deltaTime);                
            }
            else
            {
                if (animator.GetBool("Run"))
                {
                    animator.SetBool("Run", false);
                    CDToLookCamera = 2;
                }

                if (CDToLookCamera <= 0)
                    objectToLookAt = Camera.main.transform.position;

                targetPoint = new Vector3(objectToLookAt.x, transform.position.y, objectToLookAt.z) - transform.position;
                targetRotation = Quaternion.LookRotation(targetPoint, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }

        if (CDToLookCamera > 0)
            CDToLookCamera -= Time.deltaTime;

    }
}
