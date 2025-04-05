using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public ItemSO itmeSO;
    public int quantity;

    public Image itemImage;
    public TMP_Text quantityText;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GetComponentInParent<InventoryManager>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(quantity > 0)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                inventoryManager.UseItem(this);
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                inventoryManager.DropItem(this);
            }
        }

    }

    public void UpdateUI()
    {
        if(itmeSO != null)
        {
            itemImage.sprite = itmeSO.icon;
            itemImage.gameObject.SetActive(true);

            if (itmeSO.itemType == ItemType.Helmet || 
                itmeSO.itemType == ItemType.Chest ||
                itmeSO.itemType == ItemType.Gloves ||
                itmeSO.itemType == ItemType.Necklace ||
                itmeSO.itemType == ItemType.Sword ||
                itmeSO.itemType == ItemType.Pants ||
                itmeSO.itemType == ItemType.Shield ||
                itmeSO.itemType == ItemType.Boots)
            {
                quantityText.text = "";
            }
            else
            {
                quantityText.text = quantity.ToString();
            }
        }
        else
        {
            itemImage.gameObject.SetActive(false);
            quantityText.text = "";
        }

    }
}
