using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerRepositionState : ScavengerState
{
    private GameObject playerGameObject;

    public ScavengerStateId GetId()
    {
        return ScavengerStateId.Reposition;
    }

    public void Enter(ScavengerAgent agent)
    {
        Debug.Log("Scavenger Entered: Reposition State");
        agent.navMeshAgent.speed = agent.config.repositionSpeed;
        agent.navMeshAgent.acceleration = agent.config.repositionAcceleration;
        agent.navMeshAgent.angularSpeed = agent.config.repositionAngularSpeed;

        playerGameObject = GameObject.Find("Player");
        SetRandomPoint(agent);
    }
    public void Update(ScavengerAgent agent)
    {
        if (agent.navMeshAgent.remainingDistance < 0.1f)
        {
            agent.stateMachine.ChangeState(ScavengerStateId.Detection);
        }
    }

    public void Exit(ScavengerAgent agent)
    {
        agent.navMeshAgent.ResetPath();
        agent.navMeshAgent.velocity = Vector3.zero;
    }

    private void SetRandomPoint(ScavengerAgent agent)
    {
        // Calculate a random direction away from the player
        Vector3 randomDirection = Random.insideUnitSphere * agent.config.maxDistanceFromPlayer;

        // Ensure the destination is not towards the player
        Vector3 toPlayer = (playerGameObject.transform.position - agent.transform.position).normalized;
        randomDirection -= toPlayer * Vector3.Dot(randomDirection, toPlayer);

        // Calculate the target position
        Vector3 randomDestination = agent.transform.position + randomDirection;

        // Set the NavMesh agent's destination
        agent.navMeshAgent.SetDestination(randomDestination);
    }
}
