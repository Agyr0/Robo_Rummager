using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scavenger : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    #region Patrolling
    private int destPoint;
    private float patrolWaitTime;

    [SerializeField]
    [Tooltip("List of points that the Scavenger will patrol.")]
    public Transform[] scavengerPatrolPoints;

    [SerializeField]
    [Tooltip("Check the box to have the Scavenger patrol randomly amongst patrol points, leave unchecked to patrol in order.")]
    private bool randomPatrol;

    [SerializeField]
    [Tooltip("Minimum time the Scavenger will wait at a location before moving to another.")]
    private float minWaitTime;

    [SerializeField]
    [Tooltip("Maximum time the Scavenger will wait at a location before moving to another.")]
    private float maxWaitTime;
    #endregion

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
        Debug.Log("Agent Velocity: " + agent.velocity.magnitude);
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            ScavengerPatrolling();
    }

    // Patrolling
    private void ScavengerPatrolling()
    {
        if (scavengerPatrolPoints.Length == 0)
            return;

        if (agent.remainingDistance <= agent.stoppingDistance) // Check if Scavenger has reached position
        {
            patrolWaitTime -= Time.deltaTime; // Countdown the wait timer
            if (patrolWaitTime <= 0)
            {
                if (randomPatrol == false)
                    destPoint = (destPoint + 1) % scavengerPatrolPoints.Length;
                else if (randomPatrol == true)
                    destPoint = Random.Range(0, scavengerPatrolPoints.Length);
                agent.destination = scavengerPatrolPoints[destPoint].position; // If timer hits 0 and within stopping distance set a new point
            }
        }
        else
        {
            if (patrolWaitTime <= 0)
                patrolWaitTime = Random.Range(minWaitTime, maxWaitTime);  // If timer hits 0 and not within stopping distance reset timer for next point
        }
    }
}
