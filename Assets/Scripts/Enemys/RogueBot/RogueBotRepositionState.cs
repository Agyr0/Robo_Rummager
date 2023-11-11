using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueBotRepositionState : RogueBotState
{
    public RogueBotStateId GetId()
    {
        return RogueBotStateId.Reposition;
    }

    public void Enter(RogueBotAgent agent)
    {
        Debug.Log("RogueBot Entered: Reposition State");
        agent.navMeshAgent.speed = agent.config.repositionSpeed;
        agent.navMeshAgent.acceleration = agent.config.repositionAcceleration;
        agent.navMeshAgent.angularSpeed = agent.config.repositionAngularSpeed;

        SetRandomPoint(agent);
    }

    public void Update(RogueBotAgent agent)
    {
        if (agent.navMeshAgent.remainingDistance < 0.1f)
        {
            Debug.Log("Changed to Chase State from Reposition");
            agent.stateMachine.ChangeState(RogueBotStateId.Chase);
        }
    }

    public void Exit(RogueBotAgent agent)
    {
    }

    private void SetRandomPoint(RogueBotAgent agent)
    {
        // Calculate a random direction away from the player
        Vector3 randomDirection = Random.insideUnitSphere * Random.Range(agent.config.minRepositionDistance, agent.config.maxRepositionDistance);

        // Ensure the destination is not towards the player
        Vector3 toPlayer = (GameManager.Instance.playerController.gameObject.transform.position - agent.transform.position).normalized;
        randomDirection -= toPlayer * Vector3.Dot(randomDirection, toPlayer);

        // Calculate the target position
        Vector3 randomDestination = agent.transform.position + randomDirection;

        // Set the NavMesh agent's destination
        agent.navMeshAgent.SetDestination(randomDestination);
    }
}
