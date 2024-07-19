using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ThirdPerson;
using UnityEngine;

public class Gargoyle : MonoBehaviour
{
    public List<WaypointPatrol> ghosts = new List<WaypointPatrol>();

    public Quaternion[] rotations;
    public float rotateTime;
    public int idRot;
    public float speedRot;

    public bool canRotate;

    public bool changeDirection;

    private void Start()
    {
        ghosts = FindObjectsOfType<WaypointPatrol>().ToList();
    }

    public void SetTheTarget(Transform target, bool value)
    {
        foreach (WaypointPatrol wp in ghosts)
        {
            wp.SetFollow(target, value);
        }
    }

    private void Update()
    {
        if (!canRotate)
        {
            Quaternion rot = Quaternion.Slerp(transform.rotation, rotations[idRot], speedRot * Time.deltaTime);
            rot.x = 0;
            rot.z = 0;

            transform.rotation = rot;

            if (transform.rotation == rotations[idRot])
            {
                StartCoroutine(DelayRotate());
            }
        }

    }

    private IEnumerator DelayRotate()
    {

        if (!changeDirection)
        {
            if (idRot >= rotations.Length - 1)
            {
                changeDirection = true;
            }
            else
            {
                idRot++;
            }
        }
        else
        {
            if (idRot == 0)
            {
                changeDirection = false;
            }
            else
            {
                idRot--;
            }

        }

        canRotate = true;
        yield return new WaitForSeconds(rotateTime);
        canRotate = false;
    }
}
