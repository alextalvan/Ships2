using UnityEngine;

public class ProjectileType2 : Projectile
{
    protected override void DealDamage(Collision collision)
    {
        HullOnline hull = collision.gameObject.GetComponent<HullOnline>();
        SailOnline sails = collision.gameObject.GetComponent<SailOnline>();
        if (hull)
        {
            //hull.Damage(owner.gameObject.GetComponent<ShipAttributesOnline>().damage);
            collision.gameObject.GetComponent<BuoyancyScript>().ChangeBuoyancy(collision.contacts[0].point, hullDamage, damageRadius);
        }
        else if (sails)
        {
            //todo + masts
        }
        Delete();
    }
}
