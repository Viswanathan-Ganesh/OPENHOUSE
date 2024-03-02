using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;

public class FirstPersonController : MonoBehaviour
{

    PhotonView view;
    public bool CanMove { get; private set; } = true;
    private bool IsSprinting => canSprint && Input.GetKey(sprintKey);
    private bool ShouldJump => Input.GetKeyDown(jumpKey) && characterController.isGrounded;

    [Header("Optional Functions")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;


    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;

    [Header("Look Parametrs")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 100)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 100)] private float lowerLookLimit = 80.0f;

    [Header("Jumping Parameters")]
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 30.0f;

    private Camera playerCamera;
    private CharacterController characterController;

    private Vector3 moveDirection;
    private Vector2 currentInput;

    public PhotonView photonView;

    private float rotationX = 0;

    private float rotX;
    private float rotY;

    public float minX;
    public float maxX;

    public GameObject dummy;
    void Awake()
    {
        playerCamera = Camera.main;;
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Get the main camera in the scene
        Camera mainCamera = Camera.main;

        // // Make sure a main camera exists
        // if (mainCamera != null)
        // {
        //     // Set the main camera as a child of the player GameObject
        //     mainCamera.transform.SetParent(transform);

        //     // Set the local position and rotation of the main camera relative to the player
        //     mainCamera.transform.localPosition = Vector3.zero; // You may adjust this as needed
        //     mainCamera.transform.localRotation = Quaternion.identity; // You may adjust this as needed
        // }
        // else
        // {
        //     Debug.LogError("Main camera not found in the scene!");
        // }
    }
    private void Start()
    {
        //view = GetComponent<PhotonView>();
        CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();

        if (_cameraWork != null)
        {
            if(photonView.IsMine)
            {
                _cameraWork.OnStartFollowing();
            }
        }
        else
        {
            Debug.LogError("CameraWork is missing!");
        }

    }

    void Update()
    {
        
        if (photonView.IsMine && PhotonNetwork.IsConnected)
        {
            if (CanMove)
            {
                HandleMovementInput();
                HandleMouseLook();
                if (canJump)
                    HandleJump();

                ApplyFinalMovements();
            }
        }
           
    }

    private void HandleMovementInput()
    {
        currentInput = new Vector2((IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"), (IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));
        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;
    }
    private void HandleMouseLook()
    {

        //Camera.main.transform.Rotate(Vector3.right, -Input.GetAxis("Mouse Y") * 10f);
        //Camera.main.transform.Rotate(Camera.main.transform.InverseTransformDirection(Vector3.up), Input.GetAxis("Mouse X") * 10f);

        /*        Camera.main.transform.forward = transform.forward;*/
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * lookSpeedX);
        rotX += -Input.GetAxis("Mouse Y") * lookSpeedY;
        rotY += Input.GetAxis("Mouse X") * lookSpeedX;

        rotX = Mathf.Clamp(rotX, minX, maxX);
        Camera.main.transform.rotation = Quaternion.Euler(rotX, rotY, 0f);
    }

    private void HandleJump()
    {
        if (ShouldJump)
            moveDirection.y = jumpForce;

    }

    private void ApplyFinalMovements()
    {
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);

    }

}
