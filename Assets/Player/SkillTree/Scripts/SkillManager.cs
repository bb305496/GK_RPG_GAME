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
                StatsManager.Instance.UpdateMaxHealth(10);
                break;

            case "Add Max Dmg":
                StatsManager.Instance.UpdateMaxDmg(1);
                break;

            case "Add New Attack":
                PlayerCombat.Instance.UnlockAttack2();
                break;

            case "Add Passive Healing":
                StatsManager.Instance.UpgradePassiveHealing();
                break;

            default:
                Debug.LogWarning("Skill not found" + skillName);
                break;
        }
    }
}
