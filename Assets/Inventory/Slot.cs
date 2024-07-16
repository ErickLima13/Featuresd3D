using UnityEngine;

public class Slot : MonoBehaviour
{
    public Inventory inventory;
    public int idSlot;

    public void OnClick()
    {
        inventory.OnSlotClick(idSlot);
    }

}
