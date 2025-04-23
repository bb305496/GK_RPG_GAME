using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneChanger : MonoBehaviour
{
    public string SceneToLoad;
    public Animator fadeAnim;
    public float fadeTime = 1f;
    public Vector2 newPlayerPosition;
    public Image levelReqimage;
    public TMP_Text levelReqText;
    private Transform player;

    private void Start()
    {
        if (levelReqimage != null)
        {
            levelReqimage.enabled = false;
        }
        if (levelReqText != null)
        {
            levelReqText.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && StatsManager.Instance.level >= 10)
        {
            player = collision.transform;
            fadeAnim.Play("FadeToBlack");
            StartCoroutine(DelayFade());
        }
        else
        {
            if(levelReqimage != null)
            {
                levelReqimage.enabled = true;
            }
            if (levelReqText != null)
            {
                levelReqText.enabled = true;
            }
            Debug.Log("You need to be level 10 to enter this area.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (levelReqimage != null)
            {
                levelReqimage.enabled = false;
            }
            if (levelReqText != null)
            {
                levelReqText.enabled = false;
            }
        }
    }

    IEnumerator DelayFade()
    {
        yield return new WaitForSeconds(fadeTime);
        player.position = newPlayerPosition;
        SceneManager.LoadScene(SceneToLoad);
    }
}
