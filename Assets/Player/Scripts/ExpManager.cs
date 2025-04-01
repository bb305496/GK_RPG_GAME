using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ExpManager : MonoBehaviour
{
    public int level;
    public int currentExp;
    public int expToLevel = 10;
    public Slider expSlider;
    public TMP_Text levelText;

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
        level++;
        currentExp -= expToLevel;
        expToLevel = Mathf.RoundToInt(expToLevel * 1.2f);
        OnLevelUp?.Invoke(1);
    }

    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;
        levelText.text = "Level " + level; ;
    }
}
