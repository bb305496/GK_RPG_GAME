using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpManager : MonoBehaviour
{
    public int level;
    public int currentExp;
    public int expToLevel = 10;
    public Slider expSlider;
    public TMP_Text levelText;


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
        currentExp = 0;
        expToLevel = Mathf.RoundToInt(expToLevel * 1.2f);
    }

    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;
        levelText.text = "Level " + level; ;
    }
}
