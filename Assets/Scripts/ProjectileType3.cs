using UnityEngine;

public class ProjectileType3 : Projectile
{
    protected override void DealDamage(Collision collision)
    {
        if (collision.collider.GetComponent<ProjectileType3>())
            return;

        HullOnline hull = collision.collider.GetComponent<HullOnline>();
        if (hull)
        {
            hull.Damage(collision.contacts[0].point, hullDamage, damageRadius, gameObject);
            hull.GetComponent<ShipAttributesOnline>().DamageAllSails(sailDamage);
            RpcSpawnHit(collision.contacts[0].point);
            base.DealDamage(collision);
        }
        Delete();
    }
}
