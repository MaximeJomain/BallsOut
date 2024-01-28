using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public float movementSpeed, cameraSpeed;
    public Material blueMaterial, redMaterial;
    
    public bool invertControls, canMove, canJump, eventBlockTime;
    
    private Vector2 moveInput, lookInput;
    private new Rigidbody rigidbody;
    private Camera mainCamera;
    private MeshRenderer meshRenderer;
    private SpawnPlayer respawnPoint;

    private void Awake()
    {
        Physics.gravity = new Vector3(0f, -40f, 0f);
        rigidbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        canMove = true;
        eventBlockTime = false;
        meshRenderer = GetComponent<MeshRenderer>();
        respawnPoint = GameObject.FindGameObjectWithTag("SpawnPlayer").GetComponent<SpawnPlayer>();
    }

    private void Start()
    {
        meshRenderer.material = PlayerPrefs.GetInt("PlayerColor", 0) == 0 ? blueMaterial : redMaterial;
        invertControls = false;
        canJump = false;
    }

    private void FixedUpdate()
    {
        if (!canMove) return;
        
        if (moveInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.Euler(0f, mainCamera.transform.rotation.eulerAngles.y, 0f);

            Vector3 movementInput = invertControls ? new Vector3(-moveInput.x, 0f, -moveInput.y) : new Vector3(moveInput.x, 0f, moveInput.y);
            Vector3 moveDirection = targetRotation * movementInput;
            
            // Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y);
            rigidbody.velocity = moveDirection * movementSpeed;
        }
    }

    private void Update()
    {
        if (lookInput != Vector2.zero)
        {
            lookInput.x *= 180f; 
            freeLookCamera.m_XAxis.Value += lookInput.x * cameraSpeed * Time.deltaTime;
            freeLookCamera.m_YAxis.Value += lookInput.y * cameraSpeed * Time.deltaTime;
        }
    }

    private void Die()
    {
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    
    public void Look(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>().normalized;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Plain") && !eventBlockTime)
        {
            canMove = true;
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Plain"))
        {
            canMove = false;
        }
    }
    
    public void Jump()
    {
        if (canJump)
        {
            canJump = false;
            Level3 level3 = GameObject.Find("Level 3 Manager").GetComponent<Level3>();
            if (!level3.hasJumped)
            {
                level3.PlayAudioCoroutine(2);
                level3.hasJumped = true;
                respawnPoint.RespawnPlayer();
                Destroy(gameObject);
            }
        }
    }
}
