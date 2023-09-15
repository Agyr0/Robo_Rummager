using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Inventory_Slot
{
    [SerializeField]
    private int _amountStored;

    [SerializeField]
    private Resource_ItemData _slotItemData;

    private Inventory_Slot()
    {
    }

    private Inventory_Slot(Resource_ItemData itemData)
    {
        _slotItemData = itemData;
    }

    public int AmountStored
    {
        get { return _amountStored; }
        set { _amountStored = value; }
    }

    public Resource_ItemData SlotItemData
    {
        get { return _slotItemData; }
        set { _slotItemData = value; }
    }
}
