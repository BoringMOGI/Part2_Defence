using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Bullet
{
    protected override void HitTarget()
    {
        target.OnDamaged(power);
        CreateVFX();
    }
}
