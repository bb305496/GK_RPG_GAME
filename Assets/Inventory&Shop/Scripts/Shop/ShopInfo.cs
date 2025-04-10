using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class ShopInfo : MonoBehaviour
{
    public CanvasGroup infoPanel;

    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;

    [Header("Stats")]
    public TMP_Text[] statTexts;

    private RectTransform infoPanelRect;

    private void Awake()
    {
        infoPanelRect = GetComponent<RectTransform>();
    }

    public void ShowItemInfo(ItemSO itemSO)
    {
        infoPanel.alpha = 1;
        itemNameText.text = itemSO.itemName;
        itemDescriptionText.text = itemSO.itemDescription;

        List<string> stats = new List<string>();
        if (itemSO.maxHealth > 0) stats.Add("Max HP: " + itemSO.maxHealth);
        if (itemSO.currentHealth > 0) stats.Add("Healing: " + itemSO.currentHealth);
        if (itemSO.damage > 0) stats.Add("DMG: " + itemSO.damage);  
        if (itemSO.speed > 0) stats.Add("SPD: " + itemSO.speed);
        if (itemSO.duration > 0) stats.Add("Duration: " + itemSO.duration);

        if (stats.Count <= 0)
            return;

        for (int i = 0; i < statTexts.Length; i++)
        {
            if (i < stats.Count)
            {
                statTexts[i].text = stats[i];
                statTexts[i].gameObject.SetActive(true);
            }
            else
            {
                statTexts[i].gameObject.SetActive(false);
            }
        }
    }

    public void HideItemInfo()
    {
        infoPanel.alpha = 0;
        itemNameText.text = "";
        itemDescriptionText.text = "";
    }

    public void FollowMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 offset = new Vector3(20, -20, 0);

        infoPanelRect.position = mousePos + offset;
    }    
}
