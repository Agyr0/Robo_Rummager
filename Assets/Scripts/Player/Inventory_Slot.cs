using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Slot : MonoBehaviour
{
    [SerializeField]
    private Image _itemIcon;

    [SerializeField]
    private ResourceType _itemStored;

    [SerializeField]
    private int _amountStored;

    [SerializeField]
    private Text _textDisplayCount;

    public Image ItemIcon
    {
        get { return _itemIcon; }
        set { _itemIcon = value; }
    }

    public ResourceType ItemStored
    {
        get { return _itemStored; }
        set { _itemStored = value; }
    }

    public int AmountStored
    {
        get { return _amountStored; }
        set { _amountStored = value; }
    }

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.INVENTORY_UPDATE, OnInventoryUpdate);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.INVENTORY_UPDATE, OnInventoryUpdate);
    }

    private void OnInventoryUpdate()
    {
        if (ItemStored != ResourceType.Empty)
        {
            this.GetComponent<Image>().sprite = ItemIcon.sprite;
            _textDisplayCount.text = AmountStored.ToString();
        }
    }
}
