using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class RogueBotChargeState : RogueBotState
{
    private Transform playerTransform;

    public RogueBotStateId GetId()
    {
        return RogueBotStateId.Charge;
    }

    public void Enter(RogueBotAgent agent)
    {
        Debug.Log("Charge State");
        agent.navMeshAgent.speed = agent.config.chargeSpeed;
        agent.navMeshAgent.acceleration = agent.config.chargeAcceleration;
        agent.navMeshAgent.angularSpeed = agent.config.chargeAngularSpeed;

        agent.StartCoroutine(PauseBeforeCharge(agent));
        agent.chargeHitbox.SetActive(true);
        agent.StartCoroutine(ExitChargeState(agent));
    }
    
    public void Update(RogueBotAgent agent)
    {
    }

    public void Exit(RogueBotAgent agent)
    {
    }

    IEnumerator PauseBeforeCharge(RogueBotAgent agent)
    {
        agent.navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(agent.config.pauseBeforeChargeTime);
        agent.navMeshAgent.isStopped = false;
    }

    IEnumerator ExitChargeState(RogueBotAgent agent)
    {
        yield return new WaitForSeconds(agent.config.chargeDuration);
        agent.chargeHitbox.SetActive(false);
        RogueBotChaseState chaseState = agent.stateMachine.GetState(RogueBotStateId.Chase) as RogueBotChaseState;
        agent.stateMachine.ChangeState(RogueBotStateId.Chase);
    }
}
