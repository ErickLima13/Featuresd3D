using UnityEngine;

public class ObjectsOfInteraction : MonoBehaviour
{
    public InteractableItem objeto;

    private void Start()
    {
      //  interactionScript = FindObjectOfType<Interaction>();
    }

    public virtual void Interaction()
    {
        //interactionScript.SetMessage(objeto.msgOnInteraction);
    }

    public void StartInteraction()
    {
        // interactionScript.SetMessage(objeto.msgInteraction);
    }
}
