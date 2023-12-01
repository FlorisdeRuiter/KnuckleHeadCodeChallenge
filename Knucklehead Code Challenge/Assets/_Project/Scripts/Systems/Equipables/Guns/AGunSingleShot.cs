public abstract class AGunSingleShot : AGun, IEquipableOnUse
{
    public void EquipableOnUse()
    {
        if (m_CurrentShotInterval > 0 ||
            m_CurrentAmmo <= 0)
            return;

        Shoot();
        m_CurrentShotInterval = m_ShotInterval;
        m_CurrentAmmo--;
    }
}
