using UnityEngine;

public class BalistaTower : Tower
{
    [SerializeField] private GameObject projectilePrefab;

    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// Fires a projectile and sends it to the enemy
    /// </summary>
    protected override void FireAt(Enemy target)
    {
        if (projectilePrefab != null)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectileInstance.GetComponent<Projectile>().SetTarget(target.transform);
        }
    }

    /// <summary>
    /// When there are multiple enemies, it will fire at the one closest to the Tower
    /// </summary>
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
