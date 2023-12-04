using UnityEngine;

public class Bullet : PoolItem
{
    [SerializeField] private float _despawnTime;
    [SerializeField] private float _timeUntilDespawn;
    [SerializeField] private float _bulletSpeed;
    private Vector3 _shootDirection;

    private void Start()
    {
         _timeUntilDespawn = _despawnTime;
    }

    private void Update()
    {
        _timeUntilDespawn -= Time.deltaTime;

        // Moves bullet in the shoot direction
        transform.position += _shootDirection * _bulletSpeed * Time.deltaTime;

        // Despawns bullet over time
        if (_timeUntilDespawn <= 0)
        {
            _timeUntilDespawn = _despawnTime;
            ReturnToPool();
        }
    }

    public void SetLaunchDirection(Vector3 direction)
    {
        _shootDirection = direction;
    }
}