using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
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
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        OnEnemyDestroyed.Invoke();
    }
}
