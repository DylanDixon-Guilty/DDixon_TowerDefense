using UnityEngine;

public class CannonTower : Tower
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
    /// When there are multiple enemies, it will fire at the one with the least amount of Health
    /// </summary>
    protected override Enemy GetClosestEnemy()
    {
        DeleteEnemyInList();

        Enemy enemyHealth = null;
        float mostHealthEnemy = float.MaxValue;
        foreach (Enemy enemy in enemiesInRange)
        {
            float leastHealthEnemy = enemy.CurrentHealth;
            if (leastHealthEnemy < mostHealthEnemy)
            {
                mostHealthEnemy = leastHealthEnemy;
                enemyHealth = enemy;
            }
        }
        return enemyHealth;
    }
}
