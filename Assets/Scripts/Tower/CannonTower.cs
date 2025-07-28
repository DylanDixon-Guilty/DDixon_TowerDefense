using UnityEngine;

public class CannonTower : Tower
{
    public Transform TowerAxis;
    public Transform EnemyPosition;
    public float range = 100f;

    [SerializeField] private GameObject projectilePrefab;
    private float timerToShoot;
    private int shootableMask;
    private float effectDisplayTime = 0.2f;
    private Ray shootRay;
    private RaycastHit shootHit;
    private LineRenderer gunLine;
    private Light gunLight;

    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
    }

    protected override void Update()
    {
        timerToShoot += Time.deltaTime;
        if (timerToShoot >= FireCooldown * effectDisplayTime)
        {
            DisableEffects();
        }

        base.Update();
    }

    /// <summary>
    /// When called upon enable the gunLine and gunLight then shoot the gunLine renderer forward, then check to see if there is an enemy. 
    /// If the gunLine hits an enemy and it is not null, fire the Cannon Prefab.
    /// </summary>
    protected override void FireAt(Enemy target)
    {
        timerToShoot = 0f;
        
        gunLight.enabled = true;
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (projectilePrefab != null)
        {
            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                Enemy enemy = shootHit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                    projectileInstance.GetComponent<Projectile>().SetTarget(target.transform);
                }
                gunLine.SetPosition(1, shootHit.point);
            }
            else
            {
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            }
        }
    }

    /// <summary>
    /// Disables the gunlight and gunline
    /// </summary>
    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    protected override Enemy GetClosestEnemy()
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
}
