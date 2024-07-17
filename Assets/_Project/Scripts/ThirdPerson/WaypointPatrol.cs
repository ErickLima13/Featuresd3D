using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    private NavMeshAgent agent;

    public Transform[] waypoints;

    private int currenWaypointId;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoints[0].position);
    }

    private void Update()
    {
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            currenWaypointId = (currenWaypointId +1 ) % waypoints.Length;
            agent.SetDestination(waypoints[currenWaypointId].position);
        }
    }
}
