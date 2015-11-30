using UnityEngine;

public class ProjectileType1 : Projectile
{
    protected override void DealDamage(Collision collision)
    {
        HullOnline hull = collision.collider.GetComponent<HullOnline>();
        SailOnline sails = collision.collider.GetComponent<SailOnline>();
        if (hull)
        {
            hull.Damage(collision.contacts[0].point, hullDamage, damageRadius, this.gameObject);
            RpcSpawnHit(collision.contacts[0].point);
        }
        if (sails)
        {
            sails.Damage(sailDamage);
            RpcSpawnHit(collision.contacts[0].point);
        }
        Delete();
        base.DealDamage(collision);
    }
}
