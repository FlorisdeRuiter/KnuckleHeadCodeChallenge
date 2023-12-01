using UnityEngine;

public partial class PlayerManager //Equipment
{
    [SerializeField] private Transform _rightHandRoot;
    [SerializeField] private Transform _leftHandRoot;

    private void OnTriggerEnter(Collider other)
    {
        Pickup pickup = other.GetComponentInChildren<Pickup>();
        if (pickup != null) TryEquipItem(pickup.Prefab);
    }

    private bool TryEquipItem(GameObject prefab)
    {
        return (TryEquip(_rightHandRoot, prefab) || TryEquip(_leftHandRoot, prefab));
    }

    /// <summary>
    /// Attempts to equip an equipable item to one of the provided transform if no children exist already.
    /// </summary>
    /// <param name="parent">The Transform representing the parent where the object will be attached.</param>
    /// <param name="prefab">The GameObject prefab to be instantiated as the equipment.</param>
    /// <returns>
    /// True if the equipment process is successful (no existing child in the parent), 
    /// false otherwise (parent already has a child).
    /// </returns>
    private bool TryEquip(Transform parent, GameObject prefab)
    {
        if (parent.childCount > 0) return false;
        GameObject obj = Instantiate(prefab, parent);

        IEquipable equipable = obj.GetComponentInChildren<IEquipable>();
        if (equipable != null) equipable.Equip();
        parent.GetComponent<EquipmentController>().UpdateListeners();
        return true;
    }
}
