using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ThirdPerson
{
    public class WaypointPatrol : MonoBehaviour
    {
        private NavMeshAgent agent;

        public List<Transform> waypoints = new();

        [SerializeField] private int currenWaypointId;

        public bool isRandomWay;

        private Vector3 target;

        private Transform player;

        private bool hasPlayer;

        private PlayerMovement PlayerMovement;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            PlayerMovement = FindObjectOfType<PlayerMovement>();

            if (isRandomWay)
            {
                currenWaypointId = Random.Range(0, waypoints.Count);
            }

            target = waypoints[currenWaypointId].position;
            agent.SetDestination(target);
        }

        private void Update()
        {
            if (hasPlayer)
            {
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    PlayerMovement.IWasTaken();
                    print("peguei o player");
                }
                else
                {
                    target = player.position;
                    agent.SetDestination(target);
                }
            }
            else
            {
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    if (isRandomWay)
                    {
                        int nextWay = Random.Range(0, waypoints.Count);

                        if (nextWay == currenWaypointId)
                        {
                            currenWaypointId = (currenWaypointId + 1) % waypoints.Count;
                        }
                        else
                        {
                            currenWaypointId = nextWay;
                        }
                    }
                    else
                    {
                        currenWaypointId = (currenWaypointId + 1) % waypoints.Count; // trazendo o resto da divisão
                    }

                    target = waypoints[currenWaypointId].position;
                    agent.SetDestination(target);
                }
            }
        }

        public void SetFollow(Transform playerT, bool hPlayer)
        {
            hasPlayer = hPlayer;
            player = playerT;
        }
    }
}