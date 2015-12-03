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
            RpcSpawnWrecks(collision.contacts[0].point);
            hull.GetComponent<PlayerFX>().RpcPlaySound(PlayerFX.PLAYER_SOUNDS.EXPLOSION);
            base.DealDamage(collision);
        }
        Delete();
    }

    [ServerCallback]
    protected override void Delete()
    {
        RpcExplode(transform.position);
        RpcSpawnSplash(new Vector3(transform.position.x, WaterHelper.GetOceanHeightAt(new Vector2(transform.position.x, transform.position.z)), transform.position.z), 25f);
        base.Delete();
    }
}
