using UnityEngine;
using System.Collections;

public abstract class Upgrade : MonoBehaviour {
    protected ShipAttributes shipAttributes;
    void Start()
    {
        ShipAttributes shipAttributes = GetComponentInParent<ShipAttributes>();
    }
    public abstract void ApplyUpgrade();
        
    
}
