using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventScript : MonoBehaviour
{
    private PlayerController playerController;
    public abstract void SetPlayerInstance(PlayerController playerInstance);
}

public class SpawnPlayer : MonoBehaviour
{
    public EventScript[] eventScriptList;
    public float respawnTime;
    
    [SerializeField]
    private GameObject playerPrefab;
    
    public void Start()
    {
        InstantiatePlayer();
    }

    public void InstantiatePlayer()
    {
        var instance = Instantiate(playerPrefab, transform.position, transform.rotation);
        PlayerController playerInstance = instance.GetComponent<PlayerController>();

        if (playerInstance)
        {
            foreach (var eventScript in eventScriptList)
            {
                eventScript.SetPlayerInstance(playerInstance);
            }
        }
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnPlayerRoutine());
    }
    
    IEnumerator RespawnPlayerRoutine()
    {
        yield return new WaitForSeconds(respawnTime);
    
        InstantiatePlayer();
    }
}
