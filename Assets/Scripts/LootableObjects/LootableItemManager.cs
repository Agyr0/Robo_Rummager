using System.Collections;
using System.Collections.Generic;
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
        EventBus.Subscribe(EventType.REFRESH_RESOURCES, RefreshResources);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.SPAWN_RESOURCES, SpawnResources);
        EventBus.Unsubscribe(EventType.REFRESH_RESOURCES, RefreshResources);

    }

    private void Start()
    {
        EventBus.Publish(EventType.SPAWN_RESOURCES);
    }

    private void SpawnResources()
    {
        for (int i = 0; i < itemPrefabs.Count; i++)
        {
            for (int j = 0; j < possibleSpawnLocations.Count; j++)
            {
                //Spawn Item
                if (possibleSpawnLocations[j].Active)
                    possibleSpawnLocations[j].Active = !itemPrefabs[i].SpawnItem(possibleSpawnLocations[j].location, possibleSpawnLocations[i]);

                //Just to see it in the inspector
                possibleSpawnLocations[j].active = possibleSpawnLocations[j].Active;


                if (possibleSpawnLocations[j].Active)
                    break;
            }
        }
    }

    private void DespawnResources()
    {
        for (int i = 0; i < possibleSpawnLocations.Count; i++)
        {
            if (!possibleSpawnLocations[i].Active)
                break;

            LootBag item = possibleSpawnLocations[i].myCurObject.GetComponent<LootBag>();
            item.RefreshDrops();

            possibleSpawnLocations[i].myCurObject.SetActive(false);
        }
    }

    private void RefreshResources()
    {
        DespawnResources();
        SpawnResources();
    }

}

[System.Serializable]
public class LootableItemElement
{
    public GameObject prefab;
    [Range(0, 1)]
    public float chanceToSpawn = 0f;


    public bool SpawnItem(Transform spawnLocation, SpawnLocation locationClass)
    {
        float spawnChance = Random.Range(0, 2);
        if (spawnChance <= chanceToSpawn)
        {
            GameObject go = ObjectPooler.PullObjectFromPool(prefab);
            go.transform.position = spawnLocation.transform.position;
            go.transform.rotation = spawnLocation.transform.rotation;
            locationClass.myCurObject = go;
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
    public bool active;
    [HideInInspector]
    public GameObject myCurObject = null;
    public bool Active { get; set; } = true;

}