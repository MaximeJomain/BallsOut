using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface EventScript
{
    public void SetPlayerInstance(PlayerController playerInstance);
}

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    
    public void Start()
    {
        InstantiatePlayer();
    }

    public void InstantiatePlayer()
    {
        Instantiate(playerPrefab, transform.position, transform.rotation);
    }
}
