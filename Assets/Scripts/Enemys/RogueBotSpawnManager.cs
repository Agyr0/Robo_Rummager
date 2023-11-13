using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[DefaultExecutionOrder(2)]
public class RogueBotSpawnManager : MonoBehaviour
{
    public GameObject rougeBotPrefab;
    public List<Transform> spawnLocations;
    public int numberOfRogueBotsToSpawn;

    void Start()
    {
        SpawnRogueBots();
    }

    void SpawnRogueBots()
    {
        for (int i = 0; i < numberOfRogueBotsToSpawn; i++)
        {
            Transform randomSpawnPoint = spawnLocations[Random.Range(0, spawnLocations.Count)];
            GameObject robot = ObjectPooler.PullObjectFromPool(rougeBotPrefab);

            if (robot != null)
            {
                robot.transform.position = randomSpawnPoint.position;
                robot.transform.rotation = randomSpawnPoint.rotation;
                robot.SetActive(true);
                robot.GetComponent<NavMeshAgent>().enabled = true;
            }
            spawnLocations.Remove(randomSpawnPoint);
        }
    }
}
