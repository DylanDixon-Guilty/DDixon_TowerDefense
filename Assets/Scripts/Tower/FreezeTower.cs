using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FreezeTower : Tower
{
    [SerializeField] private GameObject freezeParticlePrefab;
    [SerializeField] private float effectLifeTime;
    [SerializeField] private float FreezeBlastRadius;
    [SerializeField] private float UnFreezeBlastRadius;
    [SerializeField] private float ReduceSpeed;

    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// Fires a freeze particle effect that then finds any enemy colliders within the BlastRadius and reduces all enemies' speed
    /// </summary>
    protected override void FireAt(Enemy target)
    {
        if(freezeParticlePrefab != null && IsTowerPlaced)
        {
            GameObject particleInstance = Instantiate(freezeParticlePrefab, FiringPoint.position, Quaternion.identity);
            Collider[] enemyColliders = Physics.OverlapSphere(FiringPoint.position, FreezeBlastRadius);
            foreach (Collider nearbyEnemies in enemyColliders)
            {
                Enemy enemy = nearbyEnemies.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.GetComponent<NavMeshAgent>().speed -= ReduceSpeed;
                }
            }
            StartCoroutine(ResetEnemySpeed(particleInstance));
        }
    }

    /// <summary>
    /// When there are multiple enemies, it will fire at the one closest to the Tower
    /// </summary>
    protected override Enemy GetTargetEnemy()
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

    /// <summary>
    /// After effectLifeTime ends, reset the enemy's speed and destroy the particlePrefab
    /// </summary>
    IEnumerator ResetEnemySpeed(GameObject particlePrefab)
    {
        yield return new WaitForSeconds(effectLifeTime);
        Collider[] enemyColliders = Physics.OverlapSphere(FiringPoint.position, UnFreezeBlastRadius);
        foreach (Collider nearbyEnemies in enemyColliders)
        {
            Enemy enemy = nearbyEnemies.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.GetComponent<NavMeshAgent>().speed = enemy.ResetSpeed;
            }
            Destroy(particlePrefab);
        }
    }
}
