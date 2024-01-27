using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    public Light blueLight, redLight;
    public AudioSource audioSource;
    public AudioClip audio1, audio2;
    public TMP_Text selectionText;
    
    private Camera mainCamera;
    private bool canInteract;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        canInteract = false;
        selectionText.gameObject.SetActive(false);
        StartCoroutine(AudioCoroutine());
    }

    private void Update()
    {
        if (!canInteract) return;
        
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            if (hit.transform.CompareTag("BlueBall"))
            {
                blueLight.intensity = 5f;
                if (Input.GetMouseButtonDown(0))
                {
                    PlayerPrefs.SetInt("PlayerColor", 0);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
                }
            }
            else if (hit.transform.CompareTag("RedBall"))
            {
                redLight.intensity = 5f;
                if (Input.GetMouseButtonDown(0))
                {
                    PlayerPrefs.SetInt("PlayerColor", 1);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
                }
            }
            else
            {
                blueLight.intensity = 2f;
                redLight.intensity = 2f;
            }
        }
    }

    private IEnumerator AudioCoroutine() {
        audioSource.clip = audio1;
        audioSource.Play();
        
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.clip = audio2;
        audioSource.Play();
        
        yield return new WaitForSeconds(audioSource.clip.length);
        canInteract = true;
        selectionText.gameObject.SetActive(true);
    }
}
