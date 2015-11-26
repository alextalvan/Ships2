using UnityEngine;
using System.Collections;
using System;

public class DefenceExpert : Upgrade {

    float healthModifier = 10;
    float rangeModifier = -1;

    public override void ApplyUpgrade()
    {
        shipAttributes.ChangeHealthMax(healthModifier);
        shipAttributes.ChangeRange(rangeModifier);
    }
}
