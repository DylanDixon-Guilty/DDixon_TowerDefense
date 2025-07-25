using UnityEngine;


public class CannonProjectile : Projectile
{
   

    private void Start()
    {
        
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void SetTarget(Transform inputTarget)
    {
        target = inputTarget;
    }
}
