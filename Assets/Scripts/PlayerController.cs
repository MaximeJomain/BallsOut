using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public new Rigidbody rigidbody;
    public float movementSpeed;
    public Vector2 input;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        if (input != Vector2.zero)
        {
            Vector3 movement = new Vector3(input.x, 0f, input.y);
            rigidbody.velocity = movement * movementSpeed;
        }
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        Debug.Log("Move : " + input);
    }
}
