using UnityEngine;
using UnityEngine.InputSystem;

public partial class EquipmentController : MonoBehaviour
{
    private enum EUseState
    {
        OnIdle,
        Idle,
        OnUse,
        Use
    }

    private EUseState _state;

    private IEquipableIdle _idleListener;
    private IEquipableleOnIdle _onIdleListener;
    private IEquipableUse _useListener;
    private IEquipableOnUse _onUseListener;


    private PlayerInput _input;

    [SerializeField] private InputActionReference _inputUseAction;

    private void Start()
    {
        _input = FindObjectOfType<PlayerInput>();
        SetInputs();
    }

    public void UpdateListeners()
    {
        _idleListener = GetComponentInChildren<IEquipableIdle>();
        _onIdleListener = GetComponentInChildren<IEquipableleOnIdle>();
        _useListener = GetComponentInChildren<IEquipableUse>();
        _onUseListener = GetComponentInChildren<IEquipableOnUse>();
    }

    private void Update()
    {
        switch (_state)
        {
            case EUseState.OnIdle:
                _onIdleListener?.EquipableOnIdle();
                _idleListener?.EquipableIdle();
                _state = EUseState.Idle;
                break;

            case EUseState.Idle:
                _idleListener?.EquipableIdle();
                break;

            case EUseState.OnUse:
                _onUseListener?.EquipableOnUse();
                _useListener?.EquipableUse();
                _state = EUseState.Use;
                break;

            case EUseState.Use:
                _useListener?.EquipableUse();
                break;

            default:
                throw new System.ArgumentOutOfRangeException();
        }
    }

    private void OnUse(InputAction.CallbackContext context)
    {
        _state = context.ReadValueAsButton() ? EUseState.OnUse : EUseState.OnIdle;

    }

    /// <summary>
    /// Assigns the functions to their input in the action map
    /// </summary>
    private void SetInputs()
    {
        _input.actions[_inputUseAction.action.name].performed += OnUse;
        _input.actions[_inputUseAction.action.name].canceled += OnUse;
    }
}
