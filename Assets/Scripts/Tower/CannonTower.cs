using UnityEngine;

public class CannonTower : Tower
{
    public float LaunchVelocityY = 750f;

    [SerializeField] private GameObject projectilePrefab;
    

    protected override void Update()
    {
        base.Update();
    }

    protected override void FireAt(Enemy target)
    {
        if (projectilePrefab != null)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectileInstance.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (0, LaunchVelocityY, 0));
            projectileInstance.GetComponent<Projectile>().SetTarget(target.transform);
        }
    }

    protected override Enemy GetClosestEnemy()
    {
        DeleteEnemyInList();

        Enemy closestEnemy = null;
        float closestDistance = float.MaxValue;
        foreach (Enemy enemy in enemiesInRange)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }
}
