using UnityEngine;


[CreateAssetMenu(fileName = "Objeto interativo", menuName = "Novo Objeto Interativo", order = 0)]
public class InteractableItem : ScriptableObject
{
    public string msgInteraction;
    public string msgOnInteraction;

    public bool hasItem;

    public int percItem; // chance percentual de encontrar o item

    public GameObject item;

    public bool DropItem()
    {
        bool dropped = false;

        if (Random.Range(0, 100) <= percItem)
        {
            dropped = true;
        }

        return dropped;
    }



}
