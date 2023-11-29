using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot_HolderChild : Inventory_SlotHolder, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject _discardButtons;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.Instance.inventoryManager.Inventory_DataArray[slotOrderNumber].AmountStored > 0) 
        {
            _discardButtons.SetActive(true);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        _discardButtons.SetActive(false);

    }
}
