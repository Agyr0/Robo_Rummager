using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField]
    private Resource_ItemData _resourceData;
    [SerializeField]
    private int _stackSize;

    public Resource_ItemData ResourceData => _resourceData;

    public int StackSize => _stackSize;

    public InventorySlot(Resource_ItemData resourceData, int stackSize)
    {
        this._resourceData = resourceData;
        _stackSize = stackSize;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        _resourceData = null;
        _stackSize = -1;
    }

    public void UpdateInventorySlot(Resource_ItemData data, int amount)
    {
        _resourceData = data;
        _stackSize = amount;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = ResourceData.MaxStackSize - _stackSize;

        return RoomLeftInStack(amountToAdd);
    }

    public bool RoomLeftInStack(int amountToAdd)
    {
        if (_stackSize + amountToAdd <= _resourceData.MaxStackSize)
            return true;

        else
            return false;
    }

    public void AddToStack(int amount)
    {
        _stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        _stackSize -= amount;
    }
}
