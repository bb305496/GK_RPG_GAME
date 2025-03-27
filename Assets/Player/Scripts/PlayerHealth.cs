using UnityEngine;
using TMPro;
public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public TMP_Text healthText;
    public Animator healthTextAnim;
    public Animator healthFillAnim;
    public Animator healthOutlineAnim;

    private UIFill uiFill;

    private void Start()
    {
        currentHealth = maxHealth;
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
        healthTextAnim.Play("HealthText");
        healthFillAnim.Play("FillHealth");
        healthOutlineAnim.Play("OutlineHealth");
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
