using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharsPPMovement : MonoBehaviour
{
    [HideInInspector] public Transform waypoint;
    public int speed = 3;
    public bool voted;
    public GameObject mask;
    Animator animator;
    public CarasControl faceControl;
    public ClothManager clothManager;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        clothManager.RandomCloth();

    }

    void Update()
    {
        if (AccessControlManager.instance.currentState == AccessControlManager.GameState.Play)
        {

            if (waypoint != null)
            {
                if (Vector3.Distance(this.transform.position, waypoint.transform.position) > 0.1f)
                {
                    if (!animator.GetBool("Walking"))
                    {
                        animator.SetBool("Walking", true);
                    }
                    this.transform.position = Vector3.MoveTowards(this.transform.position, waypoint.position, speed * Time.deltaTime);
                }
                else if (voted)
                {
                    Destroy(this.gameObject);
                }
                else if (animator.GetBool("Walking"))
                {
                    animator.SetBool("Walking", false);
                }
            }

        }

    }

    public void SmellActivate()
    {
        animator.SetTrigger("Smell");
        StartCoroutine(faceControl.GrossFace(4));
    }

}
