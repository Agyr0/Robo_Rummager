using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerShootingState : ScavengerState
{
    private ScavengerFireGun scavengerFireGun;
    private GameObject playerGameObject;

    public ScavengerStateId GetId()
    {
        return ScavengerStateId.Shooting;
    }

    public void Enter(ScavengerAgent agent)
    {
        Debug.Log("Scavenger Entered: Shooting State");
        agent.navMeshAgent.speed = agent.config.shootingSpeed;
        agent.navMeshAgent.acceleration = agent.config.shootingAcceleration;
        agent.navMeshAgent.angularSpeed = agent.config.shootingAngularSpeed;

        playerGameObject = GameObject.Find("Player");
        scavengerFireGun = agent.GetComponent<ScavengerFireGun>();
        scavengerFireGun.Shoot(agent.config.timeBetweenShots, agent);
    }

    public void Update(ScavengerAgent agent)
    {
        agent.navMeshAgent.SetDestination(playerGameObject.transform.position);
    }

    public void Exit(ScavengerAgent agent)
    {
        agent.navMeshAgent.ResetPath();
        agent.navMeshAgent.velocity = Vector3.zero;
    }
}
