using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerManager : MonoBehaviour //Equipment
{
    [Header("Equipment controllers")]
    [SerializeField] private Transform _rightHandRoot;
    [SerializeField] private Transform _leftHandRoot;
    [SerializeField] private Transform _headRoot;

    [SerializeField] private float _pickupDistance;
    private Pickup _sensedEquipableObject;

    [SerializeField] private InputActionReference _inputPickupAction;
    [SerializeField] private InputActionReference _inputDropAction;
    private EInputState _equipInputState;
    private EInputState _dropInputState;

    private void EquipItem()
    {
        if (_sensedEquipableObject == null)
            return;

        if (_sensedEquipableObject.IsWearable)
        {
            TryWearItem(_sensedEquipableObject.Prefab);
        }
        else
        {
            TryEquipItem(_sensedEquipableObject.Prefab);
        }

        if (_sensedEquipableObject.DestroyOnPickup)
        {
            Destroy(_sensedEquipableObject.gameObject);
        }
    }

    private void DropItems(Transform parent)
    {
        if (parent.childCount > 0)
        {
            Destroy(parent.GetChild(0).gameObject);
            parent.GetComponent<EquipmentController>().UpdateListeners();
        }
    }

    private void SetEquipState()
    {
        switch (_equipInputState)
        {
            case EInputState.OnInput:
                EquipItem();
                _equipInputState = EInputState.NoRelevantInput;
                break;

            case EInputState.NoRelevantInput:
                break;

            default:
                break;
        }
    }

    private void SetDropState()
    {
        switch (_dropInputState)
        {
            case EInputState.OnInput:
                DropItems(_rightHandRoot);
                DropItems(_leftHandRoot);
                DropItems(_headRoot);
                _dropInputState = EInputState.NoRelevantInput;
                break;

            case EInputState.NoRelevantInput:
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Uses a raycast from the main camera to detect objects at the mouse position.
    /// If an object is within the specified pickup distance, it sets the sensed equipable object.
    /// </summary>
    private void SetEquipableObject()
    {
        Ray ray = fpsCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _pickupDistance))
        {
            Pickup obj = hit.collider.GetComponent<Pickup>();
            if (obj)
                _sensedEquipableObject = obj;
            else
                _sensedEquipableObject = null;
        }
        else
        {
            _sensedEquipableObject = null;
        }
    }

    /// <summary>
    /// Checks to see if left or right hand are still available.
    /// Gives priority to right hand.
    /// </summary>
    /// <param name="prefab">Object you're trying to equip</param>
    private bool TryEquipItem(GameObject prefab)
    {
        return (TryEquip(_rightHandRoot, prefab) || TryEquip(_leftHandRoot, prefab));
    }

    /// <summary>
    /// Checks to see if head is still available.
    /// </summary>
    /// <param name="prefab">Object you're trying to wear</param>
    private bool TryWearItem(GameObject prefab)
    {
        return (TryEquip(_headRoot, prefab));
    }

    /// <summary>
    /// Attempts to equip an equipable item to one of the provided transform if no children exist already.
    /// </summary>
    /// <param name="parent">The Transform representing the parent where the object will be attached.</param>
    /// <param name="prefab">The GameObject prefab to be instantiated as the equipment.</param>
    /// <returns>
    /// True if the equipment process is successful (no existing child in the parent), 
    /// false otherwise (parent already has a child).
    /// </returns>
    private bool TryEquip(Transform parent, UnityEngine.GameObject prefab)
    {
        if (parent.childCount > 0) return false;
        GameObject obj = Instantiate(prefab, parent);

        IEquipable equipable = obj.GetComponentInChildren<IEquipable>();
        if (equipable != null) equipable.Equip();
        parent.GetComponent<EquipmentController>().UpdateListeners();
        return true;
    }

    private void OnPickup(InputAction.CallbackContext context)
    {
        _equipInputState = context.ReadValueAsButton() ? EInputState.OnInput : EInputState.NoRelevantInput;
    }

    private void OnDrop(InputAction.CallbackContext context)
    {
        _dropInputState = context.ReadValueAsButton() ? EInputState.OnInput : EInputState.NoRelevantInput;
    }
}
