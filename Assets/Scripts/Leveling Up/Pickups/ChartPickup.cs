using UnityEngine;
using System.Collections;

public class ChartPickup : Pickup 
{
    protected override void OnPickup(CustomOnlinePlayer player)
    {
        base.OnPickup(player);
        player.MapPieces++;
		player.GetComponent<PlayerFX> ().RpcPlaySoundForMainPlayer (PlayerFX.PLAYER_SOUNDS.PICKUP_SCROLL,false);
    }
}
