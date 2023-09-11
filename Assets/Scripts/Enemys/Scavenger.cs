using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scavenger : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private Transform playerTransform;
    

    [SerializeField]
    private LayerMask playerLayerMask;

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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerTransform = GameObject.Find("Player").transform;                    // Need to find a better way to assign this at runtime
    }

    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
        ScavengerPatrolling();
    }

    // Patrolling
    private void ScavengerPatrolling()
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

            else if (distFromLastPoint > minDistFromLastPoint)
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

    private void OnDrawGizmos()
    {
        // Draw the Patrolling Range
        Gizmos.color = UnityEngine.Color.green;
        Gizmos.DrawWireSphere(patrolCenterPoint.position, patrolRange);
    }
}
