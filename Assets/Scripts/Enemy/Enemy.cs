using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;
    public int CurrencyValue;
    public bool IsWalkingTrue;
    public event Action<int, int> OnEnemyHealthChange;

    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] private Transform endPoint;
    [SerializeField] private string animatorParamIsWalking;
    [SerializeField] private int damage;
    [SerializeField] private GameObject blueGoldCurrecy;
    

    private void Awake()
    {
        CurrentHealth = MaxHealth;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


    void Start()
    {
        animator.SetBool(animatorParamIsWalking, true);
    }

    /// <summary>
    /// This allows to Initialize the endPoint for the Enemy
    /// </summary>
    public void Initialized(Transform inputEndPoint)
    {
        endPoint = inputEndPoint;
        agent.SetDestination(endPoint.position);
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if(!agent.hasPath || agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                ReachEnd();
            }
        }
        HasDied();
    }

    /// <summary>
    /// When the enemy is hit with a projectile, the enemy will take damage
    /// </summary>
    public void EnemyTakeDamage(int damageAmount)
    {
        if (CurrentHealth > 0)
        {
            CurrentHealth = Mathf.Max(CurrentHealth - damageAmount, 0);
            OnEnemyHealthChange?.Invoke(CurrentHealth, MaxHealth);
            Debug.Log($"Current Enemy Health: {CurrentHealth}");
        }
    }

    /// <summary>
    /// When Enemy dies from a Tower, Destroy gameObject and gives points to player
    /// </summary>
    private void HasDied()
    {
        if(CurrentHealth <= 0)
        {
            Instantiate(blueGoldCurrecy, transform.position, transform.rotation);
            CurrencyManager.Currency += CurrencyValue;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// When the Enemy reach the end of the path, take away one health from the player and despawn
    /// </summary>
    private void ReachEnd()
    {
        animator.SetBool(animatorParamIsWalking, false);
        GameManager.instance.playerHealth.TakeDamage(damage);
        Destroy(gameObject);
    }
}
