using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Panel Description")]
    public GameObject panelDescription;
    public Image imageItem;
    public TextMeshProUGUI nameItem;
    public TextMeshProUGUI descriptionItem;

    [Header("Slots")]
    public Image[] slotImg;
    public List<Item> itemSlot = new();

    [Header("Buttons")]
    public Button btnUse;
    public Button btnCheck;
    public Button btnDrop;

    [SerializeField] private Item itemSelect;

    [Header("Dialog")]
    public GameObject panelDialog;
    public Image imgItemDialog;


    private void Start()
    {
        btnUse.onClick.AddListener(() => OnBtnClick(0));
        btnCheck.onClick.AddListener(() => OnBtnClick(1));
        btnDrop.onClick.AddListener(() => OnBtnClick(2));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            UpdateInventory();
        }
    }

    public void UpdateInventory()
    {
        btnUse.interactable = false;
        btnCheck.interactable = false;
        btnDrop.interactable = false;

        imageItem.sprite = null;
        nameItem.text = null;
        descriptionItem.text = null;

        panelDialog.SetActive(false);
        imgItemDialog.sprite = null;

        for (int i = 0; i < slotImg.Length; i++)
        {
            if (i < itemSlot.Count)
            {
                slotImg[i].sprite = itemSlot[i].imagem;
            }
            else
            {
                slotImg[i].sprite = null;
            }
        }
    }

    public void OnSlotClick(int idSlot)
    {
        //if (itemSlot[idSlot] == null) // esse if ta errado
        //{
        //    return;
        //}

        if (idSlot >= itemSlot.Count) // minha ideia funcionou!
        {
            return;
        }


        imageItem.sprite = itemSlot[idSlot].imagem;
        nameItem.text = itemSlot[idSlot].name;
        descriptionItem.text = itemSlot[idSlot].description;

        itemSelect = itemSlot[idSlot];

        CheckItem(itemSlot[idSlot].use, btnUse);
        CheckItem(itemSlot[idSlot].check, btnCheck);
        CheckItem(itemSlot[idSlot].drop, btnDrop);
    }

    public void OnBtnClick(int idButton)
    {
        switch (idButton)
        {
            case 0:

                break;
            case 1:

                break;
            case 2:



                imgItemDialog.sprite = itemSelect.imagem;
                panelDialog.SetActive(true);


                break;
        }
    }

    public void ButtonDialog(int idButton)
    {
        switch (idButton)
        {
            case 0:
                panelDialog.SetActive(false);
                break;
            case 1:
                panelDialog.SetActive(false);
                itemSlot.Remove(itemSelect);
                itemSelect = null;
                UpdateInventory();
                break;
        }
    }

    //Minhas ideias
    private void CheckItem(bool value, Button btn)
    {
        if (value)
        {
            btn.interactable = true;
        }
        else
        {
            btn.interactable = false;
        }
    }
}


