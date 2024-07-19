using UnityEngine;
using UnityEngine.AI;

namespace ThirdPerson
{
    public class WaypointPatrol : MonoBehaviour
    {
        private NavMeshAgent agent;

        public Transform[] waypoints;

        [SerializeField] private int currenWaypointId;

        public bool isRandomWay;

        private Vector3 target;

        private Transform player;

        private bool isFollow;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();

            if (isRandomWay)
            {
                currenWaypointId = Random.Range(0, waypoints.Length);
            }

            target = waypoints[currenWaypointId].position;
            agent.SetDestination(target);
        }

        private void Update()
        {
            if (isFollow)
            {
                target = player.position;
                agent.SetDestination(target);
            }
            else
            {
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    if (isRandomWay)
                    {
                        int nextWay = Random.Range(0, waypoints.Length);

                        if (nextWay == currenWaypointId)
                        {
                            currenWaypointId = (currenWaypointId + 1) % waypoints.Length;
                        }
                        else
                        {
                            currenWaypointId = nextWay;
                        }
                    }
                    else
                    {
                        currenWaypointId = (currenWaypointId + 1) % waypoints.Length; // trazendo o resto da divisão
                    }

                    target = waypoints[currenWaypointId].position;
                    agent.SetDestination(target);
                }
            }
        }

        public void SetFollow(Transform playerT, bool follow)
        {
            isFollow = follow;
            player = playerT;
        }
    }
}