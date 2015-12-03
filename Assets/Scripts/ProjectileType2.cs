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
            hull.GetRigidBody.AddExplosionForce(explosionForce, collision.contacts[0].point, damageRadius);
            RpcSpawnWrecks(collision.contacts[0].point);
            PlayerFX.PLAYER_SOUNDS s = (PlayerFX.PLAYER_SOUNDS)((int)(PlayerFX.PLAYER_SOUNDS.HIT1) + Random.Range(0, 2));
            hull.GetComponent<PlayerFX>().RpcPlaySound(s);
            base.DealDamage(collision);
        }
        if (sails)
        {
            collision.gameObject.GetComponent<ShipAttributesOnline>().DamageAllSails(sailDamage);
            hull.GetRigidBody.AddExplosionForce(explosionForce/2f, collision.contacts[0].point, damageRadius);
            RpcSpawnWrecks(collision.contacts[0].point);
        }
        Delete(false);
    }

    [ServerCallback]
    protected override void ProcessDeath()
    {
        if (Time.time > birthDate + lifeTime)
            Delete(false);
        else if (transform.position.y < WaterHelper.GetOceanHeightAt(new Vector2(transform.position.x, transform.position.z)))
            Delete(true);
    }

    [ServerCallback]
    protected override void Delete(bool underWater)
    {
        if (underWater)
            RpcSpawnSplash(transform.position, 5f);
        else
            RpcExplode(transform.position);

        base.Delete(underWater);
    }
}
