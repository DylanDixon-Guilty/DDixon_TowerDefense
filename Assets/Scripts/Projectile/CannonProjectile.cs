using UnityEngine;


public class CannonProjectile : Projectile
{
    private Rigidbody cannonRb;

    private void Awake()
    {
        cannonRb = GetComponent<Rigidbody>();
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
