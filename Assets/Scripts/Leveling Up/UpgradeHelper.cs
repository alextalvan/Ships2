using UnityEngine;
using System.Collections;

public class UpgradeHelper  {

	public static void ApplyUpgrade(LevelUser.UPGRADES up, ShipAttributesOnline target)
	{
		switch (up) 
		{
		case LevelUser.UPGRADES.ANGLE_CORRECTION:
			target.RangeMultiplier += 0.1f;
			break;

		case LevelUser.UPGRADES.CRAZY_GUNNERS:
			target.DamageModifier +=0.1f;
			target.RangeMultiplier += 0.1f;
			target.HullMaxHealth -= 25f;
			break;

		case LevelUser.UPGRADES.DEFENCE_EXPERT:
			target.HullMaxHealth += 50f;
			target.RangeMultiplier -=0.1f;

			break;

		case LevelUser.UPGRADES.POWDER_MONKEY:
			target.ReloadRateModifier += 0.1f;
			break;

		case LevelUser.UPGRADES.PRECISION:
			target.RangeMultiplier +=0.2f;
			target.ReloadRateModifier -= 0.1f;
			break;

		case LevelUser.UPGRADES.REINFORCED_PLANKS:
			target.HullMaxHealth += 25f;
			break;
		}

		target.GetComponent<PlayerCaptionController>().RpcPushCaption("You have been upgraded with: " + up.ToString(),4f);
	}
}
