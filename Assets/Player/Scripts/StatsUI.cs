using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlots;
    public CanvasGroup statsCanvas;

    private bool statsOpen = false;

    public void Start()
    {
        UpdateAllStats();
    }

    public void Update()
    {
        if (Input.GetButtonDown("ToggleStats"))
            if (statsOpen)
            {
                statsCanvas.alpha = 0;
                statsOpen = false;
            }
            else
            {
                statsCanvas.alpha = 1;
                statsOpen = true;
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
