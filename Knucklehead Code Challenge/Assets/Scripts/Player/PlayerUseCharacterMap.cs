using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerUse
{
    private PlayerInput _input;

    [Header("Input References")]
    [SerializeField] private InputActionReference _useReference;

    private void OnUse(InputAction.CallbackContext context)
    {
        _usePressed = context.ReadValueAsButton();
    }

    /// <summary>
    /// Assigns the functions to their input in the action map
    /// </summary>
    private void SetInputs()
    {
        _input.actions[_useReference.action.name].performed += OnUse;
        _input.actions[_useReference.action.name].canceled += OnUse;
    }
}
