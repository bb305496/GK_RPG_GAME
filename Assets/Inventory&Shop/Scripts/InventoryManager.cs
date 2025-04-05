using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] itemSlots;
    public InventorySlot helmetSlot;
    public InventorySlot chestSlot;
    public UseItem useItem;
    public int gold;
    public TMP_Text goldText;
    public GameObject lootPrefab;
    public Transform player;

    private void Start()
    { 
        foreach(var slot in itemSlots)
        {
            slot.UpdateUI();
        }

        UpdateEquipUI();

    }

    private void UpdateEquipUI()
    {
        helmetSlot.UpdateUI();
        chestSlot.UpdateUI();
    }

    private void OnEnable()
    {
        Loot.OnItemLooted += AddItem;
    }

    private void OnDisable()
    {
        Loot.OnItemLooted -= AddItem;
    }

    public void AddItem(ItemSO itemSO, int quantity)
    {
        if(itemSO.isGold)
        {
            gold += quantity;
            goldText.text = gold.ToString();
            return;
        }

        foreach (var slot in itemSlots)
        {
            if(slot.itmeSO == itemSO && slot.quantity < itemSO.stackSize)
            {
                int availableSpace = itemSO.stackSize - slot.quantity;
                int amountToAdd = Mathf.Min(availableSpace, quantity);

                slot.quantity += amountToAdd;
                quantity -= amountToAdd;

                slot.UpdateUI();

                if(quantity <= 0)
                    return;
            }
        }

        foreach(var slot in itemSlots)
        {
            if(slot.itmeSO == null)
            {
                int amountToAdd = Mathf.Min(itemSO.stackSize, quantity);
                slot.itmeSO = itemSO;
                slot.quantity = quantity;
                slot.UpdateUI();
                return;
            }
        }

        if(quantity > 0)
        {
            DropLoot(itemSO, quantity);
        }
        
    }

    public void DropItem(InventorySlot slot)
    {
        DropLoot(slot.itmeSO, 1);

        if (slot == helmetSlot)
        {
            StatsManager.Instance.RemoveEquipmentStats(slot.itmeSO);
        }
        if (slot == chestSlot)
        {
            StatsManager.Instance.RemoveEquipmentStats(slot.itmeSO);
        }

        slot.quantity--;
        if (slot.quantity <= 0)
        {
            slot.itmeSO = null;
        }
        slot.UpdateUI();
    }

    private void DropLoot(ItemSO itemSO, int quantity)
    {
       Loot loot = Instantiate(lootPrefab, player.position, Quaternion.identity).GetComponent<Loot>();
       loot.Initialize(itemSO, quantity);
    }

    public void UseItem(InventorySlot slot)
    {
        if(slot.itmeSO != null && slot.quantity >=0)
        {
            if (slot.itmeSO.itemType == ItemType.Helmet ||
                slot.itmeSO.itemType == ItemType.Chest)
            {
                EquipItem(slot);
                return;
            }


            useItem.ApplyItemEffect(slot.itmeSO);

            slot.quantity--;
            if(slot.quantity <=0)
            {
                slot.itmeSO = null;
            }
            slot.UpdateUI();
        }
    }

    // Equpiment methods

    private void EquipItem(InventorySlot slot)
    {
        InventorySlot equipSlot = GetEquipmentSlotForItem(slot.itmeSO);
        if (equipSlot == null) return;

        if (equipSlot.itmeSO == slot.itmeSO)
        {
            StatsManager.Instance.RemoveEquipmentStats(equipSlot.itmeSO);
            AddItem(equipSlot.itmeSO, 1);
            equipSlot.itmeSO = null;
            equipSlot.quantity = 0;
            equipSlot.UpdateUI();
            return;
        }

        if (equipSlot.itmeSO != null)
        {
            StatsManager.Instance.RemoveEquipmentStats(equipSlot.itmeSO);
            AddItem(equipSlot.itmeSO, 1);
        }

        equipSlot.itmeSO = slot.itmeSO;
        equipSlot.quantity = 1;
        equipSlot.UpdateUI();
        StatsManager.Instance.ApplyEquipmentStats(equipSlot.itmeSO);

        slot.quantity--;
        if (slot.quantity <= 0)
        {
            slot.itmeSO = null;
        }
        slot.UpdateUI();
    }

    private InventorySlot GetEquipmentSlotForItem(ItemSO item)
    {
        switch (item.itemType)
        {
            case ItemType.Helmet:
                return helmetSlot;
            case ItemType.Chest:
                return chestSlot;
            default:
                return null;
        }
    }
}
