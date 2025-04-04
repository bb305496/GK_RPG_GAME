using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class StatsManager : MonoBehaviour
{
    private ChasingWarningUI chasingWarning;
    public static StatsManager Instance;
    public StatsUI statsUI;
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
    private int passiveHealingLevel = 0;
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
        chasingWarning = FindFirstObjectByType<ChasingWarningUI>();
    }

    private void Update()
    {
        if(hasPassiveHealing)
        {
            PassiveHealing();
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


    public void UpgradePassiveHealing()
    {
        hasPassiveHealing = true;
        passiveHealingLevel++;
    }

    public void PassiveHealing()
    {
        // Jeœli gracz jest œcigany – nie lecz!
        if (chasingWarning != null && chasingWarning.IsBeingChased())
        {
            return;
        }
        healingTimer -= Time.deltaTime;
        if (healingTimer <= 0)
        {
            int healingAmount = passiveHealingLevel;
            currentHealth += healingAmount;
            if(currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            healingTimer = healingCooldown;
        }

    }

    //#######################################ITEMS########################################

    public void UpdateSpeed(int amount)
    {
        speed += amount;
        statsUI.UpdateAllStats();
    }

}
