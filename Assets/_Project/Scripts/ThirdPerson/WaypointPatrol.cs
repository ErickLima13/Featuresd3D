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

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();

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
                target = player.position;
                agent.SetDestination(target);

                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    player.GetComponent<PlayerMovement>().SetPlayer(); // arrumar isso aqui, fazer o um fade na tela e o fantasma parar de seguir
                    print("peguei o player");
                    hasPlayer = false;
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