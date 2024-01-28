using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : MonoBehaviour
{
    public GameObject obstacle;
    public bool hasJumped;

    private PlayerController playerController;
    
    [Header("Audio Parameters")]
    public AudioClip jumpStart;
    public AudioClip jumpMiddle;
    public AudioClip jumpEnd;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(AudioCoroutine1());
    }

    public void PlayAudioCoroutine(int index)
    {
        StartCoroutine("AudioCoroutine" + index);
    }

    private IEnumerator AudioCoroutine1()
    {
        // audioSource.clip = audio1;
        // audioSource.Play();
        // yield return new WaitForSeconds(audioSource.clip.length);
        // yield return new WaitForSeconds(soundWaitTime);
        
        yield return new WaitForSeconds(0f);
        audioSource.clip = jumpStart;
        audioSource.Play();
    }
    
    private IEnumerator AudioCoroutine2()
    {
        audioSource.clip = jumpMiddle;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        yield return new WaitForSeconds(1f);
        Destroy(obstacle);

        audioSource.clip = jumpEnd;
        audioSource.Play();
    }


}
