using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerPatrolState : ScavengerState
{
    private float patrolWaitTime;
    private int patrolPointIndex;
    private GameObject playerGameObject;

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

        playerGameObject = GameObject.Find("Player");
    }

    public void Update(ScavengerAgent agent)
    {
        // Check if player is in vision cone to determine if agent should swap to detection state
        if(agent.scavengerSensor.IsInSight(playerGameObject) == true)
        {
            agent.stateMachine.ChangeState(ScavengerStateId.Detection);
        }

        // Safety net in case there are no patrol points for some reason
        if (agent.config.scavengerPatrolPoints.Count == 0)
        {
            Debug.Log(this + "has no patrol points!");
            return;
        }

        // Patrolling Logic
        if (agent.navMeshAgent.remainingDistance <= agent.navMeshAgent.stoppingDistance) // Check if Scavenger has reached position
        {
            patrolWaitTime -= Time.deltaTime; // Countdown the wait timer
            if (patrolWaitTime <= 0)
            {
                if (agent.config.randomPatrol == false)
                    patrolPointIndex = (patrolPointIndex + 1) % agent.config.scavengerPatrolPoints.Count;
                else if (agent.config.randomPatrol == true)
                    patrolPointIndex = Random.Range(0, agent.config.scavengerPatrolPoints.Count);

                agent.navMeshAgent.destination = agent.config.scavengerPatrolPoints[patrolPointIndex].transform.position; // If timer hits 0 and within stopping distance set a new point
            }
        }
        // Not positive that this is neccesary? TODO: Test if this else statement can be removed.
        else
        {
            if (patrolWaitTime <= 0)
                patrolWaitTime = Random.Range(agent.config.minWaitTime, agent.config.maxWaitTime);  // If timer hits 0 and not within stopping distance reset timer for next point
        }
    }

    public void Exit(ScavengerAgent agent)
    {
        agent.navMeshAgent.ResetPath();
        agent.navMeshAgent.velocity = Vector3.zero;
    }
}
