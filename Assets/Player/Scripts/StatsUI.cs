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
                Time.timeScale = 1;
                UpdateAllStats();
                statsCanvas.alpha = 0;
                statsCanvas.blocksRaycasts = false;
                statsOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                UpdateAllStats();
                statsCanvas.alpha = 1;
                statsCanvas.blocksRaycasts = true;
                statsOpen = true;
            }
    }

    public void UpdateAllStats()
    {
        UpdateHP();
        UpdateATK();
        UpdateZWI();
    }
    public void UpdateHP()
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>().text = "HP " + StatsManager.Instance.maxHealth;
    }

    public void UpdateATK()
    {
        statsSlots[1].GetComponentInChildren<TMP_Text>().text = "ATK " + StatsManager.Instance.damage;
    }

    public void UpdateZWI()
    {
        statsSlots[2].GetComponentInChildren<TMP_Text>().text = "ZWI " + StatsManager.Instance.speed;
    }
}
