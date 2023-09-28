using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameManager : Singleton<SaveGameManager>
{
    public static SaveData data;

    [SerializeField]
    private List<Resource_ItemData> _resourceList;

    [SerializeField]
    private List<Robot_RecipeData> _robot_RecipeDataList;

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
        data.save_PlayerContracts.Clear();
        data.save_BoardContracts.Clear();
        data._robot_BoardTypeList.Clear();
        data._robot_PlayerTypeList.Clear();
    }

    public static void LoadData(SaveData _data)
    {
        data = _data;
        SaveGameManager.Instance.LoadPlayerInventory(data);
    }

    public static void TryLoadData()
    {
        data = SaveLoad.Load();
        EventBus.Publish(EventType.PLAYER_LOADGAME,data);
        //EventBus.Publish(EventType.ONLOAD);
    }

    private void SaveWorkshopStorage()
    {
        SaveGameManager.data.save_WorkShopStorage = WorkshopManager.Instance.WorkshopStorage;
    }

    private void SavePlayerInventory()
    {
        for (int i = 0; i < data.save_Inventory_SlotAmounts.Length; i++)
        {
            data.save_Inventory_SlotAmounts[i] = Player_InventoryManager.Instance.Inventory_DataArray[i].AmountStored;
            data.save_Inventory_SlotResources[i] = Player_InventoryManager.Instance.Inventory_DataArray[i].SlotItemData.ResourceName;
        }
    }

    private void SavePlayerContract()
    {
        SaveGameManager.data.save_PlayerContracts = Player_Contract_Manager.Instance.Contract_DataList;
        if(data.save_BoardContracts.Count != 0)
            {
            for (int i = 0; i < data.save_BoardContracts.Count; i++)
            {
                data._robot_PlayerTypeList.Add(Player_Contract_Manager.Instance.Contract_DataList[i].RobotType);
            }
        }
    }

    private void SaveBoardContract()
    {
        SaveGameManager.data.save_BoardContracts = ContractBoard_Manager.Instance.Contract_DataList;
        Debug.Log("How many contracts were saved: " + SaveGameManager.data.save_BoardContracts.Count);
        if (data.save_BoardContracts.Count != 0)
        {
            for (int i = 0; i < data.save_BoardContracts.Count; i++)
            {
                data._robot_BoardTypeList.Add(ContractBoard_Manager.Instance.Contract_DataList[i].RobotType);
            }
        }
    }

    private void LoadPlayerInventory(SaveData data)
    {
        for (int i = 0; i < data.save_Inventory_SlotAmounts.Length; i++)
        {
            switch (data.save_Inventory_SlotResources[i])
            {
                case ResourceType.MotherBoard:
                    Player_InventoryManager.Instance.Inventory_DataArray[i].SlotItemData = _resourceList[1];
                    break;
                case ResourceType.Wire:
                    Player_InventoryManager.Instance.Inventory_DataArray[i].SlotItemData = _resourceList[2];
                    break;
                case ResourceType.Oil:
                    Player_InventoryManager.Instance.Inventory_DataArray[i].SlotItemData = _resourceList[3];
                    break;
                case ResourceType.Metal_Scrap:
                    Player_InventoryManager.Instance.Inventory_DataArray[i].SlotItemData = _resourceList[4];
                    break;
                case ResourceType.Advanced_Sensors:
                    Player_InventoryManager.Instance.Inventory_DataArray[i].SlotItemData = _resourceList[5];
                    break;
                case ResourceType.Radioactive_Waste:
                    Player_InventoryManager.Instance.Inventory_DataArray[i].SlotItemData = _resourceList[6];
                    break;
                case ResourceType.Z_Crystal:
                    Player_InventoryManager.Instance.Inventory_DataArray[i].SlotItemData = _resourceList[7];
                    break;
                case ResourceType.Black_Matter:
                    Player_InventoryManager.Instance.Inventory_DataArray[i].SlotItemData = _resourceList[8];
                    break;
                case ResourceType.Empty:
                    Player_InventoryManager.Instance.Inventory_DataArray[i].SlotItemData = _resourceList[0];
                    break;
                default:
                    break;
            }
        }
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
        for (int i = 0; i < data.save_BoardContracts.Count; i++)
        {
            Debug.Log(_robot_RecipeDataList[i]);
            Debug.Log(data.save_BoardContracts[i].Contract_TimerCount);
            Debug.Log("How many contracts am I loading: " + data.save_BoardContracts.Count);
            switch (data._robot_BoardTypeList[i])
            {
                case RobotType.Dog:
                    EventBus.Publish(EventType.BOARD_ADDLOADCONTRACT, _robot_RecipeDataList[0], data.save_BoardContracts[i].Contract_TimerCount);
                    break;
                case RobotType.Cat:
                    EventBus.Publish(EventType.BOARD_ADDLOADCONTRACT, _robot_RecipeDataList[1], data.save_BoardContracts[i].Contract_TimerCount);
                    break;
                case RobotType.Rabbit:
                    break;
                case RobotType.Turtle:
                    break;
                case RobotType.GuinneaPig:
                    break;
                case RobotType.HouseKeep:
                    break;
                case RobotType.ElderCare:
                    break;
                case RobotType.YardMaintenance:
                    break;
                case RobotType.Nurse:
                    break;
                case RobotType.Retail:
                    break;
                case RobotType.BodyGuard:
                    break;
                case RobotType.HouseProtector:
                    break;
                case RobotType.PoliceBot:
                    break;
                case RobotType.MilitaryBot:
                    break;
                case RobotType.FootBall:
                    break;
                default:
                    break;
            }
        }
    }

}
