using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoadSystem
{
    [System.Serializable]

    public class SaveData
    {
        //public SerializableDictionary<string, WorkshopStorage> chestDictionary;
        public WorkshopStorage save_WorkShopStorage;
        public Inventory_Slot[] save_Inventory_Slots;
        public List<Contract_Data> save_PlayerContracts;
        public List<Contract_Data> save_BoardContracts;


        //SaveData constructor
        public SaveData() 
        {
            save_WorkShopStorage = new WorkshopStorage();
            save_Inventory_Slots = new Inventory_Slot[Player_InventoryManager.Instance.InventorySlotCount];
            save_PlayerContracts = new List<Contract_Data>();
            save_BoardContracts = new List<Contract_Data>();
            //chestDictionary = new SerializableDictionary<string, WorkshopStorage> ();
        }
    }
}
