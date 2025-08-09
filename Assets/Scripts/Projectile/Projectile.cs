using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    
    [SerializeField] protected float speed;
    
    protected Transform target;
    protected int ProjectileDamage;

    protected virtual void Update()
    {
        if(target != null)
        {
            Vector3 direction = target.position - transform.position;
            transform.position += direction * speed * Time.deltaTime;
            transform.forward = direction;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    /// <summary>
    /// Set to find the Enemy Transform in Cannon, Arrow, and Boulder Projectile Scripts
    /// </summary>
    public abstract void SetTarget(Transform inputTarget);

    protected abstract void OnTriggerEnter(Collider other);
}
