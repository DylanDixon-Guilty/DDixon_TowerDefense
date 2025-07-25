using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    
    [SerializeField] protected float speed;
    protected float lifeTime = 3f;
    
    protected Transform target;
    
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

    public abstract void SetTarget(Transform inputTarget);

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == target)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if(enemy != null)
            {
                Destroy(enemy.gameObject);
            }
        }
        Destroy(gameObject);
    }
}
