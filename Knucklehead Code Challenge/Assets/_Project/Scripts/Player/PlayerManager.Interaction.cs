using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerManager : MonoBehaviour //Interaction
{
    [SerializeField] private GameObject _sensedInteractableObject;

    [SerializeField] private InputActionReference _inputInteractAction;
    private EInputState _interactInputState;

    private void Interact()
    {
        if (_sensedInteractableObject != null)
        {
            IInteractable interactable;
            if (_sensedInteractableObject.TryGetComponent<IInteractable>(out interactable))
            {
                interactable.Interact();
            }
        }
    }

    private void SetInteractState()
    {
        switch (_interactInputState)
        {
            case EInputState.OnInput:
                Interact();
                _interactInputState = EInputState.NoRelevantInput;
                break;

            case EInputState.NoRelevantInput:
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Uses a raycast from the main camera to detect objects at the mouse position.
    /// If an object is within the specified pickup distance, it sets the sensed interactable object.
    /// </summary>
    private void SetInteractableObject()
    {
        Ray ray = fpsCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _pickupDistance))
        {
            GameObject obj = hit.collider.gameObject;

            if (obj)
                _sensedInteractableObject = obj;
            else
                _sensedInteractableObject = null;
        }
        else
        {
            _sensedInteractableObject = null;
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        _interactInputState = context.ReadValueAsButton() ? EInputState.OnInput : EInputState.NoRelevantInput;
    }
}
