using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agyr.Workshop;

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

        EventBus.Subscribe<SaveData>(EventType.PLAYER_LOADGAME, LoadWorkshopStorage);
        EventBus.Subscribe<SaveData>(EventType.PLAYER_LOADGAME, LoadPlayerContracts);
        EventBus.Subscribe<SaveData>(EventType.PLAYER_LOADGAME, LoadBoardContracts);
    }

    public void DeleteData(string saveID)
    {
        SaveLoad.DeleteSaveData(saveID);
    }

    public static void SaveData(string saveID)
    {
        var saveData = data;

        SaveLoad.Save(saveData, saveID);
    }

    public static void TryLoadData(string saveID)
    {
        data = SaveLoad.Load(saveID);
        SaveGameManager.Instance.LoadPlayerInventory(data);

        EventBus.Publish(EventType.SAVECONTRACTPURGE);
        EventBus.Publish(EventType.PLAYER_LOADGAME,data);
        EventBus.Publish(EventType.ONLOAD);
    }

    private void SaveWorkshopStorage()
    {
        SaveGameManager.data.save_WorkShopStorage = WorkshopManager.Instance.WorkshopStorage;
    }

    private void SavePlayerInventory()
    {
        for (int i = 0; i < Player_InventoryManager.Instance.Inventory_DataArray.Length; i++)
        {
            data.save_Inventory_SlotAmounts[i] = Player_InventoryManager.Instance.Inventory_DataArray[i].AmountStored;
            data.save_Inventory_SlotResources[i] = Player_InventoryManager.Instance.Inventory_DataArray[i].SlotItemData.ResourceName;
        }
        data.save_Player_CreditPurse = Player_InventoryManager.Instance.CreditPurse;
        data.save_Player_SlotCount = Player_InventoryManager.Instance.InventorySlotCount;
    }

    private void SavePlayerContract()
    {
        data._robot_PlayerTypeList.Clear();
        SaveGameManager.data.save_PlayerContracts = Player_Contract_Manager.Instance.Contract_DataList;
        if (data.save_PlayerContracts.Count != 0)
        {
            for (int i = 0; i < data.save_PlayerContracts.Count; i++)
            {
                data._robot_PlayerTypeList.Add(Player_Contract_Manager.Instance.Contract_DataList[i].RobotType);
            }
        }
        
    }

    private void SaveBoardContract()
    {
        data._robot_BoardTypeList.Clear();
        SaveGameManager.data.save_BoardContracts = ContractBoard_Manager.Instance.Contract_DataList;
        if (data.save_BoardContracts.Count != 0)
        {
            for (int i = 0; i < data.save_BoardContracts.Count; i++)
            {
                Debug.Log(ContractBoard_Manager.Instance.Contract_DataList[i].RobotType);
                
                data._robot_BoardTypeList.Add(ContractBoard_Manager.Instance.Contract_DataList[i].RobotType);
                Debug.Log(data._robot_BoardTypeList[i]);
            }
        }
        
    }

    private void LoadPlayerInventory(SaveData data)
    {
        for (int i = 0; i < data.save_Inventory_SlotAmounts.Length; i++)
        {
            Player_InventoryManager.Instance.Inventory_DataArray[i].AmountStored = data.save_Inventory_SlotAmounts[i];
            switch (data.save_Inventory_SlotResources[i])
            {
                case ResourceType.Empty:
                    Player_InventoryManager.Instance.Inventory_DataArray[i].SlotItemData = _resourceList[0];
                    break;
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
                default:
                    break;
            }
        }
        Player_InventoryManager.Instance.CreditPurse = data.save_Player_CreditPurse;
        Player_InventoryManager.Instance.InventorySlotCount = data.save_Player_SlotCount;
    }

    private void LoadWorkshopStorage(SaveData data)
    {
        WorkshopManager.Instance.WorkshopStorage = data.save_WorkShopStorage;
    }

    private void LoadPlayerContracts(SaveData data)
    {
        Player_Contract_Manager.Instance.Contract_DataList.Clear();
        for (int i = 0; i < data.save_PlayerContracts.Count; i++)
        {
            switch (data._robot_PlayerTypeList[i])
            {
                case RobotType.Dog:
                    EventBus.Publish(EventType.PLAYER_LOADCONTRACT, _robot_RecipeDataList[0], data.save_PlayerContracts[i].Contract_TimerCount);
                    break;
                case RobotType.Cat:
                    EventBus.Publish(EventType.PLAYER_LOADCONTRACT, _robot_RecipeDataList[1], data.save_PlayerContracts[i].Contract_TimerCount);
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

    private void LoadBoardContracts(SaveData data)
    {
        ContractBoard_Manager.Instance.Contract_DataList.Clear();
        for (int i = 0; i < data.save_BoardContracts.Count; i++)
        {
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
