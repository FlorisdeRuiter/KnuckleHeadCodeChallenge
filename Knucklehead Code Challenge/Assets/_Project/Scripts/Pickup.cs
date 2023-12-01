using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    public GameObject Prefab => _prefab;
}
