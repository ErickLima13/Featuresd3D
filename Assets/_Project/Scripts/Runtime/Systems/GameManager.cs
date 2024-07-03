using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform PlayerFps;

    public float distanceInteraction;

    public Sprite[] icons; 

    private void Awake()
    {
        PlayerFps = FindObjectOfType<PlayerFps>().transform;

    }
}
