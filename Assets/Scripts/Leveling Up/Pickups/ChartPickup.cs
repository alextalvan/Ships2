using UnityEngine;
using System.Collections;

public class ChartPickup : Pickup 
{
    protected override void OnPickup(CustomOnlinePlayer player)
    {
        base.OnPickup(player);
        player.MapPieces++;
    }

 //   protected override void OnPickup (CustomOnlinePlayer player, string part)
	//{
	//	base.OnPickup (player, part);
	//	player.MapPieces++;
	//}
}
