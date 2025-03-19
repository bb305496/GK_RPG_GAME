using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{
    public void PlayButtonClick()
    {
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
