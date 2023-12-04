using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerManager : MonoBehaviour
{
    private enum EInputState
    {
        OnInput,
        NoRelevantInput
    }

    private PlayerInput _input;
    [SerializeField] private Camera fpsCam;

    private void Start()
    {
        _input = FindObjectOfType<PlayerInput>();
        SetInputs();
    }

    private void Update()
    {
        SetDropState();
        SetEquipState();
        SetInteractState();
        SetEquipableObject();
        SetInteractableObject();
    }

    private void SetInputs()
    {
        _input.actions[_inputPickupAction.action.name].performed += OnPickup;
        _input.actions[_inputPickupAction.action.name].canceled += OnPickup;

        _input.actions[_inputDropAction.action.name].performed += OnDrop;
        _input.actions[_inputDropAction.action.name].canceled += OnDrop;

        _input.actions[_inputInteractAction.action.name].performed += OnInteract;
        _input.actions[_inputInteractAction.action.name].canceled += OnInteract;
    }
}
