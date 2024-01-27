using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceController : MonoBehaviour
{
    private Rigidbody rb;
    Vector2 _movement;

   private PlayerController playerController;
   private Camera mainCamera;
   
   [SerializeField]
   private AudioClip audioClip1;

   [SerializeField]
   private AudioSource audioSource;
   
   private void Awake()
   {
       audioSource.clip = audioClip1;
       mainCamera = Camera.main;
       rb = GetComponent<Rigidbody>();
       playerController = GetComponent<PlayerController>();
   }


    void OnCollisionEnter(Collision other)
    {
        if( other.gameObject.CompareTag("Wall"))
        {
            audioSource.Play();
            playerController.canMove = false;
            const float bounceUp = -1f;
            const float bounceBack = 1250f;
            var transform1 = mainCamera.transform;
            var forward = transform1.forward;
            Vector3 bounceVector = new Vector3((forward).x, bounceUp, (forward).z);
            rb.AddForce(-bounceVector * bounceBack, ForceMode.Impulse);
            rb.angularVelocity = Vector3.zero;
            
        }
    }
}
