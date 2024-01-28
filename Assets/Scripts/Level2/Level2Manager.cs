using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour
{
    
    private GameManager _gameManager;

    [SerializeField]
    private GameObject deathZone;

    public GameObject player;

    public PlayerController controller;

    [SerializeField]
    private AudioClip audioClipStart1;

    [SerializeField]
    private AudioClip audioClipStart2;
    
    [SerializeField]
    private AudioClip audioClipFinsih1;

    [SerializeField]
    private AudioClip audioClipFinish2;

    [SerializeField]
    private AudioSource audioSource;

    private int deathNumber;

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager.deathCount > 1)
        {
            audioSource.clip = audioClipStart2;
        }
        else
        {
            audioSource.clip = audioClipStart1;
        }
        audioSource.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        controller = player.GetComponent<PlayerController>();
        controller.eventBlockTime = true;
        controller.canMove = false;
        StartCoroutine(WaitSpeech(audioSource.clip.length));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.clip = !_gameManager.lvl2NotListening ? audioClipFinsih1 : audioClipFinish2;
            controller.eventBlockTime = true;
            controller.canMove = false;
            audioSource.Play();
            StartCoroutine(WaitFinish(audioSource.clip.length));
        }
    }

    IEnumerator WaitSpeech(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (_gameManager.deathCount > 1 && audioSource.clip == audioClipStart2)
        {
            audioSource.clip = audioClipStart1;
            audioSource.Play();
            StartCoroutine(WaitSpeech(audioSource.clip.length));
        }
        else
        {
            controller.eventBlockTime = false;
            controller.canMove = true;
        }
    }

    IEnumerator WaitFinish(float duration)
    {
        yield return new WaitForSeconds(duration);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}