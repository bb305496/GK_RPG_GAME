using UnityEngine;
using TMPro;
public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public TMP_Text healthText;

    private UIFill uiFill;

    private void Start()
    {
        uiFill = FindFirstObjectByType<UIFill>();
        healthText.text = currentHealth + "/" + maxHealth;
    }

    public void Update()
    {
         UpdateUI();
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        UpdateUI();

        if (currentHealth > maxHealth)
        {   
            currentHealth = maxHealth;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            gameObject.SetActive(false);
        }
    }

    private void UpdateUI()
    {    
        uiFill.UpdateFill();
        healthText.text = currentHealth + "/" + maxHealth;
    }
}
