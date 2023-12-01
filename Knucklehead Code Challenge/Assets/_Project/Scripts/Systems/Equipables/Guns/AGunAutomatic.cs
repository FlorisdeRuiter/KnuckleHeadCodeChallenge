public abstract class AGunAutomatic : AGun, IEquipableUse
{
    public void EquipableUse()
    {
        if (m_CurrentShotInterval > 0 ||
            m_CurrentAmmo <= 0)
            return;

        Shoot();
        m_CurrentShotInterval = m_ShotInterval;
        m_CurrentAmmo--;
    }
}