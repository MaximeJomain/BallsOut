using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        InstantiatePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiatePlayer()
    {
        Instantiate(playerPrefab, transform.position, transform.rotation);
    }
}
