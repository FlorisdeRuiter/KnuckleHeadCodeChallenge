using UnityEngine;

public class RockThrowBehaviour : MonoBehaviour
{
    [SerializeField] private float _unequipableTime;
    private float _timeUntilEquipable;

    [SerializeField] private BoxCollider _pickupCollider;

    private void OnEnable()
    {
        _timeUntilEquipable = _unequipableTime;
        _pickupCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        _timeUntilEquipable -= Time.deltaTime;

        if (_timeUntilEquipable > 0 || _pickupCollider.enabled)
            return;

        _pickupCollider.enabled = true;
    }
}
