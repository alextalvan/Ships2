using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SailOnline : MonoBehaviour
{
    [SerializeField]
    ShipAttributesOnline shipAttributesOnline;

    public ShipAttributesOnline GetShipAttributes
    {
        get { return shipAttributesOnline; }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update 
    void FixedUpdate()
    {

    }
}
