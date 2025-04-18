using UnityEngine;

[CreateAssetMenu(fileName = "New Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    [TextArea]public string itemDescription;
    public Sprite icon;
    public ItemType itemType;

    public bool isGold;
    public int stackSize = 1;    

    [Header("Stats")]
    public int currentHealth;
    public int maxHealth;
    public int speed;
    public int damage;

    [Header("For Temporary Itmes")]
    public float duration;


    public void OnValidate()
    {
        if (itemType == ItemType.Helmet ||
                itemType == ItemType.Chest ||
                itemType == ItemType.Gloves ||
                itemType == ItemType.Necklace ||
                itemType == ItemType.Sword ||
                itemType == ItemType.Pants ||
                itemType == ItemType.Shield ||
                itemType == ItemType.Boots)
        {
            stackSize = 1;
        }
    }
}

public enum ItemType
{
    Consumable,
    Helmet,
    Chest,
    Gloves,
    Necklace,
    Sword,
    Pants,
    Shield,
    Boots
}
