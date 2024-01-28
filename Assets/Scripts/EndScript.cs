using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    private float interval = 0.2f; 
    
    private float maxWait = 0.6f;
    private float maxFlicker = 0.2f;

    private float timer = 2f;

    private bool flicking = false;

    public Light myLight;

    public AudioClip audio1, audioBoom;

    public AudioSource _audioSource, _audioSourceBoom;

    public GameObject _all;

    public GameObject _ball;

    public GameObject _explosionPrefab;

    private void Awake()
    {
        _audioSource.clip = audio1;
        _audioSource.Play();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitSpeech(audio1.length));
    }

    // Update is called once per frame
    void Update()
    {
        if (!flicking) return;
        timer += Time.deltaTime;
        if (timer > interval)
        {
            ToggleLight();
        }
    }

    IEnumerator WaitSpeech(float duration)
    {
        yield return new WaitForSeconds(duration);
        flicking = true;
        StartCoroutine(ShutdownAll());
    }
    
    void ToggleLight()
    {
        myLight.enabled = !myLight.enabled;
        if (myLight.enabled)
        {
            interval = Random.Range(0, maxWait);
        }
        else 
        {
            interval = Random.Range(0, maxFlicker);
        }
    
        timer = 0;
    }

    IEnumerator ShutdownAll()
    {
        yield return new WaitForSeconds(2f);
        flicking = false;
        myLight.intensity = 0f;
        Instantiate(_explosionPrefab, _ball.transform.position, _ball.transform.rotation);
        _audioSourceBoom.clip = audioBoom;
        _audioSourceBoom.Play();
        Destroy(_all);
        StartCoroutine(ReturnIntro(audioBoom.length));

    }

    IEnumerator ReturnIntro(float duration)
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(0);

    }
}
