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

    
    private bool isTeleportActivate = false;

    private bool alreadyTeleported = false;

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

        player.transform.position = randomPoint.transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTeleportActivate = true;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}
