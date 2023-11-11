using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueBotChargeState : RogueBotState
{
    public RogueBotStateId GetId()
    {
        return RogueBotStateId.Charge;
    }

    public void Enter(RogueBotAgent agent)
    {
        Debug.Log("RogueBot Entered: Charge State");
        agent.StartCoroutine(ChargePlayer(agent));
        agent.chargeHitbox.SetActive(true);
    }
    
    public void Update(RogueBotAgent agent)
    {
    }

    public void Exit(RogueBotAgent agent)
    {
    }

    IEnumerator ChargePlayer(RogueBotAgent agent)
    {
        agent.navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(agent.config.pauseBeforeChargeTime);
        agent.navMeshAgent.isStopped = false;
        agent.navMeshAgent.speed = agent.config.chargeSpeed;
        agent.navMeshAgent.acceleration = agent.config.chargeAcceleration;
        agent.navMeshAgent.angularSpeed = agent.config.chargeAngularSpeed;
        yield return new WaitForSeconds(agent.config.chargeDuration);
        agent.navMeshAgent.velocity = Vector3.zero;
        agent.stateMachine.ChangeState(RogueBotStateId.Reposition);
    }
}
