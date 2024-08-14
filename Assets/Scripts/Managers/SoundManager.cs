using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] Music;

    [SerializeField] private AudioSource musicSource;


    void Awake()
    {
        musicSource.clip = Music[0];
        musicSource.Play();

    }
}

