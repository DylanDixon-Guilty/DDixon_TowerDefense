using UnityEngine;

public class CannonTower : Tower
{


    [SerializeField] private GameObject projectilePrefab;
    private Enemy enemyHealth;

    private void Awake()
    {
        enemyHealth = GetComponent<Enemy>();
    }

    protected override void Update()
    {
        base.Update();
    }

    
    protected override void FireAt(Enemy target)
    {
        if (projectilePrefab != null)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectileInstance.GetComponent<Projectile>().SetTarget(target.transform);
        }
    }

    /// <summary>
    /// When there are multiple targets, it will fire at the one with the least amount of Health
    /// </summary>
    protected override Enemy GetClosestEnemy()
    {
        DeleteEnemyInList();

        Enemy enemyHealth = null;
        float mostHealthEnemy = float.MaxValue;
        foreach (Enemy enemy in enemiesInRange)
        {
            float leastHealthEnemy = Enemy.CurrentHealth;
            if (leastHealthEnemy < mostHealthEnemy)
            {
                mostHealthEnemy = leastHealthEnemy;
                enemyHealth = enemy;
            }
        }
        return enemyHealth;
    }
}
