using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Range(0, 15)] public float viewRadius;
    [Range(0, 360)] public float viewAngle;

    public List<Transform> visibleTargets = new List<Transform>();
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public float viewTime;

    private void Start()
    {
        StartCoroutine(FindTarget());
    }

    private IEnumerator FindTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(viewTime);
            FindVisibleTarget();
        }

    }

    public Vector3 DirFromAngle(float angleInDegress)
    {
        angleInDegress += transform.eulerAngles.y;
        return new
            Vector3(Mathf.Sin(angleInDegress * Mathf.Deg2Rad),
          0, Mathf.Cos(angleInDegress * Mathf.Deg2Rad));
    }

    private void FindVisibleTarget()
    {
        visibleTargets.Clear();

        Collider[] targetInViewRadius = Physics.OverlapSphere(
            transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetInViewRadius.Length; i++)
        {
            Transform target = targetInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            Debug.DrawRay(transform.position, dirToTarget * viewAngle, Color.green);

            if (Vector3.Angle(transform.forward, dirToTarget) <
                viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position,
                    dirToTarget, distToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    public bool ThePlayerIsInRange()
    {
        return visibleTargets.Count > 0;
    }

    public Transform GetPlayer()
    {
        return visibleTargets[0];
    }

}
