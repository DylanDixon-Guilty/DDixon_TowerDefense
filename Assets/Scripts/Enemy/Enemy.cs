using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float MaxHealth;
    public static float CurrentHealth;

    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] private Transform endPoint;
    [SerializeField] private string animatorParamIsWalking;
    [SerializeField] private int damage;

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

    public void Initialized(Transform inputEndPoint)
    {
        endPoint = inputEndPoint;
        agent.SetDestination(endPoint.position);
    }


    void Update()
    {
        if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if(!agent.hasPath || agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                ReachEnd();
            }
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
