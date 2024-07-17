using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // tentar fazer o movimento com o mouse pra deixar o jogo mobile

    private Animator animator;
    private Rigidbody rb;
    private Vector3 movement;
    private quaternion rotation = quaternion.identity;

    public float turnSpeed;

    [Header("Cam Controlls")]
    public GameObject defaultCam;
    public GameObject lookAtCam;
    private Transform cam;
    private bool isLookAt;
       

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;

        defaultCam.SetActive(true);
        lookAtCam.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isLookAt)
        {
            isLookAt = true;
            defaultCam.SetActive(false);
            lookAtCam.SetActive(true);
            animator.SetBool("isWalking", false);
        }

        if (Input.GetMouseButtonUp(1))
        {
            defaultCam.SetActive(true);
            lookAtCam.SetActive(false);
            StartCoroutine(ControlCam());
        }
    }

    private IEnumerator ControlCam()
    {
        yield return new WaitUntil(() => 
        cam.position == defaultCam.transform.position && cam.rotation == defaultCam.transform.rotation);

        isLookAt = false;
    }

    private void FixedUpdate()
    {
        if (isLookAt) {return;}

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movement.Set(horizontal,0,vertical);
        movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal,0);
        bool hasVerticalInput = !Mathf.Approximately(vertical,0);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime,0);
        rotation = Quaternion.LookRotation(desiredForward);

        animator.SetBool("isWalking", isWalking);
    }

    private void OnAnimatorMove()
    {
        rb.MovePosition(rb.position + movement * animator.deltaPosition.magnitude);
        rb.MoveRotation(rotation);
    }
}
