using UnityEngine;

public abstract class AGun : MonoBehaviour, IEquipable
{
    [Header("Gun Stats")]
    [SerializeField] protected int m_maxAmmo;
    public int MaxAmmo => m_maxAmmo;
    protected int m_currentAmmo;
    public int CurrentAmmo => m_currentAmmo;

    [SerializeField] protected float m_shotInterval;
    protected float m_currentShotInterval;

    [SerializeField] protected Transform m_firePoint;
    protected ObjectPool m_bulletPrefabPool;

    private void Start()
    {
        m_bulletPrefabPool = FindObjectOfType<ObjectPool>();
        m_bulletPrefabPool.PoolSize = m_maxAmmo;
    }

    protected void Update()
    {
        if (m_currentShotInterval > 0)
            m_currentShotInterval -= Time.deltaTime;
    }

    public void Equip()
    {
        m_currentAmmo = m_maxAmmo;
        m_currentShotInterval = 0;
    }

    /// <summary>
    /// Adds a certain amount of ammo to the gun, returning how many bullets are left in the clip
    /// </summary>
    public virtual void Reload(float pAmmo)
    {
        m_currentAmmo = Mathf.RoundToInt(Mathf.Min(m_currentAmmo + pAmmo, m_maxAmmo));
    }

    protected abstract void Shoot();
}
