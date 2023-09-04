using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _inventory_UI;

    [SerializeField]
    private GameObject[] InventorySlotArray;

    [SerializeField]
    private int _slotStackLimit;

    [SerializeField]
    private int _inventorySlotMax;

    [SerializeField]
    private int _inventorySlotMin;

    //Current method of keeping track of players inventory slot counts untill
    //a player data script is implemented.

    [SerializeField]
    private int _inventorySlotCount;

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.INVENTORY_TOGGLE, OnToggleInventory);
        EventBus.Subscribe(EventType.INVENTORY_ADDSLOT, OnInventoryAddSlot);
        EventBus.Subscribe(EventType.INVENTORY_REMOVESLOT, OnInventoryRemoveSlot);
        EventBus.Subscribe<GameObject>(EventType.INVENTORY_PICKUP, OnItemPickup);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.INVENTORY_TOGGLE, OnToggleInventory);
        EventBus.Unsubscribe(EventType.INVENTORY_ADDSLOT, OnInventoryAddSlot);
        EventBus.Unsubscribe(EventType.INVENTORY_REMOVESLOT, OnInventoryRemoveSlot);
        EventBus.Unsubscribe<GameObject>(EventType.INVENTORY_PICKUP, OnItemPickup);
    }

    private void OnToggleInventory()
    {
        if (_inventory_UI.activeSelf)
        {
            OnHideInventory();
        }
        else
        {
            OnDisplayInventory();
        }
    }

    private void OnDisplayInventory()
    {
        _inventory_UI.SetActive(true);
    }

    private void OnHideInventory()
    {
        _inventory_UI.SetActive(false);
    }

    private void OnInventoryAddSlot()
    {
        if (_inventorySlotCount <= _inventorySlotMax)
        {
            InventorySlotArray[_inventorySlotCount].SetActive(true);
            _inventorySlotCount++;
        }
    }

    private void OnInventoryRemoveSlot()
    {
        if (_inventorySlotCount > _inventorySlotMin)
        {
            InventorySlotArray[_inventorySlotCount-1].SetActive(false);
            _inventorySlotCount--;
        }
    }

    private void OnItemPickup(GameObject itemPicked)
    {
        for (int i = 0; i < _inventorySlotCount; i++)
        {
            //checks for a zero amount resource
            if (itemPicked.GetComponent<Resource_Item>().ResourceAmount != 0)
            {
                //Looks for an empty or matching resource slot
                if (InventorySlotArray[i].GetComponent<Inventory_Slot>().ItemStored == ResourceType.Empty
                    || InventorySlotArray[i].GetComponent<Inventory_Slot>().ItemStored == itemPicked.GetComponent<Resource_Item>().ItemData.ResourceName)
                {
                    if (InventorySlotArray[i].GetComponent<Inventory_Slot>().AmountStored != 25)
                    {
                        //checks if adding the resource amount would exceed the slot stack limit
                        if (InventorySlotArray[i].GetComponent<Inventory_Slot>().AmountStored +
                            itemPicked.GetComponent<Resource_Item>().ResourceAmount > _slotStackLimit)
                        {
                            itemPicked.GetComponent<Resource_Item>().ResourceAmount = (InventorySlotArray[i].GetComponent<Inventory_Slot>().AmountStored +
                            itemPicked.GetComponent<Resource_Item>().ResourceAmount) - _slotStackLimit;
                            InventorySlotArray[i].GetComponent<Inventory_Slot>().ItemStored = itemPicked.GetComponent<Resource_Item>().ItemData.ResourceName;
                            InventorySlotArray[i].GetComponent<Inventory_Slot>().AmountStored = _slotStackLimit;
                        }
                        else
                        {
                            InventorySlotArray[i].GetComponent<Inventory_Slot>().ItemStored = itemPicked.GetComponent<Resource_Item>().ItemData.ResourceName;
                            InventorySlotArray[i].GetComponent<Inventory_Slot>().AmountStored += itemPicked.GetComponent<Resource_Item>().ResourceAmount;
                            break;
                        }
                    }
                }
            }
        }
        if (itemPicked.GetComponent<Resource_Item>().ResourceAmount == 0)
        {
            itemPicked.SetActive(false);
        }
        EventBus.Publish(EventType.INVENTORY_UPDATE);
    }
}
