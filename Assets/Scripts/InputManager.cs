using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class InputManager : MonoBehaviour
{
    // Script References
    [SerializeField] private PlayerLocomotionHandler playerLocomotionHandler;
    [SerializeField] private CameraManager cameraManager; // Reference to CameraManager
    [SerializeField] private PlayerInputActions playerControls;


    [Header("Movement Inputs")]
    public float verticalInput;
    public float horizontalInput;
    public bool jumpInput;
    public bool sprintInput;
    public Vector2 movementInput;
    public float moveAmount;

    [Header("Camera Inputs")]
    public float scrollInput; // Scroll input for camera zoom
    public Vector2 cameraInput; // Mouse input for the camera

    private InputAction move;
    private InputAction look;
    private InputAction jump;
    private InputAction sprint;

    public bool isPauseKeyPressed = false;


    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleJumpInput();
        HandleCameraInput();
        HandlePauseKeyInput();
    }

    private void HandleCameraInput()
    {
        // Get mouse input for the camera
            cameraInput = look.ReadValue<Vector2>();

            // Get scroll input for camera zoom
            scrollInput = Input.GetAxis("Mouse ScrollWheel");

            // Send inputs to CameraManager
            cameraManager.zoomInput = scrollInput;
            cameraManager.cameraInput = cameraInput;        
    }

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        look = playerControls.Player.Look;
        look.Enable();

        jump = playerControls.Player.Jump;
        jump.Enable();

        sprint = playerControls.Player.Sprinting;
        sprint.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        look.Disable();
        jump.Disable();
    }

    private void HandleMovementInput()
    {
        // FIRST METHOD
        // movementInput
        
        // NEW METHOD
        movementInput = move.ReadValue<Vector2>();
        
        //movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        horizontalInput = movementInput.x;
        verticalInput = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
    }

    private void HandlePauseKeyInput()
    {
        isPauseKeyPressed = Input.GetKeyDown(KeyCode.Escape); // Detect the escape key press
    }

    private void HandleSprintingInput()
    {
        if (sprintInput = sprint.IsPressed())
        {
            playerLocomotionHandler.isSprinting = true;
        }
        else
        {
            playerLocomotionHandler.isSprinting = false;
        }
    }

    private void HandleJumpInput()
    {
        jumpInput = jump.IsPressed(); // Detect jump input (spacebar)
        if (jumpInput)
        {
            playerLocomotionHandler.HandleJump(); // Trigger jump in locomotion handler
        }
    }

    //public void OnJump(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        playerLocomotionHandler.HandleJump();
    //    }
    //}





}
