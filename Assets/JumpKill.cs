using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpKill : MonoBehaviour
{
    public PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        player.canJump = true;
    }

    private void OnTriggerExit(Collider other)
    {
        player.canJump = false;
    }
}
