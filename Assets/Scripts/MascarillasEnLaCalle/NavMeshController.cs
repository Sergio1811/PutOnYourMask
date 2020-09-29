using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    public Transform[] routePoints1;
    public Transform[] routePoints2;
    public Transform[] routePoints3;
    public Transform[] routePoints4;
    public Transform[] currentRoute;

    NavMeshAgent agent;
    Transform nextPos;
    NavMeshPath navMeshPath;
    float runnerSpeed = 8;
    float runnerAcceleration = 20;
    float runnerAngularSpeed = 200;
    float walkerSpeed = 3.5f;
    float walkerAcceleration = 8;
    float walkerAngularSpeed = 120;

    Pedestrians pedestriansScript;

    public WayPoint waypoint;
    WayPoint firstWaypoint;

    int direction;

    // Start is called before the first frame update
    void Awake()
    {
        firstWaypoint = waypoint;
        direction = 0;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoint.GetPosition());
        pedestriansScript = GetComponent<Pedestrians>();

    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (PathComplete())
            {
                bool shouldBranch = false;

                if (waypoint.nextWaypoint != null)
                {
                    if (waypoint.branches != null && waypoint.branches.Count > 0)
                    {
                        shouldBranch = Random.Range(0f, 1f) <= waypoint.branchRatio ? true : false;
                    }

                    if (waypoint.crossWalk)
                    {
                        CanICross();
                    }

                    if (shouldBranch)
                    {
                        waypoint = waypoint.branches[Random.Range(0, waypoint.branches.Count - 1)];
                    }
                    else
                    {
                        if (direction == 0)
                        {
                            waypoint = waypoint.nextWaypoint;
                        }

                        else if (direction == 1)
                        {
                            waypoint = waypoint.previousWaypoint;
                        }
                    }

                    agent.SetDestination(waypoint.GetPosition());
                }
                else
                {
                    gameObject.SetActive(false);
                    if (pedestriansScript.thisType == PedestriansManager.PedestrianType.Infected || pedestriansScript.thisType == PedestriansManager.PedestrianType.Runner_Infected)
                    {
                        pedestriansScript.MaskOn();
                    }
                }
            }
            else if(agent.isStopped)
            {
                CanICross();
            }
        }

    }

    public void IAmaRunner()
    {
        agent.speed = runnerSpeed;
        agent.acceleration = runnerAcceleration;
        agent.angularSpeed = runnerAngularSpeed;
    }
    public void IAmaWalker()
    {
        agent.speed = walkerSpeed;
        agent.acceleration = walkerAcceleration;
        agent.angularSpeed = walkerAngularSpeed;
    }

    public void PickRoute()
    {
        waypoint = firstWaypoint;
        transform.position = waypoint.GetPosition();
    }

    protected bool PathComplete()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void CanICross()
    {
        List<GameObject> currentNearbyTL = new List<GameObject>(PedestriansManager.instance.trafficLights);       

        if (currentNearbyTL.Count > 0)
        {
            Transform tMin = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = transform.position;

            foreach (GameObject t in currentNearbyTL)
            {
                float dist = Vector3.Distance(t.transform.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t.transform;
                    minDist = dist;
                }
            }

            if (tMin.GetComponent<TrafficLight>().currentState == TrafficLight.trafficLightState.Green)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }
        }
    }
}
