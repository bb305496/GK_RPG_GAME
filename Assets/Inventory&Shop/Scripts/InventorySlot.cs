using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public ItemSO itmeSO;
    public int quantity;

    public Image itemImage;
    public TMP_Text quantityText;

    public void UpdateUI()
    {
        if(itmeSO != null)
        {
            itemImage.sprite = itmeSO.icon;
            itemImage.gameObject.SetActive(true);
            quantityText.text = quantity.ToString();
        }
        else
        {
            itemImage.gameObject.SetActive(false);
            quantityText.text = "";
        }

    }
}
