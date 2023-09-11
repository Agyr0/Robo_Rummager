using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RogueBot : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform playerTransform;

    [SerializeField]
    private GameObject rogueBotAttackHitbox;
    [SerializeField]
    private LayerMask playerLayerMask;
    [SerializeField]
    private Animator rogueBotTestAttackAnimator;

    #region Patrolling
    private float patrolWaitTime;

    [Header("Patrol Settings")]
    [SerializeField]
    [Tooltip("Range that the Rogue Bot will patrol around in. Indicated by a green wire sphere.")]
    private float patrolRange;

    [SerializeField]
    [Tooltip("Center Point that the Rogue Bot will patrol around.")]
    private Transform patrolCenterPoint;

    [SerializeField]
    [Tooltip("Minimum distance that can be between new patrol points.")]
    private float minDistFromLastPoint;

    [SerializeField]
    [Tooltip("Minimum time the Rogue Bot will wait at a location before moving to another.")]
    private float minWaitTime;

    [SerializeField]
    [Tooltip("Maximum time the Rogue Bot will wait at a location before moving to another.")]
    private float maxWaitTime;
    #endregion

    #region Chasing
    private bool robotInLeashRange;
    private bool playerInChaseRange;

    [Header("Chase Settings")]
    [SerializeField]
    [Tooltip("Range that the Rogue Bot will detect the player and begin to chase them. Indicated by a blue wire sphere.")]
    private float chaseRange;

    [SerializeField]
    [Tooltip("Range that the Rogue Bot will chase the player before giving up. Indicated by a yellow wire sphere.")]
    private float leashRange;
    #endregion

    #region Attacking
    private bool playerInAttackRange;
    private bool canAttack = true;

    [Header("Attack Settings")]
    [SerializeField]
    [Tooltip("Range that the Rogue Bot will attack the player. Indicated by a red wire sphere.")]
    private float attackRange;

    [SerializeField]
    [Tooltip("The time between the Rogue Bots attacks.")]
    private float timeBetweenAttacks;
    #endregion

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.Find("Player").transform;                    // Need to find a better way to assign this at runtime
    }

    void Update()
    {
        // Check if player is in attack range
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayerMask);

        // Check if player is in sight range
        playerInChaseRange = Physics.CheckSphere(transform.position, chaseRange, playerLayerMask);

        // Check if robot is in the leash range
        if (Vector3.Distance(transform.position, patrolCenterPoint.transform.position) > leashRange)
            robotInLeashRange = false;
        else
            robotInLeashRange = true;



        // State Handler
        if (playerInAttackRange && robotInLeashRange)
            RogueBotAttacking();
        else if (playerInChaseRange && robotInLeashRange)
            RogueBotChasing();
        else
            RogueBotPatrolling();
    }

    // Patrolling
    private void RogueBotPatrolling()
    {
        if (agent.remainingDistance <= agent.stoppingDistance) // Check if Rogue Bot has reached position
        {
            patrolWaitTime -= Time.deltaTime;
            if (patrolWaitTime <= 0)
            {
                Vector3 point;
                if (RandomPoint(patrolCenterPoint.position, patrolRange, out point))
                {
                    Debug.DrawRay(point, Vector3.up, UnityEngine.Color.red, 5.0f); // Draw the Rogue Bot's next position
                    agent.SetDestination(point);
                }
            }
        }
        else
        {
            if (patrolWaitTime <= 0)
                patrolWaitTime = Random.Range(minWaitTime, maxWaitTime);
        }

        bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range; // Generate a random point
            float distFromLastPoint = Vector3.Distance(this.transform.position, randomPoint);

            if (distFromLastPoint < minDistFromLastPoint)
                randomPoint = center + Random.insideUnitSphere * range; // Generate a random point if the first point was within the minimum distance
            else if(distFromLastPoint > minDistFromLastPoint)
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))  // Find closest point on NavMesh
                {
                    result = hit.position;
                    return true;
                }
            }
            result = Vector3.zero;
            return false;
        }
    }

    // Chasing
    private void RogueBotChasing()
    {
        agent.SetDestination(playerTransform.position);
    }

    // Attacking
    private void RogueBotAttacking()
    {
        agent.ResetPath();
        if (canAttack == true)
        {
            rogueBotTestAttackAnimator.Play("RogueBotTestAttack");
            StartCoroutine(ResetRogueBotAttackCooldown());
            StartCoroutine(ActivateHitbox());
            canAttack = false;
        }
    }

    IEnumerator ResetRogueBotAttackCooldown()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        canAttack = true;
    }

    IEnumerator ActivateHitbox()
    {
        yield return new WaitForSeconds(0.75f);
        rogueBotAttackHitbox.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        rogueBotAttackHitbox.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        // Draw the Patrolling Range
        Gizmos.color = UnityEngine.Color.green;
        Gizmos.DrawWireSphere(patrolCenterPoint.position, patrolRange);

        // Draw the Sight Range
        Gizmos.color = UnityEngine.Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        // Draw the Attack Range
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Draw the Leash Range
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireSphere(patrolCenterPoint.position, leashRange);
    }
}
