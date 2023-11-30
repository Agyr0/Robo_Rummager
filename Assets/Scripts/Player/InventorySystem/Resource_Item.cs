using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource_Item : MonoBehaviour
{
    [SerializeField]
    private Resource_ItemData _itemData;

    [SerializeField]
    private int _resourceAmount;

    [SerializeField]
    private bool _isReadyForPickup = false;

    [SerializeField]
    private int _pickupTimerCount = 2;

    public Resource_ItemData ItemData
    {
        get { return _itemData; }
        set { _itemData = value; }
    }

    public int ResourceAmount
    {
        get { return _resourceAmount; }
        set { _resourceAmount = value; }
    }

    public bool IsReadyForPickup
    {
        get { return _isReadyForPickup;}
        set { _isReadyForPickup = value;}
    }

    public int PickupTimerCount
    {
        get { return _pickupTimerCount; }
        set { _pickupTimerCount = value; }
    }

    private void OnEnable()
    {
        StartCoroutine(PickupCounter());
    }

    private void OnDisable()
    {
        //ResourceAmount++;
    }

    private void Start()
    {
        if(GameManager.Instance.inventoryManager.GetComponent<Collider>().bounds.Contains(this.transform.position) == true)
        {
            EventBus.Publish<GameObject>(EventType.INVENTORY_ADDITEM, this.gameObject);
        }
    }

    IEnumerator PickupCounter()
    {
        yield return new WaitForSeconds(PickupTimerCount);
        IsReadyForPickup = true;
    }
}
