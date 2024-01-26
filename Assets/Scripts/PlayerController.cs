using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public float movementSpeed, cameraSpeed;
    
    
    private Vector2 moveInput, lookInput;
    private new Rigidbody rigidbody;
    private Camera mainCamera;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }
    
    private void FixedUpdate()
    {
        if (moveInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.Euler(0f, mainCamera.transform.rotation.eulerAngles.y, 0f);
            Vector3 moveDirection = targetRotation * new Vector3(moveInput.x, 0f, moveInput.y);
            
            // Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y);
            rigidbody.velocity = moveDirection * movementSpeed;
        }
    }

    private void Update()
    {
        if (lookInput != Vector2.zero)
        {
            Debug.Log("Look : " + lookInput);
        
            lookInput.x *= 180f; 
            freeLookCamera.m_XAxis.Value += lookInput.x * cameraSpeed * Time.deltaTime;
            freeLookCamera.m_YAxis.Value += lookInput.y * cameraSpeed * Time.deltaTime;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    
    public void Look(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>().normalized;
    }
}
