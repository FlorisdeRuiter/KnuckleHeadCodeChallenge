using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
    public GameObject HeldObject;

    public void SetHeldObject(GameObject o, ObjectHolder h)
    {
        h.HeldObject = o;
        Instantiate(o, transform);
    }
}
