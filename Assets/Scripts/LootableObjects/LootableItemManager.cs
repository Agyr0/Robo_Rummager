using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agyr.CustomAttributes;

public class LootableItemManager : Singleton<LootableItemManager>
{
    [SerializeField]
    private int minResources = 1;
    [HideInInspector]
    public int curNumResources = 0;
    [Space(15)]

#if UNITY_EDITOR
    [ArrayElementTitle("prefab")]
#endif
    [SerializeField]
    private List<LootableItemElement> itemPrefabs = new List<LootableItemElement>();


    [SerializeField]
    public List<SpawnLocation> possibleSpawnLocations = new List<SpawnLocation>();


    private int maxResources;

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
        maxResources = possibleSpawnLocations.Count;
        if (minResources > maxResources)
            minResources = maxResources;

        EventBus.Publish(EventType.SPAWN_RESOURCES);
    }

    private void SpawnResources()
    {
        do
        {
            for (int i = 0; i < itemPrefabs.Count; i++)
            {
                for (int j = 0; j < possibleSpawnLocations.Count; j++)
                {
                    if (curNumResources >= maxResources)
                        return;

                    //Spawn Item
                    if (possibleSpawnLocations[j].active)
                        possibleSpawnLocations[j].active = !itemPrefabs[i].SpawnItem(possibleSpawnLocations[j].location, possibleSpawnLocations[j]);
                }
            }
        }
        while (curNumResources <= minResources);
            
    }

    private void DespawnResources()
    {
        for (int i = 0; i < possibleSpawnLocations.Count; i++)
        {
            if (possibleSpawnLocations[i].active)
                continue;

            LootBag item = possibleSpawnLocations[i].myCurObject.GetComponent<LootBag>();
            item.RefreshDrops();

            possibleSpawnLocations[i].myCurObject.SetActive(false);
            possibleSpawnLocations[i].active = true;
        }

        curNumResources = 0;
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
            LootableItemManager.Instance.curNumResources++;
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
#if UNITY_EDITOR
    [HideInInspector]
    [ReadOnly]
#endif
    public GameObject myCurObject = null;
#if UNITY_EDITOR
    [ReadOnly]
#endif
    public bool active = true;

}