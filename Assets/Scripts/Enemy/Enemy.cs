using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] private Transform endPoint;
    [SerializeField] private string animatorParamIsWalking;
    [SerializeField] private int damage;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


    void Start()
    {
        agent.SetDestination(endPoint.position);
        animator.SetBool(animatorParamIsWalking, true);
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
