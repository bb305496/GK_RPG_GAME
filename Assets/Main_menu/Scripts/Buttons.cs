using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{
    public void PlayButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OptionsButtonClick()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitButtonClick()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
