using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

    [SerializeField]
    private GameObject spawnPoint;

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
            Debug.Log("Dead");
            GameObject playerGameObject = other.gameObject;
            Destroy(playerGameObject);
            StartCoroutine(RespawnPlayer());
        }
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(3f);

        spawnPoint.GetComponent<SpawnPlayer>().InstantiatePlayer();
    }
}
