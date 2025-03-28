using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
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
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        OnEnemyDestroyed.Invoke();
    }
}
