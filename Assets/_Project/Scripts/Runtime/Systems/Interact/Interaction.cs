using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    private Transform mainCamera;

    private GameManager gameManager;

    private Ray ray;
    private RaycastHit hitInfo;

    [SerializeField] private GameObject objInteraction;

    public float rayRange;

    public LayerMask interactionLayer;
    public Image crossHair;

    private bool isInteraction;

    private void Start()
    {
        mainCamera = Camera.main.transform;
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gameManager.NonGameplay())
        {
            return;
        }

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
            if (objInteraction != null)
            {
                objInteraction.SendMessage("EndInteraction", SendMessageOptions.DontRequireReceiver);
                objInteraction = null;
                crossHair.color = Color.white;
                isInteraction = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && objInteraction != null)
        {
            objInteraction.SendMessage("OnInteraction", SendMessageOptions.DontRequireReceiver);

            // onde vai chamar o metodo da interface.
        }
    }



}

