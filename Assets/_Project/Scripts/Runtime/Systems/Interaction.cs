using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{

    private Transform mainCamera;

    private Ray ray;
    private RaycastHit hitInfo;

    [SerializeField] private GameObject objInteraction;

    public float rayRange;

    public LayerMask interactionLayer;

    public TextMeshProUGUI messageInteraction;

    public Image crossHair;

    private bool isInteraction;

    private void Start()
    {
        mainCamera = Camera.main.transform;

    }

    private void Update()
    {
        ray.origin = mainCamera.position;
        ray.direction = mainCamera.forward;

        if (Physics.Raycast(ray, out hitInfo, rayRange, interactionLayer))
        {
            if (!isInteraction)
            {
                objInteraction = hitInfo.collider.gameObject;
                crossHair.color = Color.yellow;
                objInteraction.SendMessage("StartInteraction", SendMessageOptions.DontRequireReceiver);
                isInteraction = true;
            }
        }
        else
        {
            objInteraction = null;
            crossHair.color = Color.white;
            SetMessage("");
            isInteraction = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && objInteraction != null)
        {
            objInteraction.SendMessage("Interaction", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void SetMessage(string message)
    {
        messageInteraction.text = message;
    }

}

