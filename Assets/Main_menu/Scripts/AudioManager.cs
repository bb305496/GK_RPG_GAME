using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-----------Audio Source-----------")]
    [SerializeField] AudioSource bgMusicSource;
    [SerializeField] AudioSource campfireMusicSource;
    [SerializeField] AudioSource buttonClick;

    public void PlayButtonSound()
    {
        buttonClick.Play();
    }

}
