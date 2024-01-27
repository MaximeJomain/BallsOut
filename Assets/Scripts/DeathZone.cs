using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{

    [SerializeField]
    private GameObject spawnPoint;

    [SerializeField]
    private bool isFirstLvl;

    private int deathCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject playerGameObject = other.gameObject;
            Destroy(playerGameObject);
            if (isFirstLvl)
            {
                deathCount++;

                if (deathCount < 3)
                {
                    StartCoroutine(RespawnPlayer());
                } else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            } else
            {
                StartCoroutine(RespawnPlayer());
            }
        }
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(3f);

        spawnPoint.GetComponent<SpawnPlayer>().InstantiatePlayer();
    }
}
