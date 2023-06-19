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

    private void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        PlayBackground();
}
    public void PlayPlayerMove()
    {
        if (Time.time > nextFootStep)
        {
            nextFootStep = Time.time + footStepDelay;
            playerSource.pitch = 1f + Random.Range (-0.2f, 0.2f);
            playerSource.volume = Random.Range (0.1f, 0.2f);//to simulate different foot sound
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

    public void PlayGetKey()
    {
        sfxSource.PlayOneShot(sfxClips[3]);
    }

    public void PlayUnlockDash()
    {
        sfxSource.PlayOneShot(sfxClips[4]);
    }

    public void PlayUnlockDoor()
    {
        sfxSource.PlayOneShot(sfxClips[5]);
    }

    public void PlayWin()
    {
        bgmSource.PlayOneShot(sfxClips[6]);
        Debug.Log($"PlayWin called the clips is {sfxClips[6]}");
    }
    public void PlayBackground()
    {        
        Debug.Log($"PlayBackground called the clips is {sfxClips[7]}");
        bgmSource.Stop();
        bgmSource.clip = sfxClips[7];
        bgmSource.loop = true;
        bgmSource.Play();
    }
}
