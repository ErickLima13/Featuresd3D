using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ThirdPerson
{
    public class GhostController : MonoBehaviour
    {
        private NavMeshAgent agent;
        private FieldOfView fov;

        public List<Transform> waypoints = new();

        [SerializeField] private int currenWaypointId;

        [SerializeField] private Vector3 target;

        [SerializeField] private Transform player;

        [SerializeField] private PlayerMovement playerMovement;

        public bool hasCommand;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            fov = GetComponentInChildren<FieldOfView>();
            playerMovement = FindObjectOfType<PlayerMovement>();

            target = waypoints[currenWaypointId].position;
            agent.SetDestination(target);
        }

        private void Update()
        {
            if (!hasCommand)
            {
                if (fov.ThePlayerIsInRange() && !playerMovement.alreadyGot)
                {
                    Chase();
                }
                else
                {
                    Patrol();
                }
            }
        }

        private void Chase()
        {
            player = fov.GetPlayer();

            if (agent.remainingDistance < agent.stoppingDistance)
            {
                playerMovement.IWasTaken();

                print("peguei o player");
            }
            else
            {
                if (player != null)
                {
                    target = player.position;
                }
            }

            agent.SetDestination(target);
        }

        private void Patrol()
        {
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                currenWaypointId = (currenWaypointId + 1) % waypoints.Count; // trazendo o resto da divisão
                target = waypoints[currenWaypointId].position;
                agent.SetDestination(target);
            }
        }

        public void SetToFollowPlayer(bool value, Transform playerPos)
        {
            hasCommand = value;
            player = playerPos;

            if (player != null)
            {
                agent.SetDestination(player.position);
            }
        }
    }
}