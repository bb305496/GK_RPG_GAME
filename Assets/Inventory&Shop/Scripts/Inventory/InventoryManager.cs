using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public InventorySlot[] itemSlots;
    public InventorySlot helmetSlot;
    public InventorySlot chestSlot;
    public InventorySlot glovesSlot;
    public InventorySlot necklaceSlot;
    public InventorySlot swordSlot;
    public InventorySlot pantsSlot;
    public InventorySlot shieldSlot;
    public InventorySlot bootsSlot;
    public UseItem useItem;
    public int gold;
    public TMP_Text goldText;
    public GameObject lootPrefab;
    public Transform player;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        glovesSlot.UpdateUI();
        necklaceSlot.UpdateUI();
        swordSlot.UpdateUI();
        pantsSlot.UpdateUI();
        shieldSlot.UpdateUI();
        bootsSlot.UpdateUI();
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
            if(slot.itemSO == itemSO && slot.quantity < itemSO.stackSize)
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
            if(slot.itemSO == null)
            {
                int amountToAdd = Mathf.Min(itemSO.stackSize, quantity);
                slot.itemSO = itemSO;
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
        DropLoot(slot.itemSO, 1);

        if (slot == helmetSlot ||
            slot == chestSlot ||
            slot == glovesSlot ||
            slot == necklaceSlot ||
            slot == swordSlot ||
            slot == pantsSlot ||
            slot == shieldSlot ||
            slot == bootsSlot)
        {
            StatsManager.Instance.RemoveEquipmentStats(slot.itemSO);
        }


        slot.quantity--;
        if (slot.quantity <= 0)
        {
            slot.itemSO = null;
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
        if(slot.itemSO != null && slot.quantity >=0)
        {
            if (slot.itemSO.itemType == ItemType.Helmet ||
                slot.itemSO.itemType == ItemType.Chest ||
                slot.itemSO.itemType == ItemType.Gloves ||
                slot.itemSO.itemType == ItemType.Necklace ||
                slot.itemSO.itemType == ItemType.Sword ||
                slot.itemSO.itemType == ItemType.Pants ||
                slot.itemSO.itemType == ItemType.Shield ||
                slot.itemSO.itemType == ItemType.Boots)
            {
                EquipItem(slot);
                return;
            }


            useItem.ApplyItemEffect(slot.itemSO);

            slot.quantity--;
            if(slot.quantity <=0)
            {
                slot.itemSO = null;
            }
            slot.UpdateUI();
        }
    }

    // Equpiment methods

    private void EquipItem(InventorySlot slot)
    {
        InventorySlot equipSlot = GetEquipmentSlotForItem(slot.itemSO);
        if (equipSlot == null) return;

        if (equipSlot.itemSO == slot.itemSO)
        {
            StatsManager.Instance.RemoveEquipmentStats(equipSlot.itemSO);
            AddItem(equipSlot.itemSO, 1);
            equipSlot.itemSO = null;
            equipSlot.quantity = 0;
            equipSlot.UpdateUI();
            return;
        }

        if (equipSlot.itemSO != null)
        {
            StatsManager.Instance.RemoveEquipmentStats(equipSlot.itemSO);
            AddItem(equipSlot.itemSO, 1);
        }

        equipSlot.itemSO = slot.itemSO;
        equipSlot.quantity = 1;
        equipSlot.UpdateUI();
        StatsManager.Instance.ApplyEquipmentStats(equipSlot.itemSO);

        slot.quantity--;
        if (slot.quantity <= 0)
        {
            slot.itemSO = null;
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
            case ItemType.Gloves:
                return glovesSlot;
            case ItemType.Necklace:
                return necklaceSlot;
            case ItemType.Sword:
                return swordSlot;
            case ItemType.Pants:
                return pantsSlot;
            case ItemType.Shield:
                return shieldSlot;
            case ItemType.Boots:
                return bootsSlot;
            default:
                return null;
        }
    }

    public bool HasItem(ItemSO itmeSo)
    {
        foreach (var slot in itemSlots)
        {
            if (slot.itemSO == itmeSo && slot.quantity > 0)
            {
                return true;
            }
        }
        return false;
    }
}
