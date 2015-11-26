using UnityEngine;
using System.Collections;
using System;

public class PowderMonkey : Upgrade {

    public override void ApplyUpgrade()
    {
        shipAttributes.ChangeReloadRate(1);
    }
}
