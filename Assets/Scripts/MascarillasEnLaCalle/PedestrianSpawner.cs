using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianSpawner : MonoBehaviour
{
   

    [Header("Put normal percentage, script add it for you")]
    public float maskedPercentage;
    public float nonMaskedPercentage;
    public float infectedPercentage;
    public float runnerPercentage;
    public float runnerInfectedPercentage;
    

    //convert all of this into editor staff
    void Start()
    {
        nonMaskedPercentage += maskedPercentage;
        infectedPercentage += nonMaskedPercentage;
        runnerPercentage += infectedPercentage;
        runnerInfectedPercentage += runnerPercentage;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            SpawnChar();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject newGO = (GameObject)Instantiate(Resources.Load("Prefabs/PedestrianChars/Pedestrian"));
            SpawnType(PedestriansManager.PedestrianType.Infected, newGO);
            PedestriansManager.instance.pedestriansList.Add(newGO);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameObject newGO = (GameObject)Instantiate(Resources.Load("Prefabs/PedestrianChars/Pedestrian"));
            SpawnType(PedestriansManager.PedestrianType.Masked, newGO);
            PedestriansManager.instance.pedestriansList.Add(newGO);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameObject newGO = (GameObject)Instantiate(Resources.Load("Prefabs/PedestrianChars/Pedestrian"));
            SpawnType(PedestriansManager.PedestrianType.Non_Masked, newGO);
            PedestriansManager.instance.pedestriansList.Add(newGO);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GameObject newGO = (GameObject)Instantiate(Resources.Load("Prefabs/PedestrianChars/Pedestrian"));
            SpawnType(PedestriansManager.PedestrianType.Runner, newGO);
            PedestriansManager.instance.pedestriansList.Add(newGO);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            GameObject newGO = (GameObject)Instantiate(Resources.Load("Prefabs/PedestrianChars/Pedestrian"));
            SpawnType(PedestriansManager.PedestrianType.Runner_Infected, newGO);
            PedestriansManager.instance.pedestriansList.Add(newGO);
        }


    }

    public void SpawnChar()
    {
        PedestriansManager.PedestrianType nextCharType = ChooseType();
        print(nextCharType);
        GameObject newGO = (GameObject)Instantiate(Resources.Load("Prefabs/PedestrianChars/Pedestrian"));
        SpawnType(nextCharType, newGO);
        PedestriansManager.instance.pedestriansList.Add(newGO);
    }

    public PedestriansManager.PedestrianType ChooseType() //Mejorable
    {
        float randomValue = Random.Range(0, 100);
        Debug.Log(randomValue);
       

        if (randomValue < maskedPercentage)
        {
            return PedestriansManager.PedestrianType.Masked;
        }
        else if (randomValue < nonMaskedPercentage)
        {
            return PedestriansManager.PedestrianType.Non_Masked;
        }
        else if (randomValue < infectedPercentage)
        {
            return PedestriansManager.PedestrianType.Infected;
        }
        else if (randomValue < runnerPercentage)
        {
            return PedestriansManager.PedestrianType.Runner;
        }
        else
            return PedestriansManager.PedestrianType.Runner_Infected;

    }

    public void SpawnType(PedestriansManager.PedestrianType type, GameObject GO)
    {
        Pedestrians localPedestrians = GO.GetComponent<Pedestrians>();
        localPedestrians.thisType = type;
        switch (type)
        {
            case PedestriansManager.PedestrianType.Masked:
                localPedestrians.PutMask();
                break;
            case PedestriansManager.PedestrianType.Non_Masked:
                //Do nothing
                break;
            case PedestriansManager.PedestrianType.Infected:
                localPedestrians.Infection();
                break;
            case PedestriansManager.PedestrianType.Runner:
                //Change IA
                break;
            case PedestriansManager.PedestrianType.Runner_Infected:
                localPedestrians.Infection();
                //Change IA and Infect
                break;
            default:
                break;
        }
    }
}
