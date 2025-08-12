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

    private void Start()
    {
        Enemy enemy = GetComponent<Enemy>();
        enemy.OnJammerBeingHit += DisableNearbyTowers;
    }

    /// <summary>
    /// When the JammerEnemy gets hit, it will disable a Tower within a 1f Radius and turn them blue.
    /// The Freeze Tower intentionally does not proc this effect.
    /// If the JammerEnemy has less than 20 Health, do not activate this Ability
    /// </summary>
    private void DisableNearbyTowers()
    {
        Collider[] towerColliders = Physics.OverlapSphere(transform.position, disableTowerBlastRadius);
        foreach(Collider nearbyTowers in towerColliders)
        {
            Tower tower = nearbyTowers.GetComponent<Tower>();
            if (tower != null && tower.IsTowerPlaced != false)
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
        StartCoroutine(DisableTowerTimer());
        
    }

    /// <summary>
    /// The time that a Tower stays disable, after that time has elapsed, reenable the Tower(s).
    /// Also, turn it back to its original material
    /// </summary>
    private IEnumerator DisableTowerTimer()
    {
        yield return new WaitForSeconds(effectLifeTime);
        EnableNearbyTowers();
    }

    /// <summary>
    /// Reenable the Towers the Jammer Enemy disabled and give its original Material back.
    /// Also used in the HasDied function in the Enemy script
    /// </summary>
    public void EnableNearbyTowers()
    {
        foreach(KeyValuePair<Tower, Material> originalTowerMaterial in originalMaterials)
        {
            Tower tower = originalTowerMaterial.Key;
            Material towerMaterial = originalTowerMaterial.Value;
            if(tower != null)
            {
                tower.GetComponentInChildren<Renderer>().material = towerMaterial;
                tower.IsTowerPlaced = true;
            }
        }
        originalMaterials.Clear();
    }
}
