using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TeleportToRandomPoint : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> randomPointList = new List<GameObject>();

    private GameObject player;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip audioClip;

    [SerializeField]
    private List<AudioClip> audioClipList = new List<AudioClip>();

    private bool isTeleportActivate = false;

    private bool alreadyTeleported = false;

    private float rate = 0.1f;

    private float lastCall;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTeleportActivate && !alreadyTeleported)
        {
            if ((Time.time - lastCall) >= (1f / rate))
            {
                lastCall = Time.time;
                audioSource.clip = GetRandomSound(audioClipList);
                audioSource.Play();
            }
            

            if (Input.GetKeyDown(KeyCode.F))
            {
                GetRandomPoint(randomPointList);
                isTeleportActivate = false;
                alreadyTeleported = true;
            }
        }
        
    }

    private void GetRandomPoint(List<GameObject> listToRandomize)
    {
        int randomNum = Random.Range(0, listToRandomize.Count);
        GameObject randomPoint = listToRandomize[randomNum];

        player = GameObject.FindWithTag("Player");

        player.transform.position = randomPoint.transform.position;
    }

    private AudioClip GetRandomSound(List<AudioClip> listToRandomize)
    {
        int randomNum = Random.Range(0, listToRandomize.Count);
        AudioClip randomSound = listToRandomize[randomNum];
        return randomSound;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTeleportActivate = true;
            if (!alreadyTeleported)
            {
                StartCoroutine(StartTrollSound());
            }
        }
    }

    IEnumerator StartTrollSound()
    {
        yield return new WaitForSeconds(3.0f);
        audioSource.clip = audioClip;
        audioSource.Play();
        lastCall = Time.time;

    }
}
