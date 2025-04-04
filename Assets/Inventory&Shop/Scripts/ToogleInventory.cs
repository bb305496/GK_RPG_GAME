using UnityEngine;

public class ToogleInventory : MonoBehaviour
{
    public CanvasGroup inventoryCanvas;
    private bool isInventoryOpen = false;

    private void Update()
    {
        if (Input.GetButtonDown("ToggleInventory"))
        {
            if (isInventoryOpen)
            {
                inventoryCanvas.alpha = 0;
                inventoryCanvas.blocksRaycasts = false;
                isInventoryOpen = false;
            }
            else
            {
                inventoryCanvas.alpha = 1;
                inventoryCanvas.blocksRaycasts = true;
                isInventoryOpen = true;
            }
        }
    }
}
