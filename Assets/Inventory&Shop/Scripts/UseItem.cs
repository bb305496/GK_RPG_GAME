using System.Collections;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public void ApplyItemEffect(ItemSO itemSO)
    {
        if (itemSO.maxHealth > 0)
            StatsManager.Instance.UpdateMaxHealth(itemSO.maxHealth);

        if (itemSO.currentHealth > 0)
            StatsManager.Instance.UpdateMaxHealth(itemSO.currentHealth);

        if (itemSO.damage > 0)
            StatsManager.Instance.UpdateMaxHealth(itemSO.damage);

        if (itemSO.speed > 0)
            StatsManager.Instance.UpdateSpeed(itemSO.speed);

        if (itemSO.duration > 0)
            StartCoroutine(EffectTimer(itemSO, itemSO.duration));

    }

    
    private IEnumerator EffectTimer(ItemSO itemSO, float duration)
    {
        yield return new WaitForSeconds(duration);

        if (itemSO.maxHealth > 0)
            StatsManager.Instance.UpdateMaxHealth(-itemSO.maxHealth);

        if (itemSO.currentHealth > 0)
            StatsManager.Instance.UpdateMaxHealth(itemSO.currentHealth);

        if (itemSO.damage > 0)
            StatsManager.Instance.UpdateMaxHealth(itemSO.damage);

        if (itemSO.speed > 0)
            StatsManager.Instance.UpdateSpeed(-itemSO.speed);
    }
}
