using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ExpManager : MonoBehaviour
{
    public int currentExp;
    public int expToLevel = 30;
    public Slider expSlider;
    public TMP_Text levelText;
    public StatsManager statsManager;
    public StatsUI statsUI;

    public static event Action<int> OnLevelUp;

    private void Start()
    {
        UpdateUI();
    }

    private void OnEnable()
    {
        EnemyHealth.OnMonsterDefeated += GainExperience;
    }

    private void OnDisable()
    {
        EnemyHealth.OnMonsterDefeated -= GainExperience;
    }

    public void GainExperience(int amount)
    {
        currentExp += amount;
        if (currentExp >= expToLevel)
        {
            LevelUp();
        }

        UpdateUI();
    }

    private void LevelUp()
    {
        StatsManager.Instance.level++;
        currentExp -= expToLevel;
        expToLevel = Mathf.RoundToInt(expToLevel * 1.5f);
        OnLevelUp?.Invoke(1);
        statsManager.damage += 1;
        statsManager.maxHealth += 1;
        statsManager.currentHealth += 1;
        statsUI.UpdateAllStats();
    }

    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;
        levelText.text = "Level " + StatsManager.Instance.level; ;
    }
}
