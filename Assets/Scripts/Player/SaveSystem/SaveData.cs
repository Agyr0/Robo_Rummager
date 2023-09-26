using System;
using UnityEngine;
using Agyr.Workshop;

namespace SaveLoadSystem
{
    [System.Serializable]

    public class SaveData
    {
        //public SerializableDictionary<string, WorkshopStorage> chestDictionary;
        public WorkshopStorage save_WorkShopStorage;
        public SaveData() 
        {
            save_WorkShopStorage = new WorkshopStorage();
            //chestDictionary = new SerializableDictionary<string, WorkshopStorage> ();
        }
    }
}
