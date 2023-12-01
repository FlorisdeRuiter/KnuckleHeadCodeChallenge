using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _despawnTime;
    [SerializeField] private float _bulletSpeed;
    private Vector3 _launchDirection;

    private void Update()
    {
        _despawnTime -= Time.deltaTime;

        transform.position += _launchDirection * _bulletSpeed * Time.deltaTime;

        if (_despawnTime > 0)
            return;

        Destroy(gameObject);
    }

    public void LaunchDirection(Vector3 direction)
    {
        _launchDirection = direction;
    }
}