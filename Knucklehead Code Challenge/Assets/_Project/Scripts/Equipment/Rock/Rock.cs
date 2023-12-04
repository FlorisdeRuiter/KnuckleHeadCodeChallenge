using UnityEngine;

public class Rock : MonoBehaviour, IEquipableOnUse
{
    [SerializeField] private UnityEngine.GameObject _rock;
    [SerializeField] private float _throwSpeed;

    public void EquipableOnUse()
    {
        UnityEngine.GameObject rock = Instantiate(_rock, transform.position, Quaternion.identity);
        rock.GetComponent<BoxCollider>().enabled = false;
        rock.GetComponent<Rigidbody>().velocity = transform.forward * _throwSpeed;
        Destroy(gameObject);
    }
}
