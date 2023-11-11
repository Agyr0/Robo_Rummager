using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class ScavengerSpawnManager : MonoBehaviour
{
    public GameObject scavengerPrefab;
    public List<GameObject> spawnLocations;
    public int numberOfScavengersToSpawn;

    public void Start()
    {
        SpawnScavengers();
    }

    public void SpawnScavengers()
    {
        for (int i = 0; i < numberOfScavengersToSpawn; i++)
        {
            // Set the Spawn Location
            GameObject randomSpawnPoint = spawnLocations[Random.Range(0, spawnLocations.Count)];
            GameObject scavenger = ObjectPooler.PullObjectFromPool(scavengerPrefab);

            if (scavenger != null)
            {
                scavenger.transform.position = randomSpawnPoint.transform.position;
                scavenger.transform.rotation = randomSpawnPoint.transform.rotation;
                scavenger.SetActive(true);
                scavenger.GetComponent<NavMeshAgent>().enabled = true;
            }
            spawnLocations.Remove(randomSpawnPoint);
        }
    }
}
