using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class LootableItemManager : Singleton<LootableItemManager>
{
#if UNITY_EDITOR
    [ArrayElementTitle("prefab")]
#endif
    [SerializeField]
    private List<LootableItemElement> itemPrefabs = new List<LootableItemElement>();


    [SerializeField]
    private List<SpawnLocation> possibleSpawnLocations = new List<SpawnLocation>();

    [SerializeField]
    private int minResources = 1;
    [SerializeField]
    private int maxResources = 40;

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.SPAWN_RESOURCES, SpawnResources);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.SPAWN_RESOURCES, SpawnResources);

    }

    private void Start()
    {
        EventBus.Publish(EventType.SPAWN_RESOURCES);
    }

    public void SpawnResources()
    {
        for (int i = 0; i < itemPrefabs.Count; i++)
        {
            for (int j = 0; j < possibleSpawnLocations.Count; j++)
            {
                //Spawn Item
                possibleSpawnLocations[j].active = itemPrefabs[i].SpawnItem(possibleSpawnLocations[j].location);
                //Idk if this is checking if the location is active or not
            }
        }
    }

}

[System.Serializable]
public class LootableItemElement
{
    public GameObject prefab;
    [Range(0, 1)]
    public float chanceToSpawn = 0f;

    public bool SpawnItem(Transform spawnLocation)
    {
        float spawnChance = Random.Range(0, 2);
        if (spawnChance <= chanceToSpawn)
        {
            GameObject go = ObjectPooler.PullObjectFromPool(prefab);
            go.transform.position = spawnLocation.transform.position;
            go.transform.rotation = spawnLocation.transform.rotation;
            go.SetActive(true);
            return true;
        }
        return false;
    }
}

[System.Serializable]
public class SpawnLocation
{
    public Transform location;
    public bool active { get; set; } = true;


}