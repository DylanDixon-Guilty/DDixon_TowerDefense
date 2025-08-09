using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public abstract class Tower : MonoBehaviour
{
    public bool IsTowerPlaced = false;
    public float FireCooldown = 1.0f;
    public int TowerCost; //The cost of Tower
    public Transform TowerBase; // Where the base of the Tower rotate
    public Transform FiringPoint; // Where the projectile will fire

    protected Enemy currentEnemy;
    protected float currentFireCooldown = 1.0f;
    protected List<Enemy> enemiesInRange = new List<Enemy>();

    protected virtual void Update()
    {
        currentFireCooldown -= Time.deltaTime;
        Enemy closestEnemy = GetTargetEnemy();
        currentEnemy = closestEnemy;
        if (closestEnemy != null && currentFireCooldown <= 0.0f)
        {
            FireAt(closestEnemy);
            currentFireCooldown = FireCooldown;
        }
        else if(closestEnemy != null)
        {
            LookAtTarget();
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
    protected abstract Enemy GetTargetEnemy();

    /// <summary>
    /// When an enemy is within range of the Tower, rotate towards said enemy
    /// </summary>
    private void LookAtTarget()
    {
        if(IsTowerPlaced)
        {
            Vector3 lookDirection = currentEnemy.transform.position - TowerBase.position;
            Vector3 horizontalDirection = new Vector3(lookDirection.x, 0, lookDirection.z); // This is to only rotate Horizontally, ignoring the Y-axis
            Quaternion lookRotation = Quaternion.LookRotation(horizontalDirection);
            TowerBase.rotation = Quaternion.Slerp(TowerBase.rotation, lookRotation, Time.deltaTime * 5f); // To Rotate the TowerBase smoothly.
        }
    }

    /// <summary>
    /// On Pressing "Upgrade" button, and the player has enough Currency to Upgrade, Upgrade the Tower to 2
    /// </summary>
    public void UpgradeTower()
    {
        TowerUpgrade towerUpgrade = GetComponentInChildren<TowerUpgrade>();
        if (towerUpgrade.costToUpgrade <= CurrencyManager.CurrentCurrency)
        {
            Destroy(gameObject);
            TowerUpgraded(towerUpgrade.costToUpgrade);
            GameObject towerInstance = Instantiate(towerUpgrade.levelTwoTower, transform.position, Quaternion.identity);
            Tower tower = towerInstance.GetComponent<Tower>();
            tower.IsTowerPlaced = true;
        }
    }

    /// <summary>
    /// Take away the amount it cost to upgrade this Tower from the player's Currency
    /// </summary>
    private void TowerUpgraded(int cost)
    {
        CurrencyManager.CurrentCurrency -= cost;
    }


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
