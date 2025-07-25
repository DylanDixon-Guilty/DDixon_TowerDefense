using UnityEngine;

public class ArrowProjectile : Projectile
{

    
    protected override void Update()
    {
        base.Update();
    }

    public override void SetTarget(Transform inputTarget)
    {
        target = inputTarget;
    }
}
