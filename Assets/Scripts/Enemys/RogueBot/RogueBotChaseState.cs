using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RogueBotChaseState : RogueBotState
{
    private bool playerInChaseRange;
    private bool playerInChargeRange;
    private float timer = 0.0f;
    private float maxDistance = 1.0f;
    private Transform playerTransform;

    public RogueBotStateId GetId()
    {
        return RogueBotStateId.Chase;
    }

    public void Enter(RogueBotAgent agent)
    {
        Debug.Log("Chase State");
        agent.navMeshAgent.speed = agent.config.chaseSpeed;
        agent.navMeshAgent.acceleration = agent.config.chaseAcceleration;
        playerTransform = GameObject.Find("Player").transform;
        agent.StartCoroutine(DetectedIcon(agent));
    }

    public void Update(RogueBotAgent agent)
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            // Check if player is in charge range and if so swap to the charge state
            playerInChargeRange = Physics.CheckSphere(agent.transform.position, agent.config.chargeRange, agent.config.playerLayerMask);
            if (playerInChargeRange)
            {
                RogueBotChargeState chargeState = agent.stateMachine.GetState(RogueBotStateId.Charge) as RogueBotChargeState;
                agent.stateMachine.ChangeState(RogueBotStateId.Charge);
            }

            // Check if player is in chase range and if not return to patrolling
            playerInChaseRange = Physics.CheckSphere(agent.transform.position, agent.config.chaseRange, agent.config.playerLayerMask);
            if (!playerInChaseRange)
            {
                RogueBotPatrolState patrolState = agent.stateMachine.GetState(RogueBotStateId.Patrol) as RogueBotPatrolState;
                agent.stateMachine.ChangeState(RogueBotStateId.Patrol);
            }

            // Chasing Logic
            float sqDistance = (playerTransform.position - agent.navMeshAgent.destination).sqrMagnitude;
            if(sqDistance > maxDistance * maxDistance)
            {
                agent.navMeshAgent.SetDestination(playerTransform.position);
            }
            timer = agent.config.tickRate;
        }
    }

    public void Exit(RogueBotAgent agent)
    {
    }

    IEnumerator DetectedIcon(RogueBotAgent agent)
    {
        agent.detectedIcon.SetActive(true);
        yield return new WaitForSeconds(agent.config.spriteFlashTime);
        agent.detectedIcon.SetActive(false);
    }
}
