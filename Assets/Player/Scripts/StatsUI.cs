using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlots;
    public CanvasGroup statsCanvas;

    public void Start()
    {
        UpdateAllStats();
    }

    public void Update()
    {
        if(Input.GetButtonDown("ToggleStats"))
        {
            statsCanvas.alpha = 0;
        }
    }

    public void UpdateAllStats()
    {
        UpdateHP();
        UpdateATK();
    }
    public void UpdateHP()
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>().text = "HP " + StatsManager.Instance.maxHealth;
    }

    public void UpdateATK()
    {
        statsSlots[1].GetComponentInChildren<TMP_Text>().text = "ATK " + StatsManager.Instance.damage;
    }


}
