using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrians : MonoBehaviour
{
    public PedestriansManager.PedestrianType thisType;
    public GameObject virus = null;
    public bool masked = false;
    public GameObject mask;
    [HideInInspector] public Animator animator;
    NavMeshController navController;

    void Awake()
    {
        navController = GetComponent<NavMeshController>();
        animator = GetComponentInChildren<Animator>();
    }

    public void Infection()
    {
        if (thisType == PedestriansManager.PedestrianType.Runner)
        {
            thisType = PedestriansManager.PedestrianType.Runner_Infected;
        }
        else if (thisType == PedestriansManager.PedestrianType.Non_Masked)
        {
            thisType = PedestriansManager.PedestrianType.Infected;
        }

        InfecetedController iControl = gameObject.AddComponent<InfecetedController>();
        iControl.infectionCooldown = 2;
        virus = VSFX.instance.CreateParticleSystem(VSFX.instance.infectedPS, gameObject.transform.position, true);
        virus.transform.parent = this.transform;
    }

    public void MaskOn()
    {
        Debug.Log("Put your fucking mask on!");

        if(this.gameObject.activeSelf)
        StartCoroutine("PuttingMask");
        VSFX.instance.CreateParticleSystem(VSFX.instance.convertPS, transform.position, false);
        VSFX.instance.PlayAudio(VSFX.instance.convertedSound);

        thisType = PedestriansManager.PedestrianType.Masked;
        PutMask();

        if (virus != null)
        {
            Destroy(virus);
            virus = null;
        }
        if (this.GetComponent<InfecetedController>() != null)
            Destroy(this.GetComponent<InfecetedController>());
    }

    public void PutMask()
    {
        masked = true;
        mask.SetActive(true);
        //estethic
    }

    public void NormalNoMask()
    {
        masked = false;
        mask.SetActive(false);
    }

    public IEnumerator PuttingMask()
    {
        animator.SetTrigger("MaskOn");

        float iniSpeed = navController.agent.speed;
        navController.agent.speed = 0;

        yield return new WaitForSeconds(3.5f);
        navController.agent.speed = iniSpeed;

    }
}
