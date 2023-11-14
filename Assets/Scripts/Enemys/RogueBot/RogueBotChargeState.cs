using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RogueBotChargeState : RogueBotState
{
    private bool rogueBotCharging = false;
    private bool rogueBotSwinging = false;

    public RogueBotStateId GetId()
    {
        return RogueBotStateId.Charge;
    }

    public void Enter(RogueBotAgent agent)
    {
        Debug.Log("RogueBot Entered: Charge State");
        agent.StartCoroutine(ChargePlayer(agent));
    }

    public void Update(RogueBotAgent agent)
    {
        agent.animator.SetBool("Charging", rogueBotCharging);
        agent.animator.SetBool("Swinging", rogueBotSwinging);
        if (rogueBotSwinging)
        {
            agent.chargeHitbox.SetActive(true);
        }
        else if (rogueBotSwinging == false)
        {
            agent.chargeHitbox.SetActive(false);
        }
    }

    public void Exit(RogueBotAgent agent)
    {
    }

    IEnumerator ChargePlayer(RogueBotAgent agent)
    {
        // Pause before charging
        agent.navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(agent.config.pauseBeforeChargeTime);

        // Charging
        rogueBotCharging = true;
        agent.navMeshAgent.isStopped = false;
        agent.navMeshAgent.speed = agent.config.chargeSpeed;
        agent.navMeshAgent.acceleration = agent.config.chargeAcceleration;
        agent.navMeshAgent.angularSpeed = agent.config.chargeAngularSpeed;
        yield return new WaitForSeconds(agent.config.chargeDuration);
        rogueBotCharging = false;

        // Swing Arm
        rogueBotSwinging = true;
        agent.navMeshAgent.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.5f);
        rogueBotSwinging = false;
        agent.stateMachine.ChangeState(RogueBotStateId.Reposition);
    }
}
