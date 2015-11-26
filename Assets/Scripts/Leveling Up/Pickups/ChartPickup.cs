using UnityEngine;
using System.Collections;

public class ChartPickup : Pickup 
{

	protected override void OnPickup (CustomOnlinePlayer player)
	{
		base.OnPickup (player);
		player.mapPieces++;
	}
}
