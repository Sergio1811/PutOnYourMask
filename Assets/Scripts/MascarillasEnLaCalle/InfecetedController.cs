﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfecetedController : MonoBehaviour
{

    [Tooltip("El tiempo que va a estar infectando y el tiempo que no, en bucle")]public float infectionCooldown;
    float currentCooldown;
    bool infecting = false;

    void Start()
    {

    }

    void Update()
    {
        if (GetComponent<Pedestrians>().thisType == PedestriansManager.PedestrianType.Runner_Infected)
        {
            CheckNearbyToInfect();
        }
        else if(GetComponent<Pedestrians>().thisType == PedestriansManager.PedestrianType.Infected)
        {
            if(Mathf.Sin(Time.time)>0)
            {
                print("infecting!!");
                CheckNearbyToInfect();
                if(!this.GetComponent<Pedestrians>().virus.activeSelf && this.GetComponent<Pedestrians>().virus!=null)
                this.GetComponent<Pedestrians>().virus.SetActive(true);

            }
            else
            {
                if (this.GetComponent<Pedestrians>().virus.activeSelf)
                    this.GetComponent<Pedestrians>().virus.SetActive(false);
            }
            /*
            if (infecting)
            {
                CheckNearbyToInfect();
                currentCooldown -= Time.deltaTime;
                if (currentCooldown <= 0)
                {
                    infecting = !infecting;
                    this.GetComponent<Pedestrians>().virus.SetActive(false);
                    //doNotSpeak
                }
            }
            else
            {
                currentCooldown += Time.deltaTime;
                if (currentCooldown >= infectionCooldown)
                {
                    infecting = !infecting;
                    this.GetComponent<Pedestrians>().virus.SetActive(true);
                    //Speak 
                }
            }*/
        }
        else
        {
            if (this.GetComponent<Pedestrians>().virus != null)
                Destroy(this.GetComponent<Pedestrians>().virus);

            Destroy(this);
        }
    }

    void CheckNearbyToInfect()
    {
        List<GameObject> nearbyObjectives = Utils.GetNearbyObjectivesFromTarget(gameObject.transform, PedestriansManager.instance.distanceToInfect, PedestriansManager.instance.pedestriansList);

        if (nearbyObjectives != null)
        {
            foreach (var item in nearbyObjectives)
            {
                if (item.GetComponent<Pedestrians>().thisType == PedestriansManager.PedestrianType.Non_Masked || item.GetComponent<Pedestrians>().thisType == PedestriansManager.PedestrianType.Runner)
                {
                    item.GetComponent<Pedestrians>().Infection();
                }               
            }
        }
    }
}