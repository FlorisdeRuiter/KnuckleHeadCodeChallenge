using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseGun : MonoBehaviour, IUseable
{
    [SerializeField] protected int _maxAmmo = 12;
    [SerializeField] protected int _currentAmmo = 12;

    [SerializeField] protected bool _fullAuto;
    [SerializeField] protected float _timeSinceLastShot;
    [SerializeField] protected float _shotInterval;
    [SerializeField] protected bool _canShoot;

    [SerializeField] protected bool _isTryingToFire;

    protected virtual void Update()
    {
        _isTryingToFire = Mouse.current.rightButton.isPressed;
        _timeSinceLastShot += Time.deltaTime;
    }

    public void Use()
    {
        if (_canShoot || _timeSinceLastShot >= _shotInterval || _currentAmmo > 0)
        {

        }

        Shoot();
    }

    protected virtual void Shoot()
    {
        _timeSinceLastShot = 0;
        _currentAmmo -= 1;
    }
}
