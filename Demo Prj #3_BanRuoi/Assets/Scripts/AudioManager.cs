using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;

    public AudioClip music;
    public AudioClip shootingSound;
    public AudioClip deathSound;
    public AudioClip loseSound;

    public void Start()
    {
        musicAudioSource.clip = music;
        musicAudioSource.Play();
    }

    public void playSFX(AudioClip sfx)
    {
        sfxAudioSource.clip = sfx;
        sfxAudioSource.PlayOneShot(sfx);
    }
}
