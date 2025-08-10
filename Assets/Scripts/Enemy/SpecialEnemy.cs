using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SpecialEnemy : MonoBehaviour
{
    public Material Blue;

    [SerializeField] private float effectLifeTime;
    [SerializeField] private int disableBlastRadius;
    [SerializeField] private int enableBlastRadius;
    private Renderer originalRenderer;

    private void Update()
    {
        DisableNearbyTowers();
    }

    private void DisableNearbyTowers()
    {
        Enemy enemy = GetComponent<Enemy>();
        if(enemy.HasTakenDamage)
        {
            Collider[] towerColliders = Physics.OverlapSphere(transform.position, disableBlastRadius);
            foreach(Collider nearbyTowers in towerColliders)
            {
                Tower tower = nearbyTowers.GetComponent<Tower>();
                if(tower != null)
                {
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
        Collider[] towerColliders = Physics.OverlapSphere(transform.position, enableBlastRadius);
        foreach(Collider nearbyTowers in towerColliders)
        {
            Tower tower = nearbyTowers.GetComponent<Tower>();
            if (tower != null && !tower.IsTowerPlaced)
            {
                tower.IsTowerPlaced = true;
                //tower.GetComponentInChildren<Renderer>().material = originalRenderer.material;
            }
        }
    }
}
