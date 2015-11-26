using UnityEngine;

public class ProjectileType2 : Projectile
{
    protected override void DealDamage(Collision collision)
    {
        HullOnline hull = collision.gameObject.GetComponent<HullOnline>();
        SailOnline sails = collision.gameObject.GetComponent<SailOnline>();
        if (hull)
        {
            hull.Damage(collision.contacts[0].point, hullDamage, damageRadius);
        }
        if (sails)
        {
            sails.Damage(sailDamage);
        }
        Delete();
    }
}
