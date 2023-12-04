using UnityEngine;

public class AmmoClip : MonoBehaviour, IEquipableOnUse, IUnequipable
{
    [SerializeField] private int _ammoAmount;

    public void Unequip()
    {
        Destroy(gameObject);
    }

    public void EquipableOnUse()
    {
        if (_ammoAmount <= 0)
            return;

        AGun gun = FindObjectOfType<AGun>();
        if (gun)
        {
            gun.Reload(_ammoAmount);
            Unequip();
        }
    }

    private void OnDestroy()
    {
        GetComponentInParent<EquipmentController>().UpdateListeners();
    }
}
