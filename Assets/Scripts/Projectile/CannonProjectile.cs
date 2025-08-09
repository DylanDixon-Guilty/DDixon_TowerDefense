using UnityEngine;


public class CannonProjectile : Projectile
{
    public float BlastRadius = 2f;
    public GameObject ExplosionEffect;

    [SerializeField] private int cannonBallDamage;


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
        Explode();
    }

    /// <summary>
    /// When the cannon ball hits an Enemy, it will find any colliders within a 2f(BlastRadius) radius.
    /// Then damage all Enemies in that radius
    /// </summary>
    protected void Explode()
    {
        Instantiate(ExplosionEffect, transform.position, transform.rotation);
        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, BlastRadius);
        foreach (Collider nearbyEnemies in enemyColliders)
        {
            Enemy enemy = nearbyEnemies.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.EnemyTakeDamage(cannonBallDamage);
            }
        }
        Destroy(gameObject);
    }
}
