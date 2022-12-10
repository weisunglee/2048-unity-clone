using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip moveSound;
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] AudioClip winSound;

    private const float volume = 0.7f;

    public void PlayMoveSound()
    {     
        if (Time.timeScale == 1) 
        {
            audioSource.PlayOneShot(moveSound, volume);
        }
    }

    public void PlayGameOverSound()
    {
        if (Time.timeScale == 1)
        {
            audioSource.PlayOneShot(gameOverSound, volume);
        }            
    }

    public void PlayWinSound()
    {
        if (Time.timeScale == 1)
        {
            audioSource.PlayOneShot(winSound, volume);
        }
    }
}
