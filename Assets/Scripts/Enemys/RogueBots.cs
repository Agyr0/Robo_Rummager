using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class RogueBots : MonoBehaviour
{
    private NavMeshAgent agent;

    public Transform player;

    [SerializeField]
    private LayerMask playerLayerMask;

    #region Patrolling
    private float patrolWaitTime;

    [Header("Patrol Settings")]
    [SerializeField]
    [Tooltip("Range that the Rogue Bot will patrol around in.")]
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
    [SerializeField]
    private bool playerInSightRange;

    [SerializeField]
    [Tooltip("Range that the Rogue Bot will detect the player and begin to chase them.")]
    private float sightRange;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayerMask);

        if (playerInSightRange)
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

    // Chasing
    private void RogueBotChasing()
    {
        agent.SetDestination(player.position);
    }

    private void OnDrawGizmos()
    {
        // Draw the Patrolling Range
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireSphere(patrolCenterPoint.position, patrolRange);

        // Draw the Sight Range
        Gizmos.color = UnityEngine.Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
