using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private GameManager gameManager;

    public float mouseSensitivity;
    
    public float minRotX, maxRotX;

    public Transform playerBody;

    private float xRotation;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (gameManager.NonGameplay())
        {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minRotX, maxRotX);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }


}
