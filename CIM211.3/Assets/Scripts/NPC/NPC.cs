using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPC : MonoBehaviour
{
    [Header("Movement")]
    public float wanderRadius = 25f;      
    public float minWait = 1.5f;          
    public float maxWait = 4.0f;             
    public float arriveDistance = 0.6f;      

    [Header("Animation")]
    public Animator animator;


    private NavMeshAgent agent;
    private float waitTimer;
    private float waitTarget;
    private bool hasDestination;

    private float normalizedSpeed;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        if (animator == null) animator = GetComponentInChildren<Animator>();

        agent.updatePosition = true;
        agent.updateRotation = false; 
    }

    void Start()
    {
        PickNewWait();
        TrySetNewDestination();
    }

    void Update()
    {
        UpdateRotation();

        UpdateAnimation();

        if (!hasDestination || HasArrived())
        {
            hasDestination = false;
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitTarget)
            {
                PickNewWait();
                TrySetNewDestination();
            }
        }
    }

    private void UpdateRotation()
    {
        Vector3 vel = agent.velocity;
        vel.y = 0f;

        if (vel.sqrMagnitude > 0.05f)
        {
            Quaternion targetRot = Quaternion.LookRotation(vel.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 8f);
        }
    }

    private void UpdateAnimation()
    {
        float speed01 = 0f;
        if (agent != null && agent.speed > 0.01f)
        {
            speed01 = Mathf.Clamp01(agent.velocity.magnitude / agent.speed);
        }
            

        normalizedSpeed = Mathf.Lerp(normalizedSpeed, speed01, Time.deltaTime * 10f);

        if (animator != null)
        {
            if (normalizedSpeed > 0.01f)
                animator.SetTrigger("Moving");
            else
            {
                animator.SetTrigger("Idle");
            }
        }
    }

    private bool HasArrived()
    {
        if (agent.pathPending) return false;

        if (agent.remainingDistance == Mathf.Infinity) return false;

        return agent.remainingDistance <= Mathf.Max(arriveDistance, agent.stoppingDistance + 0.05f);
    }

    private void PickNewWait()
    {
        waitTimer = 0f;
        waitTarget = Random.Range(minWait, maxWait);
    }

    private void TrySetNewDestination()
    {
        Vector3 origin = transform.position;

        if (NavMesh.SamplePosition(origin, out var onMesh, 2f, NavMesh.AllAreas))
            origin = onMesh.position;

        Vector3 randomDir = Random.insideUnitSphere * wanderRadius;
        randomDir.y = 0f;
        Vector3 target = origin + randomDir;

        if (NavMesh.SamplePosition(target, out var hit, wanderRadius, NavMesh.AllAreas))
        {
            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(hit.position, path) && path.status == NavMeshPathStatus.PathComplete)
            {

                agent.speed = Mathf.Min(agent.speed, 2.2f); 

                agent.SetDestination(hit.position);
                hasDestination = true;
            }
        }
    }
}
