using UnityEngine;

public class ArrowProjectile : Projectile
{

    
    protected override void Update()
    {
        base.Update();

        lifeTime -= Time.deltaTime;
        if (lifeTime == 0)
        {
            Destroy(gameObject);
        }
    }

    public override void SetTarget(Transform inputTarget)
    {
        target = inputTarget;
    }
}
