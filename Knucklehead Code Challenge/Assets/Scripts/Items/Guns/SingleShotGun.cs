using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SingleShotGun : BaseGun
{
    protected override void Update()
    {
        base.Update();

        if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            _canShoot = true;
        }
    }

    protected override void Shoot()
    {
        base.Shoot();

        _canShoot = false;
    }
}
