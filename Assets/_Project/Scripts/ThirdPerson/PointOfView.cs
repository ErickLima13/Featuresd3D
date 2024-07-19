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

        public bool isFollow;

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
                        isFollow = false;
                        break;
                    case Enemy.Gargoyle:
                        print("Gargoyle viu");
                        gargoyle.SetTheTarget(player,true);
                        break;
                }
            }
            else
            {
                player = null;

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