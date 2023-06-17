using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioSource playerSource;

    public List<AudioClip> sfxClips;

    private float footStepDelay = 0.35f;
    private float nextFootStep = 0;

    public void PlayPlayerMove()
    {
        if (Time.time > nextFootStep)
        {
            nextFootStep = Time.time + footStepDelay;
            playerSource.PlayOneShot(sfxClips[0]);
        }
    }

    public void PlayPlayerDash()
    {
        playerSource.PlayOneShot(sfxClips[1]);
    }

    public void PlayPlayerDeath()
    {
        playerSource.PlayOneShot(sfxClips[2]);
    }
}
