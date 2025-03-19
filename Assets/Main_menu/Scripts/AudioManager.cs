using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("-----------Audio Source-----------")]
    [SerializeField] AudioSource bgMusicSource;
    [SerializeField] AudioSource campfireMusicSource;
    [SerializeField] AudioSource buttonClick;

    private void Start()
    {
        bgMusicSource.Play();
        campfireMusicSource.Play();
    }

    public void PlayButtonSound()
    {
        buttonClick.Play();
    }
}
