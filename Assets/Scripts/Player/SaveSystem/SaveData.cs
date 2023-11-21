using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Agyr.Workshop;

namespace SaveLoadSystem
{
    [System.Serializable]

    public class SaveData
    {
        //public SerializableDictionary<string, WorkshopStorage> chestDictionary;
        public WorkshopStorage save_WorkShopStorage;

        public int[] save_Inventory_SlotAmounts;
        public ResourceType[] save_Inventory_SlotResources;

        public int save_Player_SlotCount;
        public int save_Player_CreditPurse;

        public List<Contract_Data> save_PlayerContracts;
        public List<Contract_Data> save_BoardContracts;

        public List<RobotType> _robot_BoardTypeList;
        public List<RobotType> _robot_PlayerTypeList;


        //SaveData constructor
        public SaveData() 
        {
            save_WorkShopStorage = new WorkshopStorage();
            //save_Inventory_SlotAmounts = new int[Player_InventoryManager.Instance.Inventory_DataArray.Count()];
            //save_Inventory_SlotResources = new ResourceType[Player_InventoryManager.Instance.Inventory_DataArray.Count()];
            save_PlayerContracts = new List<Contract_Data>();
            save_BoardContracts = new List<Contract_Data>();
            _robot_BoardTypeList = new List<RobotType>();
            _robot_PlayerTypeList = new List<RobotType>();
            save_Player_SlotCount = 2;
            save_Player_CreditPurse = 0;
            //chestDictionary = new SerializableDictionary<string, WorkshopStorage> ();
        }
    }
}
