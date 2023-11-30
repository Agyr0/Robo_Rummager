using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory_SlotHolder : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject _discardButtons;

    [SerializeField]
    private TextMeshProUGUI _slotStoredAmountText;

    [SerializeField]
    private Image _slotIconSprite;

    public int slotOrderNumber;

    public TextMeshProUGUI slotStoredAmountText
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
        SlotIconSprite.sprite = inventoryContainer.GetComponent<Player_InventoryManager>().Inventory_DataArray[slotOrderNumber].SlotItemData.ResourceIcon;
        if (inventoryContainer.GetComponent<Player_InventoryManager>().Inventory_DataArray[slotOrderNumber].AmountStored > 0)
        {
            slotStoredAmountText.text = inventoryContainer.GetComponent<Player_InventoryManager>().Inventory_DataArray[slotOrderNumber].AmountStored.ToString();
        }
        else
        {
            slotStoredAmountText.text = "";
        }
        
    }

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
