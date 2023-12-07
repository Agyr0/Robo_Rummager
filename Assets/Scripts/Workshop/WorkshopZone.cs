using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorkshopZone
{
    [HideInInspector]
    public LootableItemManager lootableItemManager;

    [SerializeField, Range(0,1)]
    private float percentResourcesLeftBeforeRespawn = .25f;

    public bool ResourcesAvailable()
    {

        float resourcesFound = 0;

        //Get ref to lootableItemManager
        if (lootableItemManager == null)
            lootableItemManager = LootableItemManager.Instance;

        //Loop through spawned resources and check if still in scene
        for (int i = 0; i < lootableItemManager.possibleSpawnLocations.Count; i++)
        {
            //If spawn location doesnt have an object: continue
            if (lootableItemManager.possibleSpawnLocations[i].active)
                continue;

            //If the lootable item is active in the scene
            if (lootableItemManager.possibleSpawnLocations[i].myCurObject.activeInHierarchy)
                resourcesFound++;
        }

        //If there are more resources in the map than the percentResourcesLeftBeforeRespawn
        if ((resourcesFound/ lootableItemManager.curNumResources) > percentResourcesLeftBeforeRespawn)
            return true;
        

        
        return false;
    }
}
