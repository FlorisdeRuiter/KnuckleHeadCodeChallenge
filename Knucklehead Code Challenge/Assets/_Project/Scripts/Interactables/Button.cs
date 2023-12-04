using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent _onClick;

    public void Interact()
    {
        _onClick?.Invoke();
    }
}
