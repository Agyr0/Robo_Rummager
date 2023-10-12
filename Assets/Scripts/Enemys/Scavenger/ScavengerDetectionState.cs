using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerDetectionState : ScavengerState
{
    private float timer = 0.0f;
    private float detectionStateTimer;
    private GameObject playerGameObject;
    private ScavengerWeaponIK weaponIK;

    public ScavengerStateId GetId()
    {
        return ScavengerStateId.Detection;
    }

    public void Enter(ScavengerAgent agent)
    {
        Debug.Log("Scavenger Entered: Detection State");
        agent.navMeshAgent.speed = agent.config.detectionSpeed;
        agent.navMeshAgent.acceleration = agent.config.detectionAcceleration;
        agent.navMeshAgent.angularSpeed = agent.config.detectionAngularSpeed;

        playerGameObject = GameObject.Find("Player");
        detectionStateTimer = agent.config.detectionStateMaxTime;
        weaponIK = agent.GetComponent<ScavengerWeaponIK>();
        weaponIK.enabled = true;
    }

    public void Update(ScavengerAgent agent)
    {
        detectionStateTimer -= Time.deltaTime;
        timer -= Time.deltaTime;

        // Check if player is in shooting cone to determine if agent should swap to detection state
        if (agent.scavengerSensor.playersShooting.Count >= 1)
        {
            agent.stateMachine.ChangeState(ScavengerStateId.Shooting);
        }

        if (detectionStateTimer < 0.0f)
        {
            agent.stateMachine.ChangeState(ScavengerStateId.Patrol);
        }

        if (timer < 0.0f)
        {
            // Sets the agents desitination equal to the players destination
            // TODO: need to swap the animations to the gun being raised 
            // Chasing Logic
            agent.navMeshAgent.SetDestination(playerGameObject.transform.position);
            timer = agent.config.tickRate;
        }
    }

    public void Exit(ScavengerAgent agent)
    {
        agent.navMeshAgent.ResetPath();
        agent.navMeshAgent.velocity = Vector3.zero;
        weaponIK.enabled = false;
    }
}
