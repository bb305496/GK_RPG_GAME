using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    [Header("-----------Audio Source-----------")]
    [SerializeField] AudioSource bgMusicSource;
    [SerializeField] AudioSource campfireMusicSource;

    private void Start()
    {
        bgMusicSource.Play();
        campfireMusicSource.Play();
    }
}
