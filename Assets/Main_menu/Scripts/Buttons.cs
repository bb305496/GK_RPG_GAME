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
        //TO DO
    }

    public void ExitButtonClick()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
