using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class InteractInInspector : MonoBehaviour
{
    public Transform mainCamera;

    private GameManager gameManager;

    private Ray ray;
    private RaycastHit hitInfo;

    public float rayRange;

    public LayerMask interactionLayer;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate()
    {
        ray.origin = mainCamera.position;
        ray.direction = mainCamera.forward;

        if (Physics.Raycast(ray, out hitInfo, rayRange, interactionLayer))
        {
            if (hitInfo.collider.gameObject.CompareTag("Clue"))
            {
                gameManager.clueText.text = "Há algo aqui";
                gameManager.hasClue = true;
            }
            else
            {
                gameManager.clueText.text = "";
                gameManager.hasClue = false;
            }
        }
        else
        {
            gameManager.clueText.text = "";
            gameManager.hasClue = false;
        }
    }


}

 

