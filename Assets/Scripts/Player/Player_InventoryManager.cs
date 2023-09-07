using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Player_InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _inventory_UI;

    [SerializeField]
    private GameObject _inventory_HUD_UI;

    [SerializeField]
    private Inventory_Slot[] Inventory_DataArray;

    [SerializeField]
    private GameObject[] InventoryItemDropDialougeArray;

    [SerializeField]
    private GameObject[] Inventory_SlotArray;

    [SerializeField]
    private GameObject[] InventoryHUD_SlotArray;

    [SerializeField]
    private int _slotStackLimit;

    [SerializeField]
    private int _inventorySlotMax;

    [SerializeField]
    private int _inventorySlotMin;

    [SerializeField]
    private Resource_ItemData _resourceEmpty;

    [SerializeField]
    private GameObject _itemBlankPrefab;

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
        _inventory_HUD_UI.SetActive(false);
    }

    private void OnHideInventory()
    {
        _inventory_UI.SetActive(false);
        _inventory_HUD_UI.SetActive(true);
    }

    private void OnInventoryAddSlot()
    {
        if (_inventorySlotCount <= _inventorySlotMax)
        {
            InventoryHUD_SlotArray[_inventorySlotCount].SetActive(true);
            Inventory_SlotArray[_inventorySlotCount].SetActive(true);
            _inventorySlotCount++;
        }
    }

    private void OnInventoryRemoveSlot()
    {
        if (_inventorySlotCount > _inventorySlotMin)
        {
            InventoryHUD_SlotArray[_inventorySlotCount-1].SetActive(false);
            Inventory_SlotArray[_inventorySlotCount-1].SetActive(false);
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
                if (Inventory_SlotArray[i].GetComponent<Inventory_Slot>().ItemStored == ResourceType.Empty
                    || Inventory_SlotArray[i].GetComponent<Inventory_Slot>().ItemStored == itemPicked.GetComponent<Resource_Item>().ItemData.ResourceName)
                {
                    Inventory_SlotArray[i].GetComponent<Inventory_Slot>().ItemIcon.sprite = itemPicked.GetComponent<Resource_Item>().ItemData.ResourceIcon;
                    if (Inventory_SlotArray[i].GetComponent<Inventory_Slot>().AmountStored != 25)
                    {
                        //checks if adding the resource amount would exceed the slot stack limit
                        if (Inventory_SlotArray[i].GetComponent<Inventory_Slot>().AmountStored +
                            itemPicked.GetComponent<Resource_Item>().ResourceAmount > _slotStackLimit)
                        {
                            itemPicked.GetComponent<Resource_Item>().ResourceAmount = (Inventory_SlotArray[i].GetComponent<Inventory_Slot>().AmountStored +
                            itemPicked.GetComponent<Resource_Item>().ResourceAmount) - _slotStackLimit;

                            Inventory_SlotArray[i].GetComponent<Inventory_Slot>().ItemStored = itemPicked.GetComponent<Resource_Item>().ItemData.ResourceName;
                            Inventory_SlotArray[i].GetComponent<Inventory_Slot>().AmountStored = _slotStackLimit;

                            Inventory_SlotArray[i].GetComponent<Inventory_Slot>().SlotItemData = itemPicked.GetComponent<Resource_Item>().ItemData;

                            Inventory_SlotArray[i].GetComponent<Inventory_Slot>().ItemIcon.sprite = itemPicked.GetComponent<Resource_Item>().ItemData.ResourceIcon;

                            InventoryHUD_SlotArray[i].GetComponent<Inventory_Slot>().ItemStored = Inventory_SlotArray[i].GetComponent<Inventory_Slot>().ItemStored;
                            InventoryHUD_SlotArray[i].GetComponent<Inventory_Slot>().AmountStored = Inventory_SlotArray[i].GetComponent<Inventory_Slot>().AmountStored;
                            InventoryHUD_SlotArray[i].GetComponent<Inventory_Slot>().ItemIcon.sprite = Inventory_SlotArray[i].GetComponent<Inventory_Slot>().ItemIcon.sprite;

                        }
                        else
                        {
                            Inventory_SlotArray[i].GetComponent<Inventory_Slot>().ItemStored = itemPicked.GetComponent<Resource_Item>().ItemData.ResourceName;
                            Inventory_SlotArray[i].GetComponent<Inventory_Slot>().AmountStored += itemPicked.GetComponent<Resource_Item>().ResourceAmount;
                            Inventory_SlotArray[i].GetComponent<Inventory_Slot>().ItemIcon.sprite = Inventory_SlotArray[i].GetComponent<Inventory_Slot>().ItemIcon.sprite;
                            Inventory_SlotArray[i].GetComponent<Inventory_Slot>().SlotItemData = itemPicked.GetComponent<Resource_Item>().ItemData;

                            InventoryHUD_SlotArray[i].GetComponent<Inventory_Slot>().ItemStored = Inventory_SlotArray[i].GetComponent<Inventory_Slot>().ItemStored;
                            InventoryHUD_SlotArray[i].GetComponent<Inventory_Slot>().AmountStored = Inventory_SlotArray[i].GetComponent<Inventory_Slot>().AmountStored;
                            InventoryHUD_SlotArray[i].GetComponent<Inventory_Slot>().ItemIcon.sprite = Inventory_SlotArray[i].GetComponent<Inventory_Slot>().ItemIcon.sprite;
                            itemPicked.GetComponent<Resource_Item>().ResourceAmount = 0;
                            break;
                        }
                    }
                }
            }
        }
        
        if (itemPicked.GetComponent<Resource_Item>().ResourceAmount == 0)
        {
            itemPicked.gameObject.SetActive(false);
        }
        EventBus.Publish(EventType.INVENTORY_UPDATE);
    }

    public void OnButtonPress(int slotNumber)
    {
        if (Inventory_SlotArray[slotNumber].GetComponent<Inventory_Slot>().ItemStored != ResourceType.Empty)
        {
            InventoryItemDropDialougeArray[slotNumber].SetActive(true);
        }
    }

    public void OnItemDrop(int slotNumber)
    {
        //EventBus.Publish<InventorySlot>(EventType.INVENTORY_ITEMDROPPED, ItemDropped);
        Inventory_Slot ItemData;
        ItemData = Inventory_SlotArray[slotNumber].GetComponent<Inventory_Slot>();

        GameObject tempItemBlank;
        tempItemBlank = Instantiate(_itemBlankPrefab, this.transform.position, this.transform.rotation);
        tempItemBlank.GetComponent<Resource_Item>().ItemData = ItemData.SlotItemData;
        tempItemBlank.GetComponent<Resource_Item>().ResourceAmount = ItemData.AmountStored;

        Inventory_SlotArray[slotNumber].GetComponent<Inventory_Slot>().ItemIcon.sprite = _resourceEmpty.ResourceIcon;
        Inventory_SlotArray[slotNumber].GetComponent<Inventory_Slot>().ItemStored = ResourceType.Empty;
        Inventory_SlotArray[slotNumber].GetComponent<Inventory_Slot>().AmountStored = 0;
        InventoryItemDropDialougeArray[slotNumber].SetActive(false);
        EventBus.Publish(EventType.INVENTORY_UPDATE);
    }

    public void ItemDropped(Inventory_Slot ItemData)
    {
        GameObject tempItemBlank;
        tempItemBlank = Instantiate(_itemBlankPrefab, this.transform.position, this.transform.rotation);
        tempItemBlank.GetComponent<Resource_Item>().ItemData = ItemData.SlotItemData;
        tempItemBlank.GetComponent<Resource_Item>().ResourceAmount = ItemData.AmountStored;
    }

    public void OnItemDropCanceled(int slotNumber)
    {
        InventoryItemDropDialougeArray[slotNumber].SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        EventBus.Publish<GameObject>(EventType.INVENTORY_PICKUP, other.gameObject);
    }
}
