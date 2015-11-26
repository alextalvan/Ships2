using UnityEngine;
using System.Collections;
using System;

public class ReinforcedPlanks : Upgrade {

    private float modifier = 5;

    public override void ApplyUpgrade()
    {
        shipAttributes.ChangeHealthMax(modifier);
    }
}
