using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MasterInteraction : MonoBehaviour
{
    private GameManager gameManager;

    public Transform target;
    public GameObject canvas;
    public Image icon;
    public TextMeshProUGUI txtInteraction;

    public bool isBehind;

    public bool isShowDistance;
    public bool IsShowBehind;

    public InteractableItem objeto;

    private float distance;

    public bool onInteraction;

    // minhas mudanças
    private Transform player;
    private float rangeInteraction;
    private TheOtherObjectIsBehind testBehind;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = gameManager.PlayerFps;
        rangeInteraction = gameManager.distanceInteraction;

        testBehind = GetComponent<TheOtherObjectIsBehind>();

        canvas.SetActive(false);
        SetMessage("");
        icon.sprite = gameManager.icons[0];
    }

    private void Update()
    {
        distance = Vector3.Distance(target.position, player.position);

        if (!isShowDistance && distance <= rangeInteraction)
        {
            canvas.SetActive(true);
            UpdateCanvas();
        }
        else if (!isShowDistance && distance > rangeInteraction)
        {
            canvas.SetActive(false);
        }
        else if (isShowDistance)
        {
            canvas.SetActive(true);
            UpdateCanvas();
        }
    }

    private void UpdateCanvas()
    {
        isBehind = testBehind.GetIsBehind(target, player);
        Vector2 iconPos = Camera.main.WorldToScreenPoint(target.position);

        if (isShowDistance && distance > rangeInteraction)
        {
            SetMessage(distance.ToString("N0") + "m");
        }

        if (IsShowBehind)
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
                txtInteraction.enabled = false;
                icon.enabled = false;
            }
            else
            {
                txtInteraction.enabled = true;

                if (!onInteraction)
                {
                    icon.enabled = true;
                }
                
            }
        }

        icon.transform.position = iconPos;
    }


    private void StartInteraction()
    {
        SetMessage(objeto.msgInteraction);
        icon.sprite = gameManager.icons[1];
    }

    private void OnInteraction()
    {
        icon.enabled = false;
        SetMessage(objeto.msgOnInteraction);
        onInteraction = true;
    }

    private void EndInteraction()
    {
        SetMessage("");
        icon.sprite = gameManager.icons[0];
        icon.enabled = true;
        onInteraction = false;
    }

    private void SetMessage(string message)
    {
        txtInteraction.text = message;
    }
}
