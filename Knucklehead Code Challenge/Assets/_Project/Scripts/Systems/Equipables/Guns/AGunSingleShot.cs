public abstract class AGunSingleShot : AGun, IEquipableOnUse
{
    public void EquipableOnUse()
    {
        // Prevents shooting if gun is out of ammo or interval has passed yet
        if (m_currentShotInterval > 0 || m_currentAmmo <= 0)
            return;

        Shoot();
        m_currentShotInterval = m_shotInterval;
        m_currentAmmo--;
    }
}
