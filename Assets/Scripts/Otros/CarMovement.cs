using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Transform path;
    public List<Transform> nodes;
    private int currentNode = 0;

    float currentSpeed;
    public float maxSpeed;

    public Transform trafficLightSensor;
    public float distanceToStopOnTrafficLight;

    List<GameObject> trafficLightsNearby;


    private void Start()
    {
        Transform[] pathTransform = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransform.Length; i++)
        {
            if (pathTransform[i] != path.transform)
            {
                nodes.Add(pathTransform[i]);
            }
        }

        trafficLightsNearby = new List<GameObject>(PedestriansManager.instance.trafficLights);

    }

    private void Update()
    {
        Drive();
        CheckWaypointDistance();

        currentSpeed = maxSpeed;
        CheckTrafficLight();
    }


    private void Drive()
    {
        transform.position = Vector3.MoveTowards(transform.position, nodes[currentNode].position, currentSpeed * Time.deltaTime);
        transform.LookAt(nodes[currentNode]);

    }

    private void CheckWaypointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 0.05f)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }

    private void CheckTrafficLight()//Falta comprobar que esta mirnado al semaforo y no lo ha pasado ya
    {
        List<GameObject> currentNearbyTL = new List<GameObject>(trafficLightsNearby);

        currentNearbyTL = Utils.GetNearbyObjectivesFromTarget(trafficLightSensor, distanceToStopOnTrafficLight, trafficLightsNearby);

        if (currentNearbyTL.Count > 0)
        {
            for (int i = 0; i < currentNearbyTL.Count; i++)
            {
                if (currentNearbyTL[i].GetComponent<TrafficLight>().currentState == TrafficLight.trafficLightState.Red)
                {
                    currentSpeed = 0;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(trafficLightSensor.position, distanceToStopOnTrafficLight);
    }

}


