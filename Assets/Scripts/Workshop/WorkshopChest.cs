using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agyr.Workshop
{
    public class WorkshopChest : MonoBehaviour, IInteractable
    {
        private Player_InventoryManager inventoryManager;
        private WorkshopManager workshopManager;

        private void Start()
        {
            workshopManager = WorkshopManager.Instance;
            inventoryManager = GameManager.Instance.inventoryManager;
        }

        public void HandleInteract()
        {
            //Transfer all items in Inventory_DataArray into WorkshopStorage
            for (int i = 0; i < inventoryManager.Inventory_DataArray.Length; i++)
            {
                switch (inventoryManager.Inventory_DataArray[i].SlotItemData.ResourceName)
                {
                    case ResourceType.MotherBoard:
                        workshopManager.WorkshopStorage.MotherBoardCount += inventoryManager.Inventory_DataArray[i].AmountStored;
                        break;
                    case ResourceType.Wire:
                        workshopManager.WorkshopStorage.WireCount += inventoryManager.Inventory_DataArray[i].AmountStored;
                        break;
                    case ResourceType.Oil:
                        workshopManager.WorkshopStorage.OilCount += inventoryManager.Inventory_DataArray[i].AmountStored;
                        break;
                    case ResourceType.Metal_Scrap:
                        workshopManager.WorkshopStorage.MetalScrapCount += inventoryManager.Inventory_DataArray[i].AmountStored;
                        break;
                    case ResourceType.Advanced_Sensors:
                        workshopManager.WorkshopStorage.SensorCount += inventoryManager.Inventory_DataArray[i].AmountStored;
                        break;
                    case ResourceType.Radioactive_Waste:
                        workshopManager.WorkshopStorage.RadioactiveWasteCount += inventoryManager.Inventory_DataArray[i].AmountStored;
                        break;
                    case ResourceType.Z_Crystal:
                        workshopManager.WorkshopStorage.ZCrystalCount += inventoryManager.Inventory_DataArray[i].AmountStored;
                        break;
                    case ResourceType.Black_Matter:
                        workshopManager.WorkshopStorage.BlackMatterCount += inventoryManager.Inventory_DataArray[i].AmountStored;
                        break;
                    case ResourceType.Empty:
                        break;
                    default:
                        break;
                }
                //Clear out the Data Array
                inventoryManager.Inventory_DataArray[i].SlotItemData = inventoryManager.ResourceEmpty;
                inventoryManager.Inventory_DataArray[i].AmountStored = 0;
                EventBus.Publish(EventType.INVENTORY_UPDATE, inventoryManager.gameObject);
            }
            //Transfer credits and clear inventory credits
            workshopManager.WorkshopStorage.CreditCount += inventoryManager.CreditPurse;
            inventoryManager.CreditPurse = 0;
        }

    }
}