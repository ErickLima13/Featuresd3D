using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public enum GameState
{
    Gameplay, Item
}

public class GameManager : MonoBehaviour
{
    public GameState currentState;

    public Transform PlayerFps;

    public float distanceInteraction;

    public Sprite[] icons;

    [Header("Map")]
    public GameObject mapCam;
    public GameObject mapPanel;
    private bool isShowMap;

    [Header("Inspector")]
    public GameObject gameplayCanvas;
    public GameObject inspector;
    public GameObject itemObject;
    public float speed;
    public quaternion initRotation;
    public TextMeshProUGUI clueText;
    private bool isInspector;
    public bool hasClue;

    private void Awake()
    {
        PlayerFps = FindObjectOfType<PlayerFps>().transform;
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameState.Gameplay:
                ShowMap();
                break;
            case GameState.Item:
                CheckItem();
                break;
        }
    }

    public void CheckItem()
    {
        if (Input.GetKey(KeyCode.I))
        {
            OnInspector();
            Destroy(itemObject);
        }

        if (Input.GetKeyDown(KeyCode.E) && hasClue)
        {
            // metodo para criar uma pista no item verificado
            print("Li a mensagem");
        }

        if (Input.GetMouseButton(0))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            itemObject.transform.Rotate(Vector3.up, -x * speed);
            itemObject.transform.Rotate(Vector3.right, y * speed);
        }

        if (Input.GetKey(KeyCode.O))
        {
            itemObject.transform.rotation = initRotation;
        }
    }

    public void ShowMap()
    {
        if (Input.GetKeyUp(KeyCode.M))
        {
            isShowMap = !isShowMap;

            switch (isShowMap)
            {
                case true:
                    Time.timeScale = 0;
                    mapCam.SetActive(true);
                    mapPanel.SetActive(true);
                    break;
                case false:
                    Time.timeScale = 1;
                    mapCam.SetActive(false);
                    mapPanel.SetActive(false);
                    break;
            }
        }
    }

    public void OnInspector()
    {
        isInspector = !isInspector;
        gameplayCanvas.SetActive(!isInspector);
        inspector.SetActive(isInspector);

        switch (isInspector)
        {
            case true:
                if (itemObject != null)
                {
                    initRotation = itemObject.transform.rotation;
                }

                currentState = GameState.Item;
                break;
            case false:
                currentState = GameState.Gameplay;
                break;
        }
    }

    public bool NonGameplay()
    {
        return currentState != GameState.Gameplay;
    }
}
