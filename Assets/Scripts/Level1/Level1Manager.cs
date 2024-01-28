using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Manager : MonoBehaviour
{

    [SerializeField]
    private GameObject deathZone;

    public PlayerController player;

    [SerializeField]
    private AudioClip audioClip1;

    [SerializeField]
    private AudioClip audioClip2;

    [SerializeField]
    private AudioClip audioClip4;

    [SerializeField]
    private AudioSource audioSource;

    public AudioSource bgMusic;

    private void Awake()
    {
        DontDestroyOnLoad(bgMusic);
        audioSource.clip = audioClip1;
        audioSource.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.ambientIntensity = 1f;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        player.canMove = false;
        player.eventBlockTime = true;

        StartCoroutine(WaitIntro());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int deathNumber = deathZone.GetComponent<DeathZone>().deathCount;

            if (deathNumber == 0)
            {
                player.canMove = false;
                player.eventBlockTime = true;
                audioSource.clip = audioClip2;
                audioSource.Play();

                StartCoroutine(WaitDialog(3.0f));
            }
            else if (deathNumber == 1)
            {
                player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
                player.canMove = false;
                player.eventBlockTime = true;
                audioSource.clip = audioClip4;
                audioSource.Play();

                StartCoroutine(WaitDialog(6.5f));
            }
        }
    }

    IEnumerator WaitIntro()
    {
        yield return new WaitForSeconds(14.0f);
        bgMusic.Play();
        
        yield return new WaitForSeconds(2f);
        player.canMove = true;
        player.eventBlockTime = false;
    }

    IEnumerator WaitDialog(float duration)
    {
        yield return new WaitForSeconds(duration);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
