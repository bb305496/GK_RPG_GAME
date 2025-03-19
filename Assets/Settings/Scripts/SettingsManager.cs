using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public TMP_Dropdown graphicsDropdown;
    public Slider masterVol, musicVol, SFXVol;
    public AudioMixer mainAudioMixer;

    public void ChangeGraphicsQuality()
    {
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }

    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("MasterVol", masterVol.value);
    }

    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("MusicVol", musicVol.value);
    }

    public void ChangeSFXVolume()
    {
        mainAudioMixer.SetFloat("SFXVol", SFXVol.value);
    }
}
