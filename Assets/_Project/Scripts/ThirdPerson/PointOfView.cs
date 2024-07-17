using UnityEngine;

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
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player = null;
            isPlayerInRange = false;
        }
    }


}
