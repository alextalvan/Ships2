using UnityEngine;

public class ProjectileType1 : Projectile
{
    protected override void DealDamage(Collision collision)
    {
        HullOnline hull = collision.gameObject.GetComponent<HullOnline>();
        SailOnline sails = collision.gameObject.GetComponent<SailOnline>();
        if (hull)
        {
            hull.Damage(collision.contacts[0].point, hullDamage, damageRadius);
        }
        else if (sails)
        {
            sails.Damage(sailDamage);
        } 
        Delete();
    }
}
