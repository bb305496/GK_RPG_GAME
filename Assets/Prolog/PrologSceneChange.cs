using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologSceneChange : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DelayBeforeSceneChange(81f, 4));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(4);
        }
    }

    private IEnumerator DelayBeforeSceneChange(float seconds, int sceneInxed)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneInxed);
    }
}
