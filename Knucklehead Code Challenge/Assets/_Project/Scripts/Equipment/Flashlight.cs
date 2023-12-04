using UnityEngine;

public class Flashlight : MonoBehaviour, IEquipableOnUse, IEquipable
{
    private Light _light;

    public void Equip()
    {
        _light = GetComponentInChildren<Light>();
        _light.enabled = false;
    }

    public void EquipableOnUse()
    {
        // Toggles light
        _light.enabled = !_light.enabled;
    }
}
