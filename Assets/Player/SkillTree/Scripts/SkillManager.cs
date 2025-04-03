using NUnit.Framework.Constraints;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilitPointSpent; ;
    }

    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilitPointSpent; ;
    }

    private void HandleAbilitPointSpent(SkillSlot slot)
    {
        string skillName = slot.skillSO.skillName;

        switch(skillName)
        {
            case "Add Max Health":
                StatsManager.Instance.UpdateMaxHealth(1);
                break;

            case "Add Max Dmg":
                StatsManager.Instance.UpdateMaxDmg(1);
                break;

            default:
                Debug.LogWarning("Skill not found" + skillName);
                break;
        }
    }
}
