using UnityEngine;
using System.Collections;
using System;

public class CannonBallOFFLINE : ProjectileOFFLINE {

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

    }

    protected override void DealDamage(Collision collision)
    {
        //for online
        //if (collision.collider.GetComponent<HullOnline>())
        //    collision.collider.GetComponent<BuoyancyScript>().ChangeBuoyancy(collision.contacts[0].point, damage, damageRadius);
        //else if (collider.GetComponent<SailOnline>())
        //      collision.collider.transform.parent.GetComponent<ShipAttributesOnline>().ChangeSailHealth(-sailDamage, );

        //offline
        if (collision.collider.CompareTag("Hull"))
            collision.collider.GetComponent<BuoyancyScriptOFFLINE>().ChangeBuoyancy(collision.contacts[0].point, hullDamage, damageRadius);
        else if (collision.collider.CompareTag("Sail"))
            collision.collider.transform.parent.GetComponent<ShipAttributes>().ChangeSailHealth(-sailDamage, 0);
        else if (collision.collider.CompareTag("Mast"))
            collision.collider.CompareTag("Mast"); //todo
    }
}
