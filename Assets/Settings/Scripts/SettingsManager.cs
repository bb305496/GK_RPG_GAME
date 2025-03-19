using UnityEngine;
using UnityEditor.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public TMP_Dropdown graphicsDropdown;

    public void ChangeGraphicsQuality()
    {
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }
}
