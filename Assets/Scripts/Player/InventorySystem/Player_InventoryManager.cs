using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Player_InventoryManager : Singleton<Player_InventoryManager>
{
    [SerializeField]
    private int _creditPurse;

    [SerializeField]
    private float _pickupEventInterval = .1f;

    [SerializeField]
    private Inventory_Slot[] _inventory_DataArray;

    [SerializeField]
    private GameObject[] _inventoryItemDropDialougeArray;

    [SerializeField]
    private GameObject[] _inventory_SlotArray;

    [SerializeField]
    private GameObject[] _inventoryHUD_SlotArray;

    [SerializeField]
    private List<GameObject> _inventory_ItemPickupList;

    [SerializeField]
    private List<GameObject> _inventory_ItemCullPickupList;

    [SerializeField]
    private Text _creditText;

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

    private bool _isSendingPickupEvents = false;

    public int CreditPurse
    {
        get { return _creditPurse; }
        set 
        { 
            _creditPurse = value;
            _creditText.text = "Credit: $" + _creditPurse;
        }
    }

    public Inventory_Slot[] Inventory_DataArray
    {
        get { return _inventory_DataArray; }
        set { _inventory_DataArray = value; }
    }

    public List<GameObject> Inventory_ItemPickupList
    {
        get { return _inventory_ItemPickupList; }
        set { _inventory_ItemPickupList = value; }
    }

    public List<GameObject> Inventory_ItemCullPickupList
    {
        get { return _inventory_ItemCullPickupList; }
        set { _inventory_ItemCullPickupList = value; }
    }
    
    public Resource_ItemData ResourceEmpty
    {
        get { return _resourceEmpty; }
        set { _resourceEmpty = value; }
    }
    
    //Current method of keeping track of players inventory slot counts untill
    //a player data script is implemented.

    [SerializeField]
    private int _inventorySlotCount;

    public int InventorySlotCount
    {
        get { return _inventorySlotCount; }
        set
        {
            _inventorySlotCount = value;
            if (_inventorySlotCount > 2)
            {
                OnInventoryLoadSlots(_inventorySlotCount - 2);
            }
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.inventoryManager = this;       
        EventBus.Subscribe(EventType.INVENTORY_ADDSLOT, OnInventoryAddSlot);
        EventBus.Subscribe(EventType.INVENTORY_REMOVESLOT, OnInventoryRemoveSlot);
        EventBus.Subscribe(EventType.INVENTORY_PICKUP, OnItemPickup);
        EventBus.Subscribe<GameObject>(EventType.INVENTORY_SORTPICKUP, OnItemSortPickup);
        EventBus.Subscribe<GameObject>(EventType.INVENTORY_ADDITEM, OnAddItem);
        EventBus.Subscribe<GameObject>(EventType.INVENTORY_ADDITEMCULL, OnAddCullItem);
        EventBus.Subscribe(EventType.INVENTORY_REMOVEITEM, OnRemoveItem);
        EventBus.Subscribe<int>(EventType.CONTRACT_COMPLETED, OnContractCompleation);
        EventBus.Subscribe<int>(EventType.UPGRADE_STACKSIZE, OnUpgradeStackSize);
        EventBus.Subscribe(EventType.ONLOAD, OnLoad);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.INVENTORY_ADDSLOT, OnInventoryAddSlot);
        EventBus.Unsubscribe(EventType.INVENTORY_REMOVESLOT, OnInventoryRemoveSlot);
        EventBus.Unsubscribe(EventType.INVENTORY_PICKUP, OnItemPickup);
        EventBus.Unsubscribe<GameObject>(EventType.INVENTORY_SORTPICKUP, OnItemSortPickup);
        EventBus.Unsubscribe<GameObject>(EventType.INVENTORY_ADDITEM, OnAddItem);
        EventBus.Unsubscribe<GameObject>(EventType.INVENTORY_ADDITEMCULL, OnAddCullItem);
        EventBus.Unsubscribe(EventType.INVENTORY_REMOVEITEM, OnRemoveItem);
        EventBus.Unsubscribe<int>(EventType.CONTRACT_COMPLETED, OnContractCompleation);
        EventBus.Unsubscribe<int>(EventType.UPGRADE_STACKSIZE, OnUpgradeStackSize);
    }

    private void OnInventoryLoadSlots(int slotsToAdd)
    {
        for (int i = 0; i < slotsToAdd; i++)
        {
            _inventoryHUD_SlotArray[i+2].SetActive(true);
            _inventory_SlotArray[i+2].SetActive(true);
        }
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

    public void OnUpgradeStackSize(int size)
    {
        _slotStackLimit += size;
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

    public void OnLoad()
    {
        Debug.Log("Ah gees Rick, I guess I need to update the inventory display");
        EventBus.Publish(EventType.INVENTORY_UPDATE, this.gameObject);
    }

    private void OnItemSortPickup(GameObject itemPicked)
    {
        for (int i = 0; i < _inventorySlotCount; i++)
        {
            //checks for a zero amount resource
            if (itemPicked.GetComponent<Resource_Item>().ResourceAmount != 0 &&
                itemPicked.GetComponent<Resource_Item>().IsReadyForPickup == true)
            {
                if (itemPicked.GetComponent<Resource_Item>().ItemData.ResourceName != ResourceType.Credit)
                {
                    Debug.Log("An item was picked up");
                    //Looks for an empty or matching resource slot
                    if (Inventory_DataArray[i].SlotItemData.ResourceName == ResourceType.Empty
                        || Inventory_DataArray[i].SlotItemData.ResourceName == itemPicked.GetComponent<Resource_Item>().ItemData.ResourceName)
                    {
                        Debug.Log("A slot was found");
                        Inventory_DataArray[i].SlotItemData = itemPicked.GetComponent<Resource_Item>().ItemData;
                        if (Inventory_DataArray[i].AmountStored != _slotStackLimit)
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
                else
                {
                    CreditPurse += itemPicked.GetComponent<Resource_Item>().ResourceAmount;
                    itemPicked.GetComponent<Resource_Item>().ResourceAmount = 0;
                }
            }
        }
        
        if (itemPicked.GetComponent<Resource_Item>().ResourceAmount == 0)
        {
            EventBus.Publish<GameObject>(EventType.INVENTORY_ADDITEMCULL, itemPicked);
            itemPicked.gameObject.SetActive(false);
        }
        EventBus.Publish(EventType.INVENTORY_UPDATE, this.gameObject);
    }

    public void OnInventorySlotInteract(int slotNumber)
    {
        if (Inventory_DataArray[slotNumber].SlotItemData.ResourceName != ResourceType.Empty)
            _inventoryItemDropDialougeArray[slotNumber].SetActive(true);
    }

    public void OnItemPickup()
    {
        if (Inventory_ItemPickupList.Count > 0)
        {
            Inventory_ItemPickupList = Inventory_ItemPickupList.OrderBy(x => Vector2.Distance(this.transform.position, x.transform.position)).ToList();

            for (int x = 0; x < Inventory_ItemPickupList.Count; x++)
                EventBus.Publish<GameObject>(EventType.INVENTORY_SORTPICKUP, Inventory_ItemPickupList[x]);

        }
        EventBus.Publish(EventType.INVENTORY_UPDATE, this.gameObject);
        //EventBus.Publish(EventType.INVENTORY_REMOVEITEM);
    }

    public void OnItemDrop(int slotNumber)
    {
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
        EventBus.Publish(EventType.INVENTORY_UPDATE, this.gameObject);
    }

    public void OnItemDropCancelled(int slotNumber)
    {
        _inventoryItemDropDialougeArray[slotNumber].SetActive(false);
    }

    public void OnAddItem(GameObject item_GO)
    {
        if (!Inventory_ItemPickupList.Contains(item_GO))
            Inventory_ItemPickupList.Add(item_GO);
    }

    public void OnAddCullItem(GameObject item_GO)
    {
        if (!Inventory_ItemCullPickupList.Contains(item_GO))
            Inventory_ItemCullPickupList.Add(item_GO);
    }

    private void OnRemoveItem()
    {
        
        for (int i = 0; i < Inventory_ItemCullPickupList.Count; i++)
            Inventory_ItemPickupList.Remove(Inventory_ItemCullPickupList[i]);

        Inventory_ItemCullPickupList.Clear();
        EventBus.Publish(EventType.INVENTORY_UPDATE, this.gameObject);
        
    }

    private void OnContractCompleation(int creditValue)
    {
        CreditPurse += creditValue;
    }

    IEnumerator SendPickupEvents()
    {
        while (_isSendingPickupEvents)
        {
            //Debug.Log("Checking for pickups");
            EventBus.Publish(EventType.INVENTORY_PICKUP);

            yield return new WaitForSeconds(_pickupEventInterval);
            
            if (Inventory_ItemPickupList.Count == 0)
            {
                _isSendingPickupEvents = false;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Resource_Item>() != null)
        {
            if (_isSendingPickupEvents == false)
            {
                _isSendingPickupEvents = true;
                StartCoroutine(SendPickupEvents());
            }
            EventBus.Publish<GameObject>(EventType.INVENTORY_ADDITEM, other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Resource_Item>() != null)
        {
            EventBus.Publish<GameObject>(EventType.INVENTORY_ADDITEMCULL, other.gameObject);
            EventBus.Publish(EventType.INVENTORY_REMOVEITEM);
        }
        /*
        if (other.GetComponent<Resource_Item>() != null)
        {
            Inventory_ItemCullPickupList.Add(other.gameObject);
        }
        */
    }
}
