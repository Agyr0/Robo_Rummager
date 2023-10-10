using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerShootingState : ScavengerState
{
    private Transform playerTransform;

    public ScavengerStateId GetId()
    {
        return ScavengerStateId.Shooting;
    }

    public void Enter(ScavengerAgent agent)
    {
        playerTransform = GameObject.Find("Player").transform;
        agent.animator.Play("Shooting");
    }

    public void Update(ScavengerAgent agent)
    {
        agent.navMeshAgent.SetDestination(agent.transform.position);
    }

    public void Exit(ScavengerAgent agent)
    {
        agent.navMeshAgent.ResetPath();
        agent.navMeshAgent.velocity = Vector3.zero;
    }
}
