using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorkshopZone
{
    public LootableItemManager lootableItemManager;

    public bool ResourcesAvailable()
    {
        //Get ref to lootableItemManager
        if (lootableItemManager == null)
            lootableItemManager = LootableItemManager.Instance;

        //Loop through spawned resources and check if still in scene
        for (int i = 0; i < lootableItemManager.possibleSpawnLocations.Count; i++)
        {
            //If spawn location doesnt have an object: continue
            if (lootableItemManager.possibleSpawnLocations[i].active)
                continue;

            //If the lootable item is active in the scene return true
            if (lootableItemManager.possibleSpawnLocations[i].myCurObject.activeInHierarchy)
                return true;
        }

        //If made it through the for loop all spawned in lootable items are not active in scene
        //so return true
        return false;
    }
}
