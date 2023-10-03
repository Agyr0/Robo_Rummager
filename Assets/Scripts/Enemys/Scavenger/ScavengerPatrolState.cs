using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerPatrolState : ScavengerState
{
    private float patrolWaitTime;

    public ScavengerStateId GetId()
    {
        return ScavengerStateId.Patrol;
    }
    
    public void Enter(ScavengerAgent agent)
    {
        Debug.Log("Scavenger Entered: Patrol State");
        agent.navMeshAgent.speed = agent.config.patrolSpeed;
        agent.navMeshAgent.acceleration = agent.config.patrolAcceleration;
        agent.navMeshAgent.angularSpeed = agent.config.patrolAngularSpeed;
    }

    public void Update(ScavengerAgent agent)
    {
        if (agent.config.scavengerPatrolPoints.Count == 0)
            return;

        if (agent.navMeshAgent.remainingDistance <= agent.navMeshAgent.stoppingDistance) // Check if Scavenger has reached position
        {
            patrolWaitTime -= Time.deltaTime; // Countdown the wait timer
            if (patrolWaitTime <= 0)
            {
                if (agent.config.randomPatrol == false)
                    destPoint = (destPoint + 1) % scavengerPatrolPoints.Length;
                else if (randomPatrol == true)
                    destPoint = Random.Range(0, scavengerPatrolPoints.Length);
                agent.navMeshAgent.destination = scavengerPatrolPoints[destPoint].position; // If timer hits 0 and within stopping distance set a new point
            }
        }
        else
        {
            if (patrolWaitTime <= 0)
                patrolWaitTime = Random.Range(agent.config.minWaitTime, agent.config.maxWaitTime);  // If timer hits 0 and not within stopping distance reset timer for next point
        }
    }

    public void Exit(ScavengerAgent agent)
    {
    }
}
