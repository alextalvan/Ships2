using UnityEngine;
using UnityEngine.Networking;

public class ProjectileType1 : Projectile
{
    protected override void DealDamage(Collision collision)
    {
        HullOnline hull = collision.collider.GetComponent<HullOnline>();
        SailOnline sails = collision.collider.GetComponent<SailOnline>();
		bool hitShip = false;

        if (hull)
        {
            hull.Damage(collision.contacts[0].point, hullDamage, damageRadius, gameObject);
            hull.GetRigidBody.AddExplosionForce(explosionForce, collision.contacts[0].point, damageRadius);
            RpcSpawnWrecks(collision.contacts[0].point);
			RpcExplode(transform.position, ImpactSoundType.SHIP_HULL);
            base.DealDamage(collision);
			hitShip = true;
        }

        if (sails)
        {
            collision.gameObject.GetComponent<ShipAttributesOnline>().DamageAllSails(sailDamage);
            sails.transform.root.gameObject.GetComponent<HullOnline>().GetRigidBody.AddExplosionForce(explosionForce/2f, collision.contacts[0].point, damageRadius);
            RpcSpawnWrecks(collision.contacts[0].point);
			hitShip = true;
        }

		if(!hitShip)
			RpcExplode(transform.position, ImpactSoundType.ROCK);

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
            //RpcExplode(transform.position);

        base.Delete(underWater);
    }
}
