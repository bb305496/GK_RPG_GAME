using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Loot")]
    public LootDrop[] possibleLoot;
    public GameObject lootPrefab; 

    public int expReward = 3;

    public delegate void MonsterDefeated(int exp);
    public static event MonsterDefeated OnMonsterDefeated;

    public int currentHealth;
    public int maxHealth;
    public EnemyHealtbarBehaviour healthbar;

    public event Action OnEnemyDestroyed;

    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetHealth(currentHealth, maxHealth);
    }

    public void ChangeHealth(int ammount)
    {
        currentHealth += ammount;
        healthbar.SetHealth(currentHealth, maxHealth);

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if(currentHealth <= 0)
        {
            OnMonsterDefeated(expReward);
            DropLoot();
            Destroy(gameObject);
        }
    }
    private void DropLoot()
    {
        if (possibleLoot == null || possibleLoot.Length == 0)
            return;

        foreach (var loot in possibleLoot)
        {
            if (UnityEngine.Random.value <= loot.dropChance)
            {
                int quantity = UnityEngine.Random.Range(loot.minQuantity, loot.maxQuantity + 1);

                var lootInstance = Instantiate(lootPrefab, transform.position, Quaternion.identity);
                var lootComponent = lootInstance.GetComponent<Loot>();

                lootComponent.Initialize(loot.item, quantity);
            }
        }
    }

    private void OnDestroy()
    {
        OnEnemyDestroyed.Invoke();
    }
}

[System.Serializable]
public class LootDrop
{
    public ItemSO item;
    public int minQuantity = 1;
    public int maxQuantity = 1;
    [Range(0f, 1f)] public float dropChance = 1f;
}