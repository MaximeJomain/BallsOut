using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSoundFromList : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> songList = new List<AudioClip>();

    private AudioSource audioSource;

    [SerializeField]
    private bool isRandom;

    private int index = -1;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AudioClip GetRandomSound(List<AudioClip> listToRandomize)
    {
        int randomNum = Random.Range(0, listToRandomize.Count);
        AudioClip randomSound = listToRandomize[randomNum];
        return randomSound;
    }

    public AudioClip GetOrderedSound(List<AudioClip> listToRandomize)
    {
        if (index + 1 < listToRandomize.Count)
        {
            index++;
            return listToRandomize[index];
        } else
        {
            return listToRandomize[0];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isRandom)
            {
                audioSource.clip = GetRandomSound(songList);
                audioSource.Play();
            } else
            {
                if (index + 1 < songList.Count)
                {
                    audioSource.clip = GetOrderedSound(songList);
                    audioSource.Play();
                }
            }
        }
    }
}
