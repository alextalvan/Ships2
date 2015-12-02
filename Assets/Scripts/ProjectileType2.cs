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
            RpcSpawnHit(collision.contacts[0].point);

			PlayerFX.PLAYER_SOUNDS s = (PlayerFX.PLAYER_SOUNDS)((int)(PlayerFX.PLAYER_SOUNDS.HIT1) + Random.Range (0, 2));
			hull.GetComponent<PlayerFX> ().RpcPlaySound (s);
            base.DealDamage(collision);
        }
        if (sails)
        {
            collision.gameObject.GetComponent<ShipAttributesOnline>().DamageAllSails(sailDamage);
            RpcSpawnHit(collision.contacts[0].point);
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
