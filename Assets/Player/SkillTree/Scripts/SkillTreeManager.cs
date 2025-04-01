using TMPro;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    public SkillSlot[] skillSlots;
    public TMP_Text skillPointsText;
    public int aviailableSkillPoints;


    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointsSpent; 
        SkillSlot.OnSkillMaxed += HandleSkillMaxed;
        ExpManager.OnLevelUp += UpdateSkillPoints;
    }

    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointsSpent; 
        SkillSlot.OnSkillMaxed -= HandleSkillMaxed;
        ExpManager.OnLevelUp -= UpdateSkillPoints;
    }
    private void Start()
    {
        foreach (SkillSlot slot in skillSlots)
        {
            slot.skillButton.onClick.AddListener(() => CheckAvailabePoints(slot));
        }
        UpdateSkillPoints(0);
    }

    private void CheckAvailabePoints(SkillSlot slot)
    {
        if (aviailableSkillPoints > 0)
        {
            slot.TryUpgradeSkill();
        }
    }

    private void HandleSkillMaxed(SkillSlot skillSlot)
    {
        foreach (SkillSlot slot in skillSlots)
        {
            if (!slot.isUnlocked && slot.CanUnlockSkill())
            {
                slot.Unlock();
            }
        }
    }

    private void HandleAbilityPointsSpent(SkillSlot slot)
    {
        if (aviailableSkillPoints > 0)
        {
            UpdateSkillPoints(-1);
        }
    }

    public void UpdateSkillPoints(int amount)
    {
        aviailableSkillPoints += amount;
        skillPointsText.text = "Skill Points: " + aviailableSkillPoints;
    }
}
