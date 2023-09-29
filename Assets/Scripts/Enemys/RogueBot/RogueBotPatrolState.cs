using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class RogueBotPatrolState : RogueBotState
{
    private float patrolWaitTime;
    private bool playerInChaseRange;
    private float timer = 0.0f;

    public RogueBotStateId GetId()
    {
        return RogueBotStateId.Patrol;
    }

    public void Enter(RogueBotAgent agent)
    {
        Debug.Log("Patrol State");
        agent.navMeshAgent.speed = agent.config.patrolSpeed;
        agent.navMeshAgent.acceleration = agent.config.patrolAcceleration;
    }

    public void Update(RogueBotAgent agent)
    {
        // Chase State Change
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            // Check if player is in chase range
            playerInChaseRange = Physics.CheckSphere(agent.transform.position, agent.config.chaseRange, agent.config.playerLayerMask);

            if (playerInChaseRange)
            {
                RogueBotChaseState chaseState = agent.stateMachine.GetState(RogueBotStateId.Chase) as RogueBotChaseState;
                agent.stateMachine.ChangeState(RogueBotStateId.Chase);
            }
            timer = agent.config.tickRate;
        }


        // Patrolling Logic
        if (agent.navMeshAgent.remainingDistance <= agent.navMeshAgent.stoppingDistance) // Check if Rogue Bot has reached position
        {
            patrolWaitTime -= Time.deltaTime; // Countdown the wait timer
            if (patrolWaitTime <= 0)
            {
                Vector3 point;
                if (RandomPoint(agent.config.patrolCenterPoint, agent.config.patrolRange, out point))
                {
                    agent.navMeshAgent.SetDestination(point); // If timer hits 0 and within stopping distance set a new point
                }
            }
        }
        else
        {
            if (patrolWaitTime <= 0)
                patrolWaitTime = Random.Range(agent.config.minWaitTime, agent.config.maxWaitTime); // If timer hits 0 and not within stopping distance reset timer for next point
        }


        bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range; // Generate a random point
            float distFromLastPoint = Vector3.Distance(agent.transform.position, randomPoint);

            if (distFromLastPoint < agent.config.minDistFromLastPoint)
                randomPoint = center + Random.insideUnitSphere * range; // Generate a random point if the first point was within the minimum distance

            else if (distFromLastPoint > agent.config.minDistFromLastPoint)
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

    public void Exit(RogueBotAgent agent)
    {
    }
}
