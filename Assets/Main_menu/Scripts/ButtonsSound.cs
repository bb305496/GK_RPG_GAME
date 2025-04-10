using UnityEngine;

public class ButtonsSound : MonoBehaviour
{
    [SerializeField] AudioSource buttonClick;

    public void PlayButtonSound()
    {
        buttonClick.Play();
    }
}
