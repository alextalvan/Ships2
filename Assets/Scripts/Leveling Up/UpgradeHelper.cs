using UnityEngine;
using System.Collections;

public class UpgradeHelper  {

	public static void ApplyUpgrade(LevelUser.UPGRADES up, ShipAttributesOnline target)
	{
		switch (up) 
		{
		case LevelUser.UPGRADES.ANGLE_CORRECTION:
			//TODO:fill these

			break;

		case LevelUser.UPGRADES.CRAZY_GUNNERS:
			break;

		case LevelUser.UPGRADES.DEFENCE_EXPERT:
			break;

		case LevelUser.UPGRADES.POWDER_MONKEY:
			break;

		case LevelUser.UPGRADES.PRECISION:
			break;

		case LevelUser.UPGRADES.REINFORCED_PLANKS:
			break;
		}

		target.GetComponent<PlayerCaptionController>().RpcPushCaption("You have been upgraded with: " + up.ToString(),4f);
	}
	
}
