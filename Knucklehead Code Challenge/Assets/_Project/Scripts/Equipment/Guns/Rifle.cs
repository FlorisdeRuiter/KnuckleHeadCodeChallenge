using UnityEngine;

public class Rifle : AGunAutomatic
{
    protected override void Shoot()
    {
        GameObject bullet = Instantiate(m_bulletPrefab, m_firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().LaunchDirection(-transform.forward);
    }
}
