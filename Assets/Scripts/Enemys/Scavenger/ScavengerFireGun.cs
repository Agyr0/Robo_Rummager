using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScavengerFireGun : MonoBehaviour
{
    public Transform gunBarrel;
    public ParticleSystem gunParticle;
    public GameObject bulletPrefab;

    public bool firingGun = false;

    public void Shoot(float timeBetweenShots, ScavengerAgent agent)
    {
        if(!firingGun)
        {
            StartCoroutine(FireGun(timeBetweenShots, agent));
            firingGun = true;
        }
    }

    IEnumerator FireGun(float timeBetweenShots, ScavengerAgent agent)
    {
        Debug.Log("Starting To Fire My GUN!");
        int numberOfBullets = Random.Range(agent.config.minShots, agent.config.maxShots);
        Debug.Log("Number of Shots:" + numberOfBullets);

        for (int i = 0; i < numberOfBullets; i++)
        {
            GameObject currBullet = ObjectPooler.PullObjectFromPool(bulletPrefab);

            currBullet.GetComponent<TrailRenderer>().enabled = false;
            currBullet.transform.position = gunBarrel.transform.position;
            currBullet.transform.rotation = gunBarrel.transform.rotation;
            currBullet.GetComponent<TrailRenderer>().enabled = true;
            currBullet.SetActive(true);
            agent.audioManager.PlayClip(agent.audioSource, agent.audioManager.FindRandomizedClip(AudioType.Gun, agent.audioManager.effectAudio));
            gunParticle.Play();

            yield return new WaitForSeconds(timeBetweenShots);
        }
        firingGun = false;
        agent.stateMachine.ChangeState(ScavengerStateId.Reposition);
    }
}
