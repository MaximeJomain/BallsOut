using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToRandomPoint : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> randomPointList = new List<GameObject>();

    private GameObject player;

    private bool isTeleportActivate = false;

    private bool alreadyTeleported = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isTeleportActivate && !alreadyTeleported)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                GetRandomPoint(randomPointList);
                isTeleportActivate = false;
                alreadyTeleported = true;
            }
        }
        
    }

    private void GetRandomPoint(List<GameObject> listToRandomize)
    {
        int randomNum = Random.Range(0, listToRandomize.Count);
        GameObject randomPoint = listToRandomize[randomNum];

        player.transform.position = randomPoint.transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTeleportActivate = true;
        }
    }
}
