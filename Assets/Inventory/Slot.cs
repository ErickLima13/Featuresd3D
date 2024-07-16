using UnityEngine;

public class Slot : MonoBehaviour
{
    public int idSlot;

    public void OnClick()
    {
        Inventory.Instance.OnSlotClick(idSlot);
    }

}
