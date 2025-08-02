using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public abstract class Tower : MonoBehaviour
{
    public float FireCooldown = 1.0f;
    public Transform TowerBase;

    protected float currentFireCooldown = 1.0f;
    protected List<Enemy> enemiesInRange = new List<Enemy>();


    protected virtual void Update()
    {
        currentFireCooldown -= Time.deltaTime;
        Enemy closestEnemy = GetClosestEnemy();
        if (closestEnemy != null && currentFireCooldown <= 0.0f)
        {
            FireAt(closestEnemy);
            currentFireCooldown = FireCooldown;
        }
    }

    /// <summary>
    /// Used to Instantiate an ProjectilePrefab and send it to the corresponding Enemy.
    /// In both the Balista and Cannon Tower Scripts
    /// </summary>
    protected abstract void FireAt(Enemy target);

    /// <summary>
    /// When there are multiple Enemies within range of a Tower, target the closest enemy to the Tower.
    /// Cannon Tower Script instead finds the Enemy with the least amount of Health
    /// </summary>
    protected abstract Enemy GetClosestEnemy();

    /// <summary>
    /// When an Enemy is Destroyed, remove it from the list of Enemies currently in the TowerScript
    /// </summary>
    protected void DeleteEnemyInList()
    {
        for (int i = enemiesInRange.Count - 1; i >= 0; i--)
        {
            if (enemiesInRange[i] == null)
            {
                enemiesInRange.RemoveAt(i);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && !enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Remove(enemy);
        }
    }
}
