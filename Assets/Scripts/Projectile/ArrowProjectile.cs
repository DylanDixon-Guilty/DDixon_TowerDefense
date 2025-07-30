using UnityEngine;

public class ArrowProjectile : Projectile
{

    
    protected override void Update()
    {
        base.Update();

    }

    /// <summary>
    /// Set to find the Transform of the Enemy's position
    /// </summary>
    public override void SetTarget(Transform inputTarget)
    {
        target = inputTarget;
    }
}
