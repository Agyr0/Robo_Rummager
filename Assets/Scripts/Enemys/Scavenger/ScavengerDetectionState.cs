using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerDetectionState : ScavengerState
{
    private float timer = 0.0f;
    private Transform playerTransform;
    private GameObject playerGameObject;

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
        playerTransform = GameObject.Find("Player").transform;
        playerGameObject = GameObject.Find("Player");
    }

    public void Update(ScavengerAgent agent)
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            // Keep checking whether or not the player is within the "Shoot" vision cone, change state if so
            //if (agent.scavengerShootingSensor.IsInSight(playerGameObject) == true)
            //    {
            //        //Swap States
            //    }
        }
        else
        {
            // Sets the agents desitination equal to the players destination
            // TODO: need to swap the animations to the gun being raised 
            agent.navMeshAgent.SetDestination(playerTransform.position);
            timer = agent.config.tickRate;
        }
    }

    public void Exit(ScavengerAgent agent)
    {
    }
}
