using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] Music;
    public AudioClip[] Sounds;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource;

    void Awake()
    {
        musicSource.clip = Music[0];
        musicSource.Play(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
