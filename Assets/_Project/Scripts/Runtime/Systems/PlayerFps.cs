using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFps : MonoBehaviour
{
    private CharacterController controller;
    private GameManager gameManager;

    [Header("Attributes")]
    public float baseSpeed;
    public float maxSpeed;
    private float speed;
    public float jumpForce;
   
    [Header("Control Gravity")]
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private bool isGrounded;
    private Vector3 velocity;
    private float gravity;
    public float gravityScale;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        gameManager = FindObjectOfType<GameManager>();
        gravity = Physics.gravity.y;
        speed = baseSpeed;
    }

    private void Update()
    {
        if (gameManager.NonGameplay())
        {
            return;
        }

        Move();
        ControlGravity();
        Jump();  
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, whatIsGround);
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Run"))
        {
            speed = maxSpeed;
        }

        if (Input.GetButtonUp("Run"))
        {
            speed = baseSpeed;
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    private void ControlGravity()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * gravityScale * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = jumpForce;
        }
    }
}
