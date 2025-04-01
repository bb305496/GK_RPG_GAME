using UnityEngine;

public class ToogleSkillTree : MonoBehaviour
{
    public CanvasGroup statsCanvas;
    private bool isStatsOpen = false;

    private void Update()
    {
        if(Input.GetButtonDown("ToggleSkillTree"))
        {
            if(isStatsOpen)
            {
                Time.timeScale = 1;
                statsCanvas.alpha = 0;
                statsCanvas.blocksRaycasts = false;
                isStatsOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                statsCanvas.alpha = 1;
                statsCanvas.blocksRaycasts = true;
                isStatsOpen = true;
            }
        }
    }
}
