using System.Collections;
using UnityEngine;

namespace ThirdPerson
{
    public class PointOfView : MonoBehaviour
    {
        // ver se vai fazer o sistema dele rotacionar

        public enum Enemy
        {
            Gargoyle, Ghost
        }

        public Enemy type;

        private bool isPlayerInRange;

        private Transform player;

        private WaypointPatrol patrol;

        public bool isFollow;

        public float waitTime;

        private void Start()
        {
            if (type == Enemy.Ghost)
            {
                patrol = GetComponentInParent<WaypointPatrol>();
            }
        }

        private void Update()
        {
            if (isPlayerInRange)
            {
                Vector3 direction = player.position - transform.position + Vector3.up; // direção
                Ray ray = new(transform.position, direction);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        switch (type)
                        {
                            case Enemy.Gargoyle:

                                break;
                            case Enemy.Ghost:

                                break;

                        }
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                player = col.transform;
                isPlayerInRange = true;

                switch (type)
                {
                    case Enemy.Ghost:
                        print("fantasma viu");
                        patrol.SetFollow(player, true);
                        StopCoroutine(DelayFollow());
                        isFollow = false;
                        break;
                    case Enemy.Gargoyle:
                        break;

                }
            }
        }

        private void OnTriggerExit(Collider col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                player = null;
                isPlayerInRange = false;

                switch (type)
                {
                    case Enemy.Ghost:

                        if (!isFollow)
                        {
                            print("NAO VEJO");
                            StartCoroutine(DelayFollow());
                        }

                        break;
                    case Enemy.Gargoyle:
                        break;

                }
            }
        }

        private IEnumerator DelayFollow()
        {
            print("VAMO AI");
            isFollow = true;
            yield return new WaitForSeconds(waitTime);
            patrol.SetFollow(player, false);
            print("Trabaia");
        }


    }
}