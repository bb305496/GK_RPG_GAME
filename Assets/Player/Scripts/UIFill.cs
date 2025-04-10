using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIFill : MonoBehaviour
{
    public Image fill;
    private PlayerHealth playerHealth;

    private int currentValue;

    private void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        UpdateFill();
    }

    public void UpdateFill()
    {
        if (playerHealth != null)
        {
            fill.fillAmount = (float)StatsManager.Instance.currentHealth / StatsManager.Instance.maxHealth;
        }
    }

}
