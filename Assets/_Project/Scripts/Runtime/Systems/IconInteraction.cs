using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class IconInteraction : MonoBehaviour
{
    public Image icon;
    public GameObject canvas;
    public TextMeshProUGUI txtDistance;

    public bool isShowDistance;
    public bool isShowBehind;

    private bool isBehind;

    private GameManager gameManager;

    private Transform player;
    private float dist;



    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = gameManager.PlayerFps;

        dist = gameManager.distanceInteraction;
        canvas.SetActive(false);
        txtDistance.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isShowDistance)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= dist)
            {
                canvas.SetActive(true);
                UpdateCanvas();
            }
            else
            {
                canvas.SetActive(false);
            }
        }
        else
        {
            canvas.SetActive(true);
            UpdateCanvas();
        }



    }

    private void UpdateCanvas()
    {
        isBehind = Vector3.Dot(transform.position - player.position, player.forward) < 0;
        Vector2 iconPos = Camera.main.WorldToScreenPoint(transform.position);

        if (isShowDistance)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > 2)
            {
                txtDistance.gameObject.SetActive(true);
                txtDistance.text = distance.ToString("N0") + "m";
            }
            else
            {
                txtDistance.gameObject.SetActive(false);
            }
        }
        else
        {
            txtDistance.gameObject.SetActive(false);
        }


        if (isShowBehind)
        {
            float minX = icon.GetPixelAdjustedRect().width / 2;
            float maxX = Screen.width - minX;
            float minY = icon.GetPixelAdjustedRect().height / 2;
            float maxY = Screen.height - minY;

            iconPos.x = Mathf.Clamp(iconPos.x, minX, maxX);
            iconPos.y = Mathf.Clamp(iconPos.y, minY, maxY);

            if (isBehind)
            {
                if (iconPos.x < Screen.width / 2)
                {
                    iconPos.x = maxX;
                }
                else
                {
                    iconPos.x = minX;
                }
            }
        }
        else
        {
            if (isBehind)
            {
                txtDistance.enabled = false;
                icon.enabled = false;
            }
            else
            {
                txtDistance.enabled = true;
                icon.enabled = true;
            }
        }

        icon.transform.position = iconPos;


    }
}
