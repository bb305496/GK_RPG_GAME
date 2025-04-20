using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsScript : MonoBehaviour
{

    public void PlayButtonClick()
    {
        StartCoroutine(DelayBeforeSceneChange("GameMenuScene"));
    }

    public void OptionsButtonClick()
    {
        StartCoroutine(DelayBeforeSceneChange("SettingsScene"));
    }

    public void CreditsButtonClick()
    {
        StartCoroutine(DelayBeforeSceneChange("CreditsScene"));
    }

    public void ExitButtonClick()
    {
        StartCoroutine(DelayBeforeExit());
    }

    public void BackButtonsClick()
    {
        StartCoroutine(DelayBeforeSceneChange("MainMenuScene"));
    }

    public void NewGameClick()
    {
        if (AudioManager.instance != null)
        {
            Destroy(AudioManager.instance.gameObject);
        }

        StartCoroutine(DelayBeforeSceneChange("PrologScene"));
    }

    public void LoadGameClick()
    {
        UnityEngine.Debug.Log("Load Game clicked");
    }

    private IEnumerator DelayBeforeSceneChange(string sceneName)
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator DelayBeforeExit()
    {
        yield return new WaitForSeconds(0.7f);
        UnityEngine.Debug.Log("Exit");
        Application.Quit();
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
