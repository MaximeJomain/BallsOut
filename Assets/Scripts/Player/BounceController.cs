using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceController : MonoBehaviour
{
    private Rigidbody rb;
    Vector2 _movement;

   private PlayerController playerController;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
    }

    void OnCollisionEnter(Collision other)
    {
        if( other.gameObject.CompareTag("Wall"))
        {
            playerController.canMove = false;
            const float bounceBack = 80f; 
            const float bounceUp = 40f; 
            rb.AddForce( bounceBack,  bounceUp, 0, ForceMode.Impulse);
            Invoke("PlayerCanMove" , 2f );
        }
    }

    void PlayerCanMove()
    {
        playerController.canMove = true;
    }
}
