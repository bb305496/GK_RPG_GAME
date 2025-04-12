using System.Collections.Generic;
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
    private Canvas parentCanvas;
    private Vector2 panelSize;
    private Vector2 offset = new Vector2(20, -20); 

    private void Awake()
    {
        infoPanelRect = infoPanel.GetComponent<RectTransform>();
        parentCanvas = GetComponentInParent<Canvas>();

        panelSize = infoPanelRect.sizeDelta;
        if (parentCanvas != null)
        {
            panelSize.x *= parentCanvas.transform.localScale.x;
            panelSize.y *= parentCanvas.transform.localScale.y;
        }
    }

    public void ShowItemInfo(ItemSO itemSO)
    {
        infoPanel.alpha = 1;
        itemNameText.text = itemSO.itemName;
        itemDescriptionText.text = itemSO.itemDescription;

        List<string> stats = new List<string>();
        if (itemSO.maxHealth != 0) stats.Add("Max HP: " + itemSO.maxHealth);
        if (itemSO.currentHealth != 0) stats.Add("Healing: " + itemSO.currentHealth);
        if (itemSO.damage != 0) stats.Add("DMG: " + itemSO.damage);
        if (itemSO.speed != 0) stats.Add("SPD: " + itemSO.speed);
        if (itemSO.duration != 0) stats.Add("Duration: " + itemSO.duration);

        for (int i = 0; i < statTexts.Length; i++)
        {
            statTexts[i].gameObject.SetActive(i < stats.Count);
            if (i < stats.Count) statTexts[i].text = stats[i];
        }

        FollowMouse();
    }

    public void HideItemInfo()
    {
        infoPanel.alpha = 0;
        itemNameText.text = "";
        itemDescriptionText.text = "";
    }

    public void FollowMouse()
    {
        if (parentCanvas == null || infoPanel.alpha == 0) return;

        Vector2 mousePosition = Input.mousePosition;
        Vector2 adjustedPosition = mousePosition;
        Vector2 finalOffset = offset;

        if (mousePosition.x + panelSize.x + offset.x > Screen.width)
        {
            finalOffset.x = -panelSize.x - offset.x;
        }

        if (mousePosition.y + offset.y - panelSize.y < 0)
        {
            finalOffset.y = Mathf.Abs(offset.y) + panelSize.y;
        }

        if (mousePosition.y + finalOffset.y + panelSize.y > Screen.height)
        {
            finalOffset.y = -Mathf.Abs(offset.y) - panelSize.y;
        }

        infoPanelRect.position = mousePosition + finalOffset;
    }

    private void Update()
    {
        if (infoPanel.alpha > 0)
        {
            FollowMouse();
        }
    }
}