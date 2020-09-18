using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    public Transform[] routePoints;
    NavMeshAgent agent;
    Transform nextPos;
    NavMeshPath navMeshPath;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        nextPos = routePoints[Random.Range(0, routePoints.Length - 1)];
        agent.destination = nextPos.position;
        navMeshPath = agent.path;
    }

    // Update is called once per frame
    void Update()
    {
        if(!agent.hasPath || navMeshPath.status != NavMeshPathStatus.PathComplete)
        {
            CreateNewPath();
        }
    }

    void CreateNewPath()
    {
        Transform currentPos = nextPos;
        nextPos = routePoints[Random.Range(0, routePoints.Length - 1)];

        if (nextPos != currentPos)
        {
            agent.destination = nextPos.position;
            navMeshPath = agent.path;
        }
    }
}
