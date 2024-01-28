using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSingleSound : MonoBehaviour
{
    public AudioClip audioClip;
    public bool destroyAfterPlay;

    private AudioSource audioSource;
    private Collider triggerCollider;
    
    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        triggerCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(audioClip);

            if (destroyAfterPlay)
            {
                triggerCollider.enabled = false;
            }
        }
    }
}
