using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ProjectileType3 : Projectile
{
    [SerializeField]
    GameObject ripples;

	[SerializeField]
	float explosionRadius;

	bool exploded = false;

    protected override void DealDamage(Collision collision)
    {
        if (collision.collider.GetComponent<ProjectileType3>())
            return;

		Delete ();
		//ForceExplosion ();
    }

    [ClientCallback]
    protected override void ProcessSplash(float size = 5f)
    {
		if (spawnedSplash)
			return;

		base.ProcessSplash (25f);
		gameObject.SetActive (true);
		ripples.SetActive (true);
    }


	public void ForceExplosion()
	{
		if (exploded)
			return;

		exploded = true;

		DoAreaDamage ();
		//Delete ();
	}

	public void ForceDelayedExplosion()
	{
		StartCoroutine (DelayedExplosion (0.1f));
	}

	IEnumerator DelayedExplosion(float delay)
	{
		yield return new WaitForSeconds (delay);
		//ForceExplosion ();
		Delete ();
	}

    /// <summary>
    /// damage area
    /// </summary>
	void DoAreaDamage()
	{
		Vector3 myPos = GetComponent<Rigidbody> ().position;
		Collider[] objects = Physics.OverlapSphere (myPos, explosionRadius);
		foreach (Collider c in objects) 
		{
			HullOnline hull = c.GetComponent<HullOnline>();
			if (hull)
			{
				hull.Damage(myPos, hullDamage, damageRadius, gameObject);
				hull.GetComponent<ShipAttributesOnline>().DamageAllSails(sailDamage);
				hull.GetRigidBody.AddExplosionForce(explosionForce, myPos, damageRadius);
				RpcSpawnWrecks(myPos);
				SpawnDebrisAt(myPos,(myPos-hull.transform.position).normalized,hull.GetComponent<CustomOnlinePlayer>());
			}

			ProjectileType3 otherProj = c.GetComponent<ProjectileType3>();
			if(otherProj)
			{
				otherProj.ForceDelayedExplosion();
			}
		}
	}

    /// <summary>
    /// destroy projectile
    /// </summary>
    [ServerCallback]
    protected override void Delete()
    {
        RpcExplode(transform.position, ImpactSoundType.EXPLOSION);
        RpcSpawnSplash(new Vector3(transform.position.x, WaterHelper.GetOceanHeightAt(new Vector2(transform.position.x, transform.position.z)), transform.position.z), 25f);
		ForceExplosion ();
        base.Delete();
    }
}
