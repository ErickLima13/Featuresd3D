using UnityEngine;
using UnityEngine.AI;

public class MovementMobile : MonoBehaviour
{
    private RaycastHit hit;

    public NavMeshAgent agent;

    public LayerMask groundMask;

    private Camera cam;

    public Animator animator;

    public bool isWalk;

    public bool isMobile;

    public Vector3 movement;


    public ParticleSystem clickEffect;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {

        if (!isMobile)
        {
            return;
        }

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = cam.ScreenToWorldPoint(mousePos);

        Debug.DrawRay(cam.transform.position, mousePos - transform.position, Color.green);

        if (agent.velocity.magnitude != 0)
        {
            print("andando");
            isWalk = true;
        }
        else
        {
            print("parado");
            isWalk = false;
        }

        animator.SetBool("isWalking", isWalk);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100, groundMask))
            {
                print(hit.collider.name);

                movement = hit.point;
                agent.SetDestination(movement);

                if (clickEffect != null)
                {
                    ParticleSystem temp = Instantiate(clickEffect, movement += new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
                    Destroy(temp.gameObject, 0.5f);
                }
            }
        }
    }


}
