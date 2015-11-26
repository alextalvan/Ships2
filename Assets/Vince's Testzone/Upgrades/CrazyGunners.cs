using UnityEngine;
using System.Collections;
using System;

public class CrazyGunners : Upgrade {

    float reloadModifier = 2;
    float healthModifier = -5;



    public override void ApplyUpgrade()
    {
        shipAttributes.ChangeReloadRate(reloadModifier);
        shipAttributes.ChangeHealthMax(healthModifier);

    }
}
