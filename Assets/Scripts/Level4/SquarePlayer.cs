using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquarePlayer : MonoBehaviour
{
    public PlayerController player;
    public Mesh sphereMesh, squareMesh;
    public float timeToReset;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.Play();
        player.GetComponent<MeshFilter>().mesh = squareMesh;
        player.canMove = false;
        Invoke(nameof(ResetToSphere), timeToReset);
    }

    private void ResetToSphere()
    {
        player.GetComponent<MeshFilter>().mesh = sphereMesh;
        player.canMove = true;
    }
}
