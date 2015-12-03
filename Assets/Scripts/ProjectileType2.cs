using UnityEngine;
using UnityEngine.Networking;

public class ProjectileType2 : Projectile
{
    protected override void DealDamage(Collision collision)
    {
        HullOnline hull = collision.collider.GetComponent<HullOnline>();
        SailOnline sails = collision.collider.GetComponent<SailOnline>();

        if (hull)
        {
            hull.Damage(collision.contacts[0].point, hullDamage, damageRadius, gameObject);
            RpcSpawnWrecks(collision.contacts[0].point);

			hull.GetComponent<PlayerFX> ().RpcPlaySound (PlayerFX.PLAYER_SOUNDS.HIT);
            base.DealDamage(collision);
        }
        if (sails)
        {
            collision.gameObject.GetComponent<ShipAttributesOnline>().DamageAllSails(sailDamage);
            RpcSpawnWrecks(collision.contacts[0].point);
        }
        Delete();
    }

    [ServerCallback]
    protected override void Delete()
    {
        if (Time.time < birthDate + lifeTime)
            RpcExplode(transform.position);

        base.Delete();
    }
}
