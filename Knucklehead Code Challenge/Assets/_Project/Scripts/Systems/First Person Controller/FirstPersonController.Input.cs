using UnityEngine;
using UnityEngine.InputSystem;

public partial class FirstPersonController //Input
{
    private PlayerInput _input;

    [Header("Input References")]
    [SerializeField] private InputActionReference _moveReference;
    [SerializeField] private InputActionReference _lookReference;
    [SerializeField] private InputActionReference _sprintReference;

    private void OnMove(InputAction.CallbackContext context)
    {
        _currentInputDirection = context.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        _mouseDisplacementThisFrame = context.ReadValue<Vector2>();
    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        _sprintButtonPressed = context.ReadValueAsButton();
    }

    /// <summary>
    /// Assigns the functions to their input in the action map
    /// </summary>
    private void SetInputs()
    {
        _input.actions[_moveReference.action.name].performed += OnMove;
        _input.actions[_moveReference.action.name].canceled += OnMove;

        _input.actions[_lookReference.action.name].performed += OnLook;
        _input.actions[_lookReference.action.name].canceled += OnLook;

        _input.actions[_sprintReference.action.name].performed += OnSprint;
        _input.actions[_sprintReference.action.name].canceled += OnSprint;
    }
}
