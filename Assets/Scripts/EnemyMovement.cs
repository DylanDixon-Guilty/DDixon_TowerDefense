using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] private Transform endPoint;
    [SerializeField] private string animatorParamIsWalking;

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
                animator.SetBool(animatorParamIsWalking, false);
            }
        }
    }
}
