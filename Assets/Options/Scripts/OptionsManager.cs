using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class OptionsManager : MonoBehaviour
{
    public TMP_Dropdown graphicsDropdown;
    //public Slider volumeSlider;
    //public AudioMixer audioMixer;

    public void ChangeGraphicsQuality()
    {
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }

    //public void ChangeVolume()
    //{
    //    audioMixer.SetFloat("Volume", volumeSlider.value);
    //}
}
