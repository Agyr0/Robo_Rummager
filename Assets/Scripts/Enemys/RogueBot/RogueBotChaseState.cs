using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueBotChaseState : RogueBotState
{
    private Transform playerTransform;
    private bool playerInChargeRange;
    private float maxDistance = 1.0f;
    private float updateTimer = 0.0f;

    public RogueBotStateId GetId()
    {
        return RogueBotStateId.Chase;
    }

    public void Enter(RogueBotAgent agent)
    {
        Debug.Log("RogueBot Entered: Chase State");
        agent.navMeshAgent.speed = agent.config.chaseSpeed;
        agent.navMeshAgent.acceleration = agent.config.chaseAcceleration;
        agent.navMeshAgent.angularSpeed = agent.config.chaseAngularSpeed;

        playerTransform = GameManager.Instance.PlayerVCam.transform;
    }

    public void Update(RogueBotAgent agent)
    {
        updateTimer -= Time.deltaTime;
        if (updateTimer < 0.0f)
        {
            // Check if player is in charge range and if so swap to the charge state
            playerInChargeRange = Physics.CheckSphere(agent.transform.position, agent.config.chargeRange, agent.config.playerLayerMask);
            if (playerInChargeRange)
            {
                RogueBotChargeState chargeState = agent.stateMachine.GetState(RogueBotStateId.Charge) as RogueBotChargeState;
                agent.stateMachine.ChangeState(RogueBotStateId.Charge);
            }

            // Chasing Logic
            float sqDistance = (playerTransform.position - agent.navMeshAgent.destination).sqrMagnitude;
            if(sqDistance > maxDistance * maxDistance)
            {
                agent.navMeshAgent.SetDestination(playerTransform.position);
            }
            updateTimer = agent.config.tickRate;
        }
    }

    public void Exit(RogueBotAgent agent)
    {
    }
}
