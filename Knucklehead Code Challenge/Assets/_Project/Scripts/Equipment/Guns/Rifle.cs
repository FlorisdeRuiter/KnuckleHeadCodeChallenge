using UnityEngine;

public class Rifle : AGunAutomatic
{
    protected override void Shoot()
    {
        UnityEngine.GameObject bullet = m_bulletPrefabPool.GetPooledObject(m_firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetLaunchDirection(-transform.forward);
    }
}
