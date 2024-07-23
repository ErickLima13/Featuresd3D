using System.Collections;
using UnityEngine;

namespace ThirdPerson
{
    public class PointOfView : MonoBehaviour
    {
        private FieldOfView fov;

        public enum Enemy
        {
            Gargoyle, Ghost
        }

        public Enemy type;

        private Transform player;

        private WaypointPatrol patrol;
        private Gargoyle gargoyle;

        public float waitTime;

        private void Start()
        {
            fov = GetComponent<FieldOfView>();

            if (type == Enemy.Ghost)
            {
                patrol = GetComponentInParent<WaypointPatrol>();
            }
            else
            {
                gargoyle = GetComponentInParent<Gargoyle>();
            }
        }

        private void Update()
        {
            if (fov.ThePlayerIsInRange())
            {
                player = fov.GetPlayer();

                switch (type)
                {
                    case Enemy.Ghost:
                        print("fantasma viu");
                        patrol.SetFollow(player, true);
                        StopCoroutine(DelayFollow());
                        break;
                    case Enemy.Gargoyle:
                        print("Gargoyle viu");
                        gargoyle.SetTheTarget(player, true);
                        StartCoroutine(DelayToPatrol());
                        break;
                }
            }
            else
            {
                player = null;

                switch (type)
                {
                    case Enemy.Ghost:
                        StartCoroutine(DelayFollow());
                        break;
                }
            }
        }

        private IEnumerator DelayToPatrol()
        {
            yield return new WaitForSeconds(3f);
            gargoyle.SetTheTarget(player, false);
        }

        private IEnumerator DelayFollow()
        {
            yield return new WaitForSeconds(waitTime);
            patrol.SetFollow(player, false);
            print("Trabaia");
        }


    }
}