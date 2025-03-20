using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public TMP_Dropdown graphicsDropdown;
    public Slider masterVol, musicVol, SFXVol;
    public AudioMixer mainAudioMixer;

    private void Start()
    {
        LoadSettings(); 
    }

    public void ChangeGraphicsQuality()
    {
        int qualityLevel = graphicsDropdown.value;
        QualitySettings.SetQualityLevel(qualityLevel);
        PlayerPrefs.SetInt("GraphicsQuality", qualityLevel);
    }

    public void ChangeMasterVolume()
    {
        float volume = masterVol.value;
        mainAudioMixer.SetFloat("MasterVol", volume);
        PlayerPrefs.SetFloat("MasterVol", volume);
    }

    public void ChangeMusicVolume()
    {
        float volume = musicVol.value;
        mainAudioMixer.SetFloat("MusicVol", volume);
        PlayerPrefs.SetFloat("MusicVol", volume);
    }

    public void ChangeSFXVolume()
    {
        float volume = SFXVol.value;
        mainAudioMixer.SetFloat("SFXVol", volume);
        PlayerPrefs.SetFloat("SFXVol", volume);
    }

    private void LoadSettings()
    {
        // Grafika
        if (PlayerPrefs.HasKey("GraphicsQuality"))
        {
            int qualityLevel = PlayerPrefs.GetInt("GraphicsQuality");
            QualitySettings.SetQualityLevel(qualityLevel);
            graphicsDropdown.value = qualityLevel;
        }

        // Master Volume
        if (PlayerPrefs.HasKey("MasterVol"))
        {
            float volume = PlayerPrefs.GetFloat("MasterVol");
            mainAudioMixer.SetFloat("MasterVol", volume);
            masterVol.value = volume;
        }

        // Music Volume
        if (PlayerPrefs.HasKey("MusicVol"))
        {
            float volume = PlayerPrefs.GetFloat("MusicVol");
            mainAudioMixer.SetFloat("MusicVol", volume);
            musicVol.value = volume;
        }

        // SFX Volume
        if (PlayerPrefs.HasKey("SFXVol"))
        {
            float volume = PlayerPrefs.GetFloat("SFXVol");
            mainAudioMixer.SetFloat("SFXVol", volume);
            SFXVol.value = volume;
        }
    }
}
