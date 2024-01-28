using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Manager : MonoBehaviour
{

    [SerializeField]
    private GameObject particlesPrefab;

    [SerializeField]
    private GameObject particles;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(particlesPrefab, particles.transform.position, particles.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Yo");
        }
    }
}
