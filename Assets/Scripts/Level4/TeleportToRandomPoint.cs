using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TeleportToRandomPoint : MonoBehaviour
{

    #region variable declaration
    [SerializeField]
    private List<GameObject> randomPointList = new List<GameObject>();

    private GameObject player;

    [SerializeField]
    private GameObject explosionPrefab;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip audioClip;

    [SerializeField]
    private AudioClip afterFClip;

    [SerializeField]
    private AudioClip afterTClip;

    [SerializeField]
    private List<AudioClip> audioClipList = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> audioAfterFClipList = new List<AudioClip>();

    [SerializeField]
    private bool isTeleportActivate = false;

    private bool alreadyTeleported = false;

    [SerializeField]
    private bool isSuicidActivate = false;

    private bool alreadySuicided = false;

    private float rate = 0.1f;

    private float lastCall;

    [SerializeField]
    private int index = -1;

    [SerializeField]
    private GameObject spawnPoint;

    #endregion

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
                audioSource.clip = afterFClip;
                audioSource.Play();
                StartCoroutine(PlayNextSound());
            }
        }
        if (isSuicidActivate && !alreadySuicided)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                // Ins?rez coroutine le temps que anim explosion se finisse
                alreadySuicided = true;
                player = GameObject.FindWithTag("Player");

                Instantiate(explosionPrefab, player.transform.position, player.transform.rotation);
                Destroy(player);
                StartCoroutine(WaitPlayerExplosion());
                
            }
        }

    }

    private AudioClip GetOrderedSound(List<AudioClip> listToRandomize)
    {
        if (index + 1 < listToRandomize.Count)
        {
            index++;
            return listToRandomize[index];
        }
        else
        {
            return listToRandomize[0];
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
            if (!alreadyTeleported)
            {
                StartCoroutine(StartTrollSound());
            }
        }
    }

    IEnumerator StartTrollSound()
    {
        yield return new WaitForSeconds(3.0f);
        if (!isTeleportActivate)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            lastCall = Time.time;
            isTeleportActivate = true;
        }
    }

    IEnumerator WaitPlayerExplosion()
    {
        yield return new WaitForSeconds(2.0f);

        audioSource.clip = afterTClip;
        audioSource.Play();

        StartCoroutine(RespawnPlayer());

    }

    IEnumerator PlayNextSound()
    {
        for (int i = 0; i < audioAfterFClipList.Count; i++)
        {
            yield return new WaitForSeconds(15.0f);
            audioSource.clip = GetOrderedSound(audioAfterFClipList);
            audioSource.Play();
        }

        isSuicidActivate = true;

    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(3f);

        isTeleportActivate = false;

        alreadyTeleported = false;

        isSuicidActivate = false;

        alreadySuicided = false;

        spawnPoint.GetComponent<SpawnPlayer>().InstantiatePlayer();
    }

}
