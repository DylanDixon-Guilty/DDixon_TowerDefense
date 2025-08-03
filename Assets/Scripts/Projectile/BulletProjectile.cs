using UnityEngine;

public class BulletProjectile : Projectile
{
    [SerializeField] private int bulletDamage = 25;

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

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.EnemyTakeDamage(bulletDamage);
            }
        }
        Destroy(gameObject);
    }
}
