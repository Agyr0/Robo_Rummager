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
    private Inventory_Slot[] _inventory_DataArray;

    [SerializeField]
    private GameObject[] _inventoryItemDropDialougeArray;

    [SerializeField]
    private GameObject[] _inventory_SlotArray;

    [SerializeField]
    private GameObject[] _inventoryHUD_SlotArray;

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

    public Inventory_Slot[] Inventory_DataArray
    {
        get { return _inventory_DataArray; }
        set { _inventory_DataArray = value; }
    }

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
            _inventoryHUD_SlotArray[_inventorySlotCount].SetActive(true);
            _inventory_SlotArray[_inventorySlotCount].SetActive(true);
            _inventorySlotCount++;
        }
    }

    private void OnInventoryRemoveSlot()
    {
        if (_inventorySlotCount > _inventorySlotMin)
        {
            _inventoryHUD_SlotArray[_inventorySlotCount-1].SetActive(false);
            _inventory_SlotArray[_inventorySlotCount-1].SetActive(false);
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
                if (Inventory_DataArray[i].SlotItemData.ResourceName == ResourceType.Empty
                    || Inventory_DataArray[i].SlotItemData.ResourceName == itemPicked.GetComponent<Resource_Item>().ItemData.ResourceName)
                {
                    Inventory_DataArray[i].SlotItemData = itemPicked.GetComponent<Resource_Item>().ItemData;
                    if (Inventory_DataArray[i].AmountStored != 25)
                    {
                        //checks if adding the resource amount would exceed the slot stack limit
                        if (Inventory_DataArray[i].AmountStored +
                            itemPicked.GetComponent<Resource_Item>().ResourceAmount > _slotStackLimit)
                        {
                            itemPicked.GetComponent<Resource_Item>().ResourceAmount = (Inventory_DataArray[i].AmountStored +
                            itemPicked.GetComponent<Resource_Item>().ResourceAmount) - _slotStackLimit;

                            Inventory_DataArray[i].SlotItemData = itemPicked.GetComponent<Resource_Item>().ItemData;
                            Inventory_DataArray[i].AmountStored = _slotStackLimit;

                            EventBus.Publish<GameObject>(EventType.INVENTORY_UPDATE, this.gameObject);
                        }
                        else
                        {
                            Inventory_DataArray[i].AmountStored += itemPicked.GetComponent<Resource_Item>().ResourceAmount;
                            Inventory_DataArray[i].SlotItemData = itemPicked.GetComponent<Resource_Item>().ItemData;

                            itemPicked.GetComponent<Resource_Item>().ResourceAmount = 0;

                            EventBus.Publish<GameObject>(EventType.INVENTORY_UPDATE, this.gameObject);
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
    }

    public void OnSlotPress(int slotNumber)
    {
        if (Inventory_DataArray[slotNumber].SlotItemData.ResourceName != ResourceType.Empty)
        {
            _inventoryItemDropDialougeArray[slotNumber].SetActive(true);
        }
    }

    public void OnItemDrop(int slotNumber)
    {
        //EventBus.Publish<InventorySlot>(EventType.INVENTORY_ITEMDROPPED, ItemDropped);
        Inventory_Slot ItemData;
        ItemData = Inventory_DataArray[slotNumber];

        GameObject tempItemBlank;
        tempItemBlank = Instantiate(_itemBlankPrefab, this.transform.position, this.transform.rotation);
        tempItemBlank.GetComponent<Resource_Item>().ItemData = ItemData.SlotItemData;
        tempItemBlank.GetComponent<Resource_Item>().ResourceAmount = ItemData.AmountStored;

        Debug.Log(Inventory_DataArray[slotNumber].SlotItemData);
        Inventory_DataArray[slotNumber].SlotItemData = _resourceEmpty;
        Debug.Log(Inventory_DataArray[slotNumber].SlotItemData);
        Inventory_DataArray[slotNumber].AmountStored = 0;
        _inventoryItemDropDialougeArray[slotNumber].SetActive(false);
        EventBus.Publish(EventType.INVENTORY_UPDATE, this.gameObject);
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
        _inventoryItemDropDialougeArray[slotNumber].SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        EventBus.Publish<GameObject>(EventType.INVENTORY_PICKUP, other.gameObject);
    }
}
