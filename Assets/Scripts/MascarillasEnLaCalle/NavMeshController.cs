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

    [HideInInspector]public NavMeshAgent agent;
    Transform nextPos;
    NavMeshPath navMeshPath;
    public float runnerSpeed = 8;
    float runnerAcceleration = 20;
    float runnerAngularSpeed = 200;
    public float walkerSpeed = 3.5f;
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
        this.gameObject.SetActive(false);

    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (PathComplete() || Vector2.Distance(Utils.ToVector2(this.transform.position), Utils.ToVector2(agent.destination))<1)
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
            else
            {
                pedestriansScript.animator.SetFloat("Speed", agent.speed);

            }
        }

    }

    public void IAmaRunner()
    {
        agent.speed = runnerSpeed;
        agent.acceleration = runnerAcceleration;
        agent.angularSpeed = runnerAngularSpeed;
        agent.avoidancePriority = Random.Range(1, 24);
    }
    public void IAmaWalker()
    {
        agent.speed = walkerSpeed;
        agent.acceleration = walkerAcceleration;
        agent.angularSpeed = walkerAngularSpeed;
        agent.avoidancePriority = Random.Range(25, 50);

    }

    public void PickRoute(WayPoint l_Waypoint)
    {
        waypoint = l_Waypoint;
        waypoint = firstWaypoint;
        transform.position = l_Waypoint.GetPosition();
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

            if (tMin.GetComponent<TrafficLight>().currentState == TrafficLight.trafficLightState.Green || tMin.GetComponent<TrafficLight>().currentState == TrafficLight.trafficLightState.Yellow)
            {
                agent.isStopped = true;
                pedestriansScript.animator.SetFloat("Speed", 0);
            }
            else
            {
                agent.isStopped = false;
                pedestriansScript.animator.SetFloat("Speed", 0);

            }
        }
    }
}
