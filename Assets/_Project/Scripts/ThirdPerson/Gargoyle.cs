using System;
using System.Collections;
using ThirdPerson;
using UnityEngine;

public class Gargoyle : MonoBehaviour
{
    public event Action<Transform> OnGargoyleSeeThePlayer;

    private FieldOfView fov;
    public Quaternion[] rotations;
    public float rotateTime;
    public int idRot;
    public float speedRot;

    public bool canRotate;

    public bool changeDirection;

    private PlayerMovement playerMovement;

    private void Start()
    {
        fov = GetComponentInChildren<FieldOfView>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (fov.ThePlayerIsInRange() && !playerMovement.alreadyGot)
        {
            OnGargoyleSeeThePlayer?.Invoke(fov.GetPlayer());
        }

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
