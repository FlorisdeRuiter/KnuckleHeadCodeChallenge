using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance { get; private set; }

    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightHand;

    private void Start()
    {
        Instance = this;
    }

    public bool CanEquipObject(GameObject o)
    {
        if (_rightHand.childCount <=0)
        {
            Instantiate(o, _rightHand);
            _rightHand.GetComponent<PlayerUse>().UseableObject = o;
            return true;
        }
        else if (_leftHand.childCount <= 0)
        {
            Instantiate(o, _leftHand);
            _leftHand.GetComponent<PlayerUse>().UseableObject = o;
            return true;
        }
        else
        {
            return false;
        }
    }
}
