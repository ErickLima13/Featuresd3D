using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    private GameManager gameManager;
    private Transform player;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = gameManager.PlayerFps;
    }

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;

        transform.position = newPosition;
    }
}
