using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScavengerFireGun : MonoBehaviour
{
    public Transform gunBarrel;
    public ParticleSystem gunParticle;
    public GameObject bulletPrefab;

    public void Shoot(float timeBetweenShots, ScavengerAgent agent)
    {
        StartCoroutine(FireGun(timeBetweenShots, agent));
    }

    IEnumerator FireGun(float timeBetweenShots, ScavengerAgent agent)
    {
        int numberOfBullets = Random.Range(agent.config.minShots, agent.config.maxShots);
        Debug.Log("Number of Shots:" + numberOfBullets);

        for (int i = 0; i < numberOfBullets; i++)
        {
            GameObject currBullet = ObjectPooler.PullObjectFromPool(bulletPrefab);
            Debug.LogError("Tucker Changed the object pooler here");

            currBullet.transform.position = gunBarrel.transform.position;
            currBullet.transform.rotation = gunBarrel.transform.rotation;
            currBullet.SetActive(true);
            gunParticle.Play();

            yield return new WaitForSeconds(timeBetweenShots);
        }
        agent.stateMachine.ChangeState(ScavengerStateId.Reposition);
    }
}
