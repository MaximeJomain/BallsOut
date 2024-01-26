using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSingleSound : MonoBehaviour
{

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip audioClip;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}
