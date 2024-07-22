using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ThirdPerson
{
    public class PlayerMovement : MonoBehaviour
    {

        // fazer o evento de (visual) exclamação ao estar perto de um inimigo
        // ver se ele vai fazer o esquema da camera (cutscene) para começar o jogo, se não fazer.

        private GameManager manager;

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

        [Header("Audio")]
        private AudioSource audioSource;

        private void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
            manager = FindObjectOfType<GameManager>();
            cam = Camera.main.transform;

            defaultCam.SetActive(true);
            lookAtCam.SetActive(false);
        }

        private void Update()
        {
            // essa mecanica vai pra um botão na tela

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
            if (isLookAt)
            {
                return;
            }

            bool hasHorizontalInput = !Mathf.Approximately(movement.x, 0);
            bool hasVerticalInput = !Mathf.Approximately(movement.y, 0);
            bool isWalking = hasHorizontalInput || hasVerticalInput;

            if (isWalking)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else
            {
                audioSource.Stop();
            }

            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, new Vector3(movement.x, 0, movement.y), turnSpeed * Time.deltaTime, 0);
            rotation = Quaternion.LookRotation(desiredForward);

            animator.SetBool("isWalking", isWalking);
        }

        public void SetMovement(InputAction.CallbackContext value)
        {
            movement = value.ReadValue<Vector2>();
        }

        private void OnAnimatorMove()
        {
            rb.MovePosition(rb.position + new Vector3(movement.x,0,movement.y) * animator.deltaPosition.magnitude);
            rb.MoveRotation(rotation);
        }

        private void OnTriggerEnter(Collider col)
        {
            // trocar para um script generico

            switch (col.tag)
            {
                case "Item":

                    IdItem id = col.GetComponent<IdItem>();

                    switch (id.type)
                    {
                        case ItemType.Gema:
                            break;
                        case ItemType.Key:
                            manager.GetKey();
                            Destroy(col.gameObject);
                            break;
                    }


                    break;
                case "Exit":
                    manager.CheckKeys();
                    break;

            }
        }
    }
}