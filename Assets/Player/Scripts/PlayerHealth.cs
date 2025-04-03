using UnityEngine;
using TMPro;
using System.Diagnostics;
public class PlayerHealth : MonoBehaviour
{

    public TMP_Text healthText;
    public Animator healthTextAnim;
    public Animator healthFillAnim;
    //public Animator healthOutlineAnim;
    public StatsUI stastUI;

    private ChasingWarningUI chasingWarning;


    private UIFill uiFill;

    private void Start()
    {
        StatsManager.Instance.currentHealth = StatsManager.Instance.maxHealth;
        uiFill = FindFirstObjectByType<UIFill>();

        chasingWarning = FindFirstObjectByType<ChasingWarningUI>();

        healthText.text = StatsManager.Instance.currentHealth + "/" + StatsManager.Instance.maxHealth;
    }

    public void Update()
    {
         UpdateUI();
    }

    public void ChangeHealth(int amount)
    {
        // Stop heal while chasing
        if (amount > 0 && chasingWarning != null && chasingWarning.IsBeingChased())
        {
            return;
        }

        StatsManager.Instance.currentHealth += amount;
        healthTextAnim.Play("HealthText");
        healthFillAnim.Play("FillHealth");
        //healthOutlineAnim.Play("OutlineHealth");
        UpdateUI();

        if (StatsManager.Instance.currentHealth > StatsManager.Instance.maxHealth)
        {
            StatsManager.Instance.currentHealth = StatsManager.Instance.maxHealth;
        }

        if (StatsManager.Instance.currentHealth <= 0)
        {
            StatsManager.Instance.currentHealth = 0;
            gameObject.SetActive(false);
        }
    }

    private void UpdateUI()
    {    
        uiFill.UpdateFill();
        healthText.text = StatsManager.Instance.currentHealth + "/" + StatsManager.Instance.maxHealth;
    }
}
