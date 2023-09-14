using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_SlotHolder : MonoBehaviour
{
    [SerializeField]
    private Text _slotStoredAmountText;

    [SerializeField]
    private Image _slotIconSprite;

    [SerializeField]
    private int _slotOrderNumber;

    public Text slotStoredAmountText
    {
        set { _slotStoredAmountText = value; }
        get { return _slotStoredAmountText; }
    }

    public Image SlotIconSprite
    {
        set { _slotIconSprite = value; }
        get { return _slotIconSprite; }
    }

    private void OnEnable()
    {
        EventBus.Subscribe<GameObject>(EventType.INVENTORY_UPDATE, OnUpdateInventorySlot);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GameObject>(EventType.INVENTORY_UPDATE, OnUpdateInventorySlot);
    }

    private void OnUpdateInventorySlot(GameObject inventoryContainer)
    {
        SlotIconSprite.sprite = inventoryContainer.GetComponent<Player_InventoryManager>().Inventory_DataArray[_slotOrderNumber].SlotItemData.ResourceIcon;
        slotStoredAmountText.text = inventoryContainer.GetComponent<Player_InventoryManager>().Inventory_DataArray[_slotOrderNumber].AmountStored.ToString();
    }
}
