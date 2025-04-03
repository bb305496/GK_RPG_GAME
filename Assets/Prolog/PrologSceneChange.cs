using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PrologSceneChange : MonoBehaviour
{
    public float videoTime = 81f;
    public TMP_Text skippText;
    private float timeLeft = 5f;
    void Start()
    {
        StartCoroutine(DelayBeforeSceneChange(videoTime, 4));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(4);
        }

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            timeLeft = 0;
            skippText.gameObject.SetActive(false);
        }
    }

    private IEnumerator DelayBeforeSceneChange(float seconds, int sceneInxed)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneInxed);
    }


}
