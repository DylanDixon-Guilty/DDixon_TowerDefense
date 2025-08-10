using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpecialEnemy : MonoBehaviour
{
    public Material Blue;

    [SerializeField] private float effectLifeTime;
    [SerializeField] private int disableTowerBlastRadius;
    [SerializeField] private int enableTowerBlastRadius;
    private Dictionary<Tower, Material> originalMaterials = new Dictionary<Tower, Material>();

    private void Update()
    {
        DisableNearbyTowers();
    }

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
