using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public partial class FirstPersonController : MonoBehaviour
{
    public bool CanMove { get; set; } = true;
    private bool _isSprinting => _canSprint && _sprintButtonPressed;

    private Camera _playerCamera;                           // The first person camera
    private CharacterController _characterController;       // The player's character controller

    [Header("Movement Toggles")]
    [SerializeField] private bool _canSprint = true;        // Whether the player can sprint

    [Header("Movement Parameters")]
    [Tooltip("The speed at which the player will move while walking in m/s")]
    [SerializeField] private float _walkSpeed = 4.0f;
    [Tooltip("The speed at which the player will move while sprinting in m/s")]
    [SerializeField] private float _sprintSpeed = 8.0f;
    [Tooltip("Force at which gravity it applied")]
    [SerializeField] private float _gravityForce = -9.8f;
    [Tooltip("The amount of control the player has over the character while mid air (Not functional yet)")]
    [SerializeField, Range(0, 1)] private float _airControl;
    private float3 _moveDirection;              // Final direction the player will move in
    private float2 _currentInputDirection;      // Value given through the keyboard/controller
    private bool _sprintButtonPressed;          // Check if sprint button is being pressed

    [Header("Look Parameters")]
    [Tooltip("The speed at which the player can change it look direction on the horizontal axis")]
    [SerializeField, Range(1, 100)] private float _lookSensitivityX = 2.0f;
    public float LookSensX { get { return _lookSensitivityX; } set { _lookSensitivityX = value; } }
    [Tooltip("The speed at which the player can change it look direction on the vertical axis")]
    [SerializeField, Range(1, 100)] private float _lookSensitivityY = 2.0f;
    public float LookSensY { get { return _lookSensitivityY; } set { _lookSensitivityY = value; } }
    [Tooltip("The amount of degrees the player can look upwards to")]
    [SerializeField, Range(1, 180)] private float _upperLookLimit = 80.0f;
    [Tooltip("The amount of degrees the player can look downwards to")]
    [SerializeField, Range(1, 180)] private float _lowerLookLimit = 80.0f;
    private float2 _mouseDisplacementThisFrame; // Value given through the mouse/joystick
    private float _cameraRotationX = 0f;        // Base camera rotation to set the vision clamp from


    private void Awake()
    {
        _playerCamera = GetComponentInChildren<Camera>();
        _characterController = GetComponent<CharacterController>();
        _input = FindObjectOfType<PlayerInput>();
        SetInputs();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!CanMove)
            return;

        HandleMovementInput();
        HandleLookDirection();

        ApplyMovement();
    }

    /// <summary>
    /// Calculates the move direction from the current input and the player's rotation
    /// </summary>
    private void HandleMovementInput()
    {
        // Cache the y direction
        float moveDirectionY = _moveDirection.y;

        // Normalizes the current input so the movement has the same speed in all directions
        float2 newDirection = math.normalizesafe(_currentInputDirection) * (_isSprinting ? _sprintSpeed : _walkSpeed);

        // Sets the move direction
        if (_characterController.isGrounded)
            _moveDirection = transform.TransformDirection(new Vector3(newDirection.x, moveDirectionY, newDirection.y));
        else
            _moveDirection = transform.TransformDirection(new Vector3(newDirection.x * _airControl, moveDirectionY, newDirection.y * _airControl));
    }

    /// <summary>
    /// Calculates the rotation the camera should be in when moving the mouse
    /// </summary>
    private void HandleLookDirection()
    {
        // Caches the current mouse input
        _cameraRotationX -= _mouseDisplacementThisFrame.y * (_lookSensitivityY * Time.deltaTime);

        // Clamps the rotation between the lower and upper look limit
        _cameraRotationX = Mathf.Clamp(_cameraRotationX, -_upperLookLimit, _lowerLookLimit);

        // Applies the rotation to the first person camera
        _playerCamera.transform.localRotation = Quaternion.Euler(_cameraRotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, _mouseDisplacementThisFrame.x * (_lookSensitivityX * Time.deltaTime), 0);
    }

    /// <summary>
    /// Applies the gravity and calculated movement from HandleMovementInput
    /// </summary>
    private void ApplyMovement()
    {
        // Applies gravity
        if (!_characterController.isGrounded)
            _moveDirection.y -= _gravityForce * Time.deltaTime;

        // Moves the character controller
        _characterController.Move(_moveDirection * Time.deltaTime);
    }
}