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
    private List<Transform> possibleSpawnLocations = new List<Transform>();

    [SerializeField]
    private int minResources = 1;
    [SerializeField]
    private int maxResources = 40;

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.TEST_EVENT_1, SpawnResources);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.TEST_EVENT_1, SpawnResources);

    }
    public void SpawnResources()
    {
        //Initialize all poolers
        for (int i = 0; i < itemPrefabs.Count; i++)
        {
            itemPrefabs[i].InitializePooler(maxResources, gameObject);
            GameObject go = Instantiate(itemPrefabs[i]._myPooler.GetPooledObject());
        }
    }

}

[System.Serializable]
public class LootableItemElement
{
    public GameObject prefab;
    [Range(0, 1)]
    public float chanceToSpawn = 0f;

    public ObjectPooler _myPooler;


    public void InitializePooler(int maxObjects, GameObject self)
    {
        _myPooler = self.AddComponent<ObjectPooler>();
        _myPooler.objectPrefab = prefab;
        _myPooler.maxObjects = maxObjects;
    }

}