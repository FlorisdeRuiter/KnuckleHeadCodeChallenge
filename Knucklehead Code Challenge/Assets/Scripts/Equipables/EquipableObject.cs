using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableObject : MonoBehaviour, IEquipable
{
    [SerializeField] private GameObject _equipedObject;

    public void Equip()
    {
        if (EquipmentManager.Instance.CanEquipObject(_equipedObject))
        {
            Destroy(gameObject);
        }
    }
}
