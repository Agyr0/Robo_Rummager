using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerShootingState : ScavengerState
{
    private int numberOfBullets = 0;
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
        numberOfBullets = Random.Range(agent.config.minShots, agent.config.maxShots);
        Debug.Log(numberOfBullets);
    }

    public void Update(ScavengerAgent agent)
    {
        agent.navMeshAgent.SetDestination(agent.transform.position);

        for (int i = 0; i < numberOfBullets; i++)
        {
            GameObject currBullet = agent.objectPooler.GetPooledObject();

            if (currBullet != null)
            {
                currBullet.transform.position = agent.scavengerWeaponIK.aimTransform.position;
                currBullet.transform.rotation = agent.scavengerWeaponIK.aimTransform.rotation;
                currBullet.SetActive(true);
                currBullet.transform.position += currBullet.transform.forward * agent.config.bulletSpeed;
            }
            float timer = agent.config.timeBetweenShots;
            if(timer <= 0)
            {
                return;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    public void Exit(ScavengerAgent agent)
    {
        agent.navMeshAgent.ResetPath();
        agent.navMeshAgent.velocity = Vector3.zero;
    }
}
