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

    float currentTime;
    float nextTime;

    public WayPoint[] wayPointsSpawn;

    //convert all of this into editor staff
    void Start()
    {
        nextTime = Random.Range(0.2f, 1.5f);
        nonMaskedPercentage += maskedPercentage;
        infectedPercentage += nonMaskedPercentage;
        runnerPercentage += infectedPercentage;
        runnerInfectedPercentage += runnerPercentage;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SpawnChar();
        }

        currentTime += Time.deltaTime;

        if (currentTime >= nextTime)
        {
            SpawnChar();
            nextTime = Random.Range(2f, 2.5f);
            currentTime = 0;

        }

    }

    public void SpawnChar()
    {
        PedestriansManager.PedestrianType nextCharType = ChooseType();
        GameObject newGO = ObjectPooler.SharedInstance.GetPooledObject();
        if (newGO != null)
        {
            newGO.GetComponent<NavMeshController>().PickRoute(wayPointsSpawn[Random.Range(0, wayPointsSpawn.Length)]);
            SpawnType(nextCharType, newGO);
            PedestriansManager.instance.pedestriansList.Add(newGO);
            newGO.SetActive(true);
        }
    }

    public PedestriansManager.PedestrianType ChooseType() //Mejorable
    {
        float randomValue = Random.Range(0, 100);

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
        {
            return PedestriansManager.PedestrianType.Runner_Infected;
        }
    }

    public void SpawnType(PedestriansManager.PedestrianType type, GameObject GO)
    {
        Pedestrians localPedestrians = GO.GetComponent<Pedestrians>();
        localPedestrians.thisType = type;
        switch (type)
        {
            case PedestriansManager.PedestrianType.Masked:
                localPedestrians.PutMask();
                GO.GetComponent<NavMeshController>().IAmaWalker();
                break;
            case PedestriansManager.PedestrianType.Non_Masked:
                localPedestrians.NormalNoMask();
                GO.GetComponent<NavMeshController>().IAmaWalker();
                //Do nothing
                break;
            case PedestriansManager.PedestrianType.Infected:
                localPedestrians.NormalNoMask();
                GO.GetComponent<NavMeshController>().IAmaWalker();
                localPedestrians.Infection();
                break;
            case PedestriansManager.PedestrianType.Runner:
                localPedestrians.NormalNoMask();
                GO.GetComponent<NavMeshController>().IAmaRunner();
                //Change IA
                break;
            case PedestriansManager.PedestrianType.Runner_Infected:
                localPedestrians.NormalNoMask();
                GO.GetComponent<NavMeshController>().IAmaRunner();
                localPedestrians.Infection();
                //Change IA and Infect
                break;
            default:
                break;
        }
    }
}
