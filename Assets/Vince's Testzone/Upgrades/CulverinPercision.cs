using UnityEngine;
using System.Collections;
using System;

public class CulverinPercision : Upgrade {

    float reloadModifier = -1;
    float rangeModifier = 2;

    public override void ApplyUpgrade()
    {
        shipAttributes.ChangeRange(rangeModifier);
        shipAttributes.ChangeReloadRate(reloadModifier);
    }
}
