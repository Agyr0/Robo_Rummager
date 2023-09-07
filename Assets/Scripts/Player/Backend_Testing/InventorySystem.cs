using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventorySystem
{
    [SerializeField]
    private List<InventorySlot> inventorySlotList;

    public List<InventorySlot> InventorySlotList => inventorySlotList;
    public int InventorySize => InventorySlotList.Count;

    //event for the slot that changed
    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size)
    {
        inventorySlotList = new List<InventorySlot>(size);
        for (int i = 0; i < size; i++)
        {
            inventorySlotList.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(Resource_ItemData itemToAdd, int amountToAdd)
    {
        if (ContainsItem(itemToAdd, out List<InventorySlot> invSlotList))
        {
            foreach (var slot in invSlotList)
            {
                if (slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    //event for inventory slot changed
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
            
        }

        if (HasEmptySlot(out InventorySlot emptySlot))
        {
            emptySlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            //reference to inventory slot changed
            OnInventorySlotChanged?.Invoke(emptySlot);
            return true;
        }

        return false;
    }

    public bool ContainsItem(Resource_ItemData itemToAdd, out List<InventorySlot> invSlot)
    {
        //will create a list of inventory sltos that contain itemToAdd
        invSlot = InventorySlotList.Where(slot => slot.ResourceData == itemToAdd).ToList();

        return invSlot == null ? true : false;
    }

    public bool HasEmptySlot(out InventorySlot emptySlot)
    {
        emptySlot = InventorySlotList.FirstOrDefault(i => i.ResourceData == null);
        return emptySlot == null ? false : true;
    }

}
