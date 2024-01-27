using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpKill : MonoBehaviour
{
    private PlayerController player;

    // private void Update()
    // {
    //     if (!player)
    //     {
    //         throw new NotImplementedException();
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        player.canJump = true;
    }

    private void OnTriggerExit(Collider other)
    {
        player.canJump = false;
    }
}
