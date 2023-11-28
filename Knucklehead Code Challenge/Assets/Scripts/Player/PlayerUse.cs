using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerUse : MonoBehaviour
{
    [SerializeField] private bool _usePressed;
    public GameObject UseableObject;
    public IUseable UseableInterface;

    private void Start()
    {
        _input = FindObjectOfType<PlayerInput>();
        SetInputs();
    }

    private void Update()
    {
        if (UseableInterface == null && UseableObject != null)
        {
            UseableInterface = UseableObject.GetComponent<IUseable>();
        }

        if (_usePressed && UseableInterface != null)
        {
            UseableInterface.Use();
        }
    }
}
