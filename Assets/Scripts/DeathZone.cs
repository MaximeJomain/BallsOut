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

    public int deathCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject playerGameObject = other.gameObject;
            Destroy(playerGameObject);
            if (isFirstLvl)
            {
                deathCount++;

                if (deathCount < 2)
                {
                    StartCoroutine(RespawnPlayer());
                } else
                {
                    StartCoroutine(LoadNextScene());
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

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(12f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
