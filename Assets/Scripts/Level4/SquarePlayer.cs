using UnityEngine;

public class SquarePlayer : MonoBehaviour
{
    private PlayerController player;
    public Mesh sphereMesh, squareMesh;

    private AudioSource audioSource;
    
    [SerializeField]
    private AudioClip audioIntro;

    [SerializeField]
    private AudioClip audioClip1;

    [SerializeField]
    private AudioClip audioClip2;

    [SerializeField]
    private AudioClip audioClip3;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        player.GetComponent<MeshFilter>().mesh = squareMesh;
        player.canMove = false;
        player.eventBlockTime = true;

        audioSource.clip = audioIntro;
        audioSource.Play();
        Invoke(nameof(WaitIntro), 5.0f);
    }
    
    private void WaitIntro()
    {
        audioSource.clip = audioClip1;
        audioSource.Play();
        Invoke(nameof(PlayClip2), 4.0f);
    }

    private void PlayClip2()
    {
        audioSource.clip = audioClip2;
        audioSource.Play();
        Invoke(nameof(PlayClip3), 8.0f);
    }

    private void PlayClip3()
    {
        audioSource.clip = audioClip3;
        audioSource.Play();
        Invoke(nameof(ResetToSphere), 1.0f);
    }
    
    private void ResetToSphere()
    {
        player.GetComponent<MeshFilter>().mesh = sphereMesh;
        player.eventBlockTime = false;
        player.canMove = true;
    }
}
