using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueBotSpawnManager : MonoBehaviour
{
    private List<SpawnLocationPriority> spawnLocationPriority;
    public int minSpawnAmount;
    public int MaxSpawnAmount;

    // Currently called on start, will want to call when player is leaving the workshop
    private void Start()
    {
        SpawnRogueBots();
    }

    private void SpawnRogueBots()
    {
        for (int i = 0; i < spawnLocationPriority.Count; i++)
        {
        }
        int numberToSpawn = Random.Range(minSpawnAmount, MaxSpawnAmount);
    }
}

[System.Serializable]
public class SpawnLocationPriority 
{
    public GameObject spawnLocation;
    public float priority;
}
