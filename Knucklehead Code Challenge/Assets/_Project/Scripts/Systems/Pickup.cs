using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] protected GameObject _prefab;
    public GameObject Prefab => _prefab;
    public bool IsWearable;
    public bool DestroyOnPickup;
}
