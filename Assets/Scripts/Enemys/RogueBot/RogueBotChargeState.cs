using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;
using UnityEngine.Polybrush;

public class RogueBotChargeState : RogueBotState
{
    private Rigidbody rb;

    public RogueBotStateId GetId()
    {
        return RogueBotStateId.Charge;
    }

    public void Enter(RogueBotAgent agent)
    {
        Debug.Log("Charge State");
        rb = agent.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        agent.navMeshAgent.velocity = Vector3.zero;
        agent.StartCoroutine(PauseBeforeCharge(agent));
        rb.AddForce(agent.navMeshAgent.velocity * 100);
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
        rb.isKinematic = true;
        yield return new WaitForSeconds(agent.config.chargeDuration);
        RogueBotChaseState chaseState = agent.stateMachine.GetState(RogueBotStateId.Chase) as RogueBotChaseState;
        agent.stateMachine.ChangeState(RogueBotStateId.Chase);
    }
}
