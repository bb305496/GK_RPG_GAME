using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsScript : MonoBehaviour
{
    public void PlayButtonClick()
    {
        if(AudioManager.instance != null)
        {
            Destroy(AudioManager.instance.gameObject);
        }

        StartCoroutine(DelayBeforeSceneChange(1));
    }

    public void OptionsButtonClick()
    {
        StartCoroutine(DelayBeforeSceneChange(2));
    }

    public void ExitButtonClick()
    {
        StartCoroutine(DelayBeforeExit());
    }

    public void BackButtonsClick()
    {
        StartCoroutine(DelayBeforeSceneChange(0));
    }
 
    private IEnumerator DelayBeforeSceneChange(int sceneInxed)
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(sceneInxed);
    }

    private IEnumerator DelayBeforeExit()
    {
        yield return new WaitForSeconds(0.7f);
        Debug.Log("Exit");
        Application.Quit();
    }
}
