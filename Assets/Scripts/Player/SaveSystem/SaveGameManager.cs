using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agyr.Workshop;

public class SaveGameManager : Singleton<SaveGameManager>
{
    public static SaveData data;

    public override void Awake()
    {
        base.Awake();

        data = new SaveData();
        EventBus.Subscribe<SaveData>(EventType.PLAYER_LOADGAME, LoadData);
        EventBus.Subscribe(EventType.PLAYER_SAVEGAME, SaveWorkshopStorage);
        EventBus.Subscribe<SaveData>(EventType.PLAYER_LOADGAME, LoadWorkshopStorage);
    }

    public void DeleteData()
    {
        SaveLoad.DeleteSaveData();
    }

    public static void SaveData()
    {
        var saveData = data;

        SaveLoad.Save(saveData);
    }

    public static void LoadData(SaveData _data)
    {
        data = _data;
    }

    public static void TryLoadData()
    {
        EventBus.Publish(EventType.PLAYER_LOADGAME,data);
    }

    private void SaveWorkshopStorage()
    {
        SaveGameManager.data.save_WorkShopStorage = WorkshopManager.Instance.WorkshopStorage;
    }

    private void LoadWorkshopStorage(SaveData data)
    {
        WorkshopManager.Instance.WorkshopStorage = data.save_WorkShopStorage;
    }

}
