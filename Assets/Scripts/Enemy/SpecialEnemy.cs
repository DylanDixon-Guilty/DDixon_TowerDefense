using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpecialEnemy : MonoBehaviour
{
    [SerializeField] private Material Blue;
    [SerializeField] private float effectLifeTime;
    [SerializeField] private int disableTowerBlastRadius;
    [SerializeField] private int enableTowerBlastRadius;
    private Dictionary<Tower, Material> originalMaterials = new Dictionary<Tower, Material>();

    private void Update()
    {
        DisableNearbyTowers();
    }

    /// <summary>
    /// When the JammerEnemy gets hit, it will disable a Tower within a 1f Radius and turn them blue.
    /// The Freeze Tower intentionally does not proc this effect
    /// </summary>
    private void DisableNearbyTowers()
    {
        Enemy enemy = GetComponent<Enemy>();
        if(enemy.HasTakenDamage)
        {
            Collider[] towerColliders = Physics.OverlapSphere(transform.position, disableTowerBlastRadius);
            foreach(Collider nearbyTowers in towerColliders)
            {
                Tower tower = nearbyTowers.GetComponent<Tower>();
                if(tower != null)
                {
                    Renderer towerRenderer = tower.GetComponentInChildren<Renderer>();
                    if(!originalMaterials.ContainsKey(tower))
                    {
                        originalMaterials[tower] = towerRenderer.material;
                    }
                    tower.GetComponentInChildren<Renderer>().material = Blue;
                    tower.IsTowerPlaced = false;
                }
            }
            enemy.HasTakenDamage = false;
            StartCoroutine(DisableTowerTimer());
        }
    }

    /// <summary>
    /// The time that a Tower stays disable, after that time has elapsed, Renable the Tower.
    /// Also, turn it back to its original material
    /// </summary>
    IEnumerator DisableTowerTimer()
    {
        yield return new WaitForSeconds(effectLifeTime);
        Collider[] towerColliders = Physics.OverlapSphere(transform.position, enableTowerBlastRadius);
        foreach(Collider nearbyTowers in towerColliders)
        {
            Tower tower = nearbyTowers.GetComponent<Tower>();
            if (tower != null && !tower.IsTowerPlaced)
            {
                tower.IsTowerPlaced = true;
                if(originalMaterials.TryGetValue(tower, out Material originalMaterial))
                {
                    tower.GetComponentInChildren<Renderer>().material = originalMaterial;
                }
                
            }
        }
        originalMaterials.Clear();
    }
}
