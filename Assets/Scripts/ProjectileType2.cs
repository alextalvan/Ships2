using UnityEngine;
using UnityEngine.Networking;

public class ProjectileType2 : Projectile
{

    /// <summary>
    /// damage corresponding ship component
    /// </summary>
    /// <param name="collision"></param>
    protected override void DealDamage(Collision collision)
	{
		HullOnline hull = collision.collider.GetComponent<HullOnline>();
		SailOnline sails = collision.collider.GetComponent<SailOnline>();
		bool hitShip = false;
		
		if (hull)
		{
			hull.Damage(collision.contacts[0].point, hullDamage, damageRadius, gameObject);
			hull.RB.AddExplosionForce(explosionForce, collision.contacts[0].point, damageRadius);
			RpcSpawnWrecks(collision.contacts[0].point);
			RpcExplode(transform.position, ImpactSoundType.SHIP_HULL);
			base.DealDamage(collision);
			hitShip = true;
		}
		
		if (sails)
		{
			collision.gameObject.GetComponent<ShipAttributesOnline>().DamageAllSails(sailDamage);
			sails.transform.root.gameObject.GetComponent<HullOnline>().RB.AddExplosionForce(explosionForce/2f, collision.contacts[0].point, damageRadius);
			RpcSpawnWrecks(collision.contacts[0].point);
			hitShip = true;
		}
		
		if(!hitShip && transform.position.y >= WaterHelper.GetOceanHeightAt (new Vector2 (transform.position.x, transform.position.z)))
			RpcExplode(transform.position, ImpactSoundType.ROCK);
		
		Delete();
	}

	protected override void OnCollisionEnter (Collision collision)
	{
		base.OnCollisionEnter (collision);
		HandleClientCollision ();
	}
	
	[ClientCallback]
	void HandleClientCollision()
	{
		this.gameObject.SetActive (false);
	}

}
