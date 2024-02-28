using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    PhotonView view;
        public PhotonView photonView;
    public float speed = 5f;
    public float sensitivity = 2f;

    private Vector2 movementInput;
    private Vector2 lookInput;
    private CharacterController characterController;

private Rigidbody playerrb;
    private float timeSinceLastInput =0f;
    private float timeToStop =1f;


    private void Start()
    {

        playerrb = GetComponent<Rigidbody>();
        
        characterController = GetComponent<CharacterController>();
    }
public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
    private void Update()
    {
         Move();
         Look();
         
    }
   

    public void Move()
    { Vector3 cameraForward = Camera.main.transform.forward;
    cameraForward.y = 0f; // Ensure the y component is zero to avoid tilting

    // Normalize the input vector to avoid faster movement diagonally
    Vector3 moveInput = new Vector3(movementInput.x, 0f, movementInput.y).normalized;

    // Calculate the movement direction based on camera orientation
    Vector3 moveDirection = cameraForward * moveInput.z + Camera.main.transform.right * moveInput.x;

    // Check if any input is being received
    if (moveDirection != Vector3.zero)
    {
        // Apply force for movement
        playerrb.AddForce(moveDirection * speed, ForceMode.Force);

        // Reset the timer since there's input
        timeSinceLastInput = 0f;
    }
    else
    {
        // Increment the timer when no input is received
        timeSinceLastInput += Time.deltaTime;

        // Use Mathf.Lerp to gradually decrease the velocity to zero over time
        float t = Mathf.Clamp01(timeSinceLastInput / timeToStop);
        playerrb.velocity = Vector3.Lerp(playerrb.velocity, Vector3.zero, t);
        
    }
    }

    public void Look()
    {
        Vector2 lookDelta = lookInput * sensitivity;
        transform.Rotate(Vector3.up * lookDelta.x);

        // Invert the y-axis for looking up and down
        Camera.main.transform.Rotate(Vector3.left * lookDelta.y);
    }

 

}