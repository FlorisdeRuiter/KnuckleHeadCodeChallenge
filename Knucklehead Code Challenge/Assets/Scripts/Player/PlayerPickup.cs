using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IEquipable o;
        if (other.TryGetComponent<IEquipable>(out o))
        {
            o.Equip();
        }
    }
}
