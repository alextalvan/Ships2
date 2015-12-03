using UnityEngine;
using UnityEngine.Networking;

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
            hull.GetRigidBody.AddExplosionForce(explosionForce, collision.contacts[0].point, damageRadius);
            RpcSpawnWrecks(collision.contacts[0].point);
            hull.GetComponent<PlayerFX>().RpcPlaySound(PlayerFX.PLAYER_SOUNDS.EXPLOSION);
            base.DealDamage(collision);
        }
        Delete(false);
    }

    [ClientCallback]
    protected override void ProcessSplash()
    {
        Vector3 pos = transform.position;
        if (!spawnedSplash && pos.y <= WaterHelper.GetOceanHeightAt(new Vector2(pos.x, pos.z)))
        {
            spawnedSplash = true;
            GameObject splashGO = (GameObject)Instantiate(splashPrefab, pos, new Quaternion(0f, Random.rotation.y, 0f, 0f));
            splashGO.GetComponent<ParticleSystem>().startRotation = Random.Range(0, 180);
        }
    }

    [ServerCallback]
    protected override void Delete(bool underWater)
    {
        RpcExplode(transform.position);
        RpcSpawnSplash(new Vector3(transform.position.x, WaterHelper.GetOceanHeightAt(new Vector2(transform.position.x, transform.position.z)), transform.position.z), 25f);
        base.Delete(false);
    }
}
