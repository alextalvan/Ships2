using UnityEngine;
using System.Collections;
using System;

//Range Upgrade
public class AngleCorrection : Upgrade {

    private float modifier = 1;


    public override void ApplyUpgrade()
    {
        shipAttributes.ChangeRange(modifier);
    }
}
