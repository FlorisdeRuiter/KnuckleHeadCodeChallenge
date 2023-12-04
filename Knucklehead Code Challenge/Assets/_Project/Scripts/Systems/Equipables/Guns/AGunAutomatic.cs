using UnityEngine;

public abstract class AGunAutomatic : AGun, IEquipableUse
{
    public void EquipableUse()
    {
        if (m_currentShotInterval > 0 || m_currentAmmo <= 0)
            return;

        Shoot();
        m_currentShotInterval = m_shotInterval;
        m_currentAmmo--;
    }
}