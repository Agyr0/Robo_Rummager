using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DefaultExecutionOrder(2)]
public class RogueBotSpawnManager : MonoBehaviour
{

    public GameObject rougeBot;
    public List<Transform> spawnPoints;

    public int numberOfObjectsToSpawn = 5;


    void Start()
    {
        SpawnRogueBots();
    }

    void SpawnRogueBots()
    {
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            GameObject robot = ObjectPooler.PullObjectFromPool(rougeBot);

            if (robot != null)
            {
                robot.transform.position = randomSpawnPoint.transform.position;
                robot.transform.rotation = randomSpawnPoint.transform.rotation;
                robot.SetActive(true);
            }
            spawnPoints.Remove(randomSpawnPoint);
        }
    }
}
