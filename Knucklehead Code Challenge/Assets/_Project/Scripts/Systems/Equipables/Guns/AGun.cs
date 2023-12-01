using UnityEngine;

public abstract class AGun : MonoBehaviour, IEquipable
{
    [SerializeField] protected int m_MaxAmmo;
    public int MaxAmmo => m_MaxAmmo;
    protected int m_CurrentAmmo;
    public int CurrentAmmo => m_CurrentAmmo;

    [SerializeField] protected float m_ShotInterval;
    protected float m_CurrentShotInterval;

    [SerializeField] protected GameObject m_bulletPrefab;
    [SerializeField] protected Transform m_firePoint;

    public void Equip()
    {
        m_CurrentAmmo = m_MaxAmmo;
        m_CurrentShotInterval = 0;
    }

    /// <summary>
    /// Adds a certain amount of ammo to the gun, returning how many bullets are left in the clip
    /// </summary>
    public virtual int Reload(float pAmmo)
    {
        m_CurrentAmmo = Mathf.RoundToInt(Mathf.Min(m_CurrentAmmo + pAmmo, m_MaxAmmo));
        return Mathf.RoundToInt(Mathf.Max(0, -((m_CurrentAmmo + pAmmo) - m_MaxAmmo)));
    }

    protected abstract void Shoot();

    protected void Update()
    {
        if (m_CurrentShotInterval > 0)
            m_CurrentShotInterval -= Time.deltaTime;
    }
}
