using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    public TMP_Text healthText;

    [Header("Combat Stats")]
    public int damage;
    public float weaponRange;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;

    [Header("Movement Stats")]
    public float speed;

    [Header("Health Stats")]
    public int maxHealth;
    public int currentHealth;
    private bool hasPassiveHealing = false;
    public float healingCooldown = 2f;
    private float healingTimer;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        healingTimer = healingCooldown;
    }

    private void Update()
    {
        if(hasPassiveHealing)
        {
            PassiveHealing(1);
        }
    }
    public void UpdateMaxHealth(int amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth;
        healthText.text = currentHealth + "/" + maxHealth;
    }

    public void UpdateMaxDmg(int amount)
    {
        damage += amount;
    }


    public void EnablePassiveHealing()
    {
        hasPassiveHealing = true;
    }

    public void PassiveHealing(int amount)
    {
        healingTimer -= Time.deltaTime;
        if (healingTimer <= 0)
        {
            currentHealth += amount;
            if(currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            healingTimer = healingCooldown;
        }

    }

}
