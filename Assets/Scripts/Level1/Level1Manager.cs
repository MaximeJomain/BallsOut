using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Manager : MonoBehaviour
{

    [SerializeField]
    private GameObject deathZone;

    public GameObject player;

    [SerializeField]
    private AudioClip audioClip1;

    [SerializeField]
    private AudioClip audioClip2;

    [SerializeField]
    private AudioClip audioClip4;

    [SerializeField]
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource.clip = audioClip1;
        audioSource.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
       
        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerController>().enabled = false;

        StartCoroutine(WaitIntro());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int deathNumber = deathZone.GetComponent<DeathZone>().deathCount;

            if (deathNumber == 0)
            {
                player.GetComponent<PlayerController>().enabled = false;
                audioSource.clip = audioClip2;
                audioSource.Play();

                StartCoroutine(WaitDialog(3.0f));
            }
            else if (deathNumber == 1)
            {
                player.GetComponent<PlayerController>().enabled = false;
                audioSource.clip = audioClip4;
                audioSource.Play();

                StartCoroutine(WaitDialog(6.5f));
            }
        }
    }

    IEnumerator WaitIntro()
    {
        yield return new WaitForSeconds(22.0f);

        player.GetComponent<PlayerController>().enabled = true;
    }

    IEnumerator WaitDialog(float duration)
    {
        yield return new WaitForSeconds(duration);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
