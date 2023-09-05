using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RogueBots : MonoBehaviour
{
    #region Patrolling
    private NavMeshAgent agent;
    private float waitTime;
    
    [Header("Patrol Settings")]
    [SerializeField]
    [Tooltip("Range that the enemy AI will patrol around in.")]
    private float range;

    [SerializeField]
    [Tooltip("Center Point that the enemy AI will patrol around.")]
    private Transform centerPoint;

    [SerializeField]
    [Tooltip("Minimum distance that can be between new patrol points.")]
    private float minDistFromLastPoint;

    [SerializeField]
    [Tooltip("Minimum time the enemy AI will wait at a location before moving to another.")]
    private float minWaitTime;

    [SerializeField]
    [Tooltip("Maximum time the enemy AI will wait at a location before moving to another.")]
    private float maxWaitTime;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        RogueBotPatrolling();
    }

    // Patrolling
    private void RogueBotPatrolling()
    {
        if (agent.remainingDistance <= agent.stoppingDistance) // Check if Rogue Bot has reached position
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                Vector3 point;
                if (RandomPoint(centerPoint.position, range, out point))
                {
                    Debug.DrawRay(point, Vector3.up, Color.red, 5.0f); // Draw the Rogue Bot's next position
                    agent.SetDestination(point);
                }
            }
        }
        else
        {
            if (waitTime <= 0)
                waitTime = Random.Range(minWaitTime, maxWaitTime);
        }

        bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range; // Generate a random point
            float distFromLastPoint = Vector3.Distance(this.transform.position, randomPoint);

            if (distFromLastPoint < minDistFromLastPoint)
                randomPoint = center + Random.insideUnitSphere * range; // Generate a random point if the first point was within the minimum distance

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))  // Find closest point on NavMesh
            {
                result = hit.position;
                return true;
            }

            result = Vector3.zero;
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        // Draw the Patrolling Range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(centerPoint.position, range);
    }
}
