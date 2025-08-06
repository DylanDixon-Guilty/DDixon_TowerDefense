using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FreezeTower : Tower
{
    public float BlastRadius = 5f;
    public float SetSpeed = 1f;

    [SerializeField] private GameObject freezeParticlePrefab;
    [SerializeField] private float effectLifeTime;

    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// Fires a freeze particle effect that then find any enemy colliders within the BlastRadius and reduces all enemies' speed
    /// </summary>
    protected override void FireAt(Enemy target)
    {
        if (freezeParticlePrefab != null && IsTowerPlaced)
        {
            GameObject particleInstance = Instantiate(freezeParticlePrefab, FiringPoint.position, Quaternion.identity);
            Collider[] enemyColliders = Physics.OverlapSphere(transform.position, BlastRadius);
            foreach (Collider nearbyEnemies in enemyColliders)
            {
                Enemy enemy = nearbyEnemies.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.GetComponent<NavMeshAgent>().speed = SetSpeed;
                }
            }
            StartCoroutine(DespawnProjectile(particleInstance));
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

    IEnumerator DespawnProjectile(GameObject particlePrefab)
    {
        yield return new WaitForSeconds(effectLifeTime);
        Enemy enemy = GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.GetComponent<NavMeshAgent>().speed = enemy.ResetSpeed;
        }
        Destroy(particlePrefab);
    }
}
