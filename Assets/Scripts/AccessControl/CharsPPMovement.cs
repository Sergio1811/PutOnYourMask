using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharsPPMovement : MonoBehaviour
{
    [HideInInspector] public Transform waypoint;
    public int speed = 3;
    public bool voted;
    public GameObject mask;
    public List<Animator> animator;
    public RuntimeAnimatorController animatorController;
    public CarasControl faceControl;
    public ClothChangerPapers clothManager;

    private void Start()
    {
        //clothManager.RandomCloth();
    }

    void Update()
    {

        GetObjects();

        if (AccessControlManager.instance.currentState == AccessControlManager.GameState.Play)
        {

            if (waypoint != null)
            {
                if (Vector3.Distance(this.transform.position, waypoint.transform.position) > 0.1f)
                {
                    foreach (var item in animator)
                    {
                        if (!item.GetBool("Walking"))
                        {
                            item.SetBool("Walking", true);
                        }
                    }
                    this.transform.position = Vector3.MoveTowards(this.transform.position, waypoint.position, speed * Time.deltaTime);
                }
                else if (voted)
                {
                    Destroy(this.gameObject);
                }
                else if (animator[0].GetBool("Walking") == true)
                {
                    foreach (var item in animator)
                    {
                        item.SetBool("Walking", false);
                    }
                }
            }

        }

    }

    public void SmellActivate()
    {
        GetObjects();
        foreach (var item in animator)
        {
            item.SetTrigger("Smell");
        }
        StartCoroutine(faceControl.GrossFace(4));
    }

    public void GetObjects()
    {
        animator.Clear();
        animator.Add(clothManager.currentCabeza.GetComponent<Animator>());
        animator.Add(clothManager.currentHeadGO.GetComponent<Animator>());
        animator.Add(clothManager.currentMaskGO.GetComponent<Animator>());
        animator.Add(clothManager.currentPantsGO.GetComponent<Animator>());
        animator.Add(clothManager.currentShirtGO.GetComponent<Animator>());
        animator.Add(clothManager.currentShoesGO.GetComponent<Animator>());

        foreach (var item in animator)
        {
            item.runtimeAnimatorController = animatorController;
        }
        mask = clothManager.currentMaskGO;
        faceControl = clothManager.currentCabeza.GetComponent<Cabeza>().cabeza.GetChild(1).GetComponent<CarasControl>();
    }
}
