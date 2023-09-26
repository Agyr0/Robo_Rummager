using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameManager : Singleton<SaveGameManager>
{
    public static SaveData data;

    public override void Awake()
    {
        base.Awake();

        data = new SaveData();

        EventBus.Subscribe(EventType.PLAYER_SAVEGAME, SaveWorkshopStorage);
        EventBus.Subscribe(EventType.PLAYER_SAVEGAME, SavePlayerInventory);
        EventBus.Subscribe(EventType.PLAYER_SAVEGAME, SavePlayerContract);
        EventBus.Subscribe(EventType.PLAYER_SAVEGAME, SaveBoardContract);

        EventBus.Subscribe<SaveData>(EventType.PLAYER_LOADGAME, LoadData);
        EventBus.Subscribe<SaveData>(EventType.PLAYER_LOADGAME, LoadWorkshopStorage);
        EventBus.Subscribe<SaveData>(EventType.PLAYER_LOADGAME, LoadPlayerContracts);
        EventBus.Subscribe<SaveData>(EventType.PLAYER_LOADGAME, LoadBoardContracts);
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
        EventBus.Publish(EventType.ONLOAD);
    }

    private void SaveWorkshopStorage()
    {
        SaveGameManager.data.save_WorkShopStorage = WorkshopManager.Instance.WorkshopStorage;
    }

    private void SavePlayerInventory()
    {
        SaveGameManager.data.save_Inventory_Slots = Player_InventoryManager.Instance.Inventory_DataArray;
    }

    private void SavePlayerContract()
    {
        SaveGameManager.data.save_PlayerContracts = Player_Contract_Manager.Instance.Contract_DataList;
    }

    private void SaveBoardContract()
    {
        SaveGameManager.data.save_BoardContracts = ContractBoard_Manager.Instance.Contract_DataList;
    }

    private void LoadWorkshopStorage(SaveData data)
    {
        Debug.Log("Workshop");
        WorkshopManager.Instance.WorkshopStorage = data.save_WorkShopStorage;
    }

    private void LoadPlayerContracts(SaveData data)
    {
        Debug.Log("ContractP");
        Player_Contract_Manager.Instance.Contract_DataList = data.save_PlayerContracts;
    }

    private void LoadBoardContracts(SaveData data)
    {
        Debug.Log("ContractB");
        ContractBoard_Manager.Instance.Contract_DataList = data.save_BoardContracts;
    }

}
