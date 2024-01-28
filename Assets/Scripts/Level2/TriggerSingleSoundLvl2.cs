using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSingleSoundLvl2 : MonoBehaviour
{

    private bool triggered = false;
    
    private AudioSource audioSource;

    private GameManager _gameManager;

    [SerializeField] private bool notListening, soloTrigger;


    [SerializeField]
    private AudioClip audioClip;
    [SerializeField]
    private AudioClip audioClipNotListening;

    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!triggered)
        {
            triggered = true;
            
            if (soloTrigger)
            {
                audioSource.clip = _gameManager.lvl2NotListening ? audioClipNotListening : audioClip;
            }
            else
            {
                if (!_gameManager.lvl2NotListening)
                {
                    _gameManager.lvl2NotListening = notListening;
                }
                

                switch (_gameManager.lvl2NotListening)
                {
                    case true when notListening:
                        audioSource.clip = audioClipNotListening;
                        break;
                    case true:
                        return;
                    case false when notListening:
                        audioSource.clip = audioClipNotListening;
                        break;
                    case false:
                        audioSource.clip = audioClip;
                        break;
                }
            }
            audioSource.Play();
        }
    }
}