using UnityEngine;


public class CannonProjectile : Projectile
{
    public float BlastRadius = 2f;
    public GameObject ExplosionEffect;

    private Rigidbody cannonRb;

    private void Awake()
    {
        cannonRb = GetComponent<Rigidbody>();
    }

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

    private void OnTriggerEnter()
    {
        Explode();
    }

    /// <summary>
    /// When the cannon ball hits an Enemy, it will find any colliders within a 2f(BlastRadius) radius.
    /// Then Destroy all Enemies in that radius
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
                Destroy(enemy.gameObject);
            }
        }
        Destroy(gameObject);
    }
}
