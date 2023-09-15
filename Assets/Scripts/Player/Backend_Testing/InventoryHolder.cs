using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField]
    private int _inventorySize;
    [SerializeField]
    protected InventorySystem inventorySystem;

    public InventorySystem InventorySystem => inventorySystem;

    //event display inventory
    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested;

    private void Awake()
    {
        inventorySystem = new InventorySystem(_inventorySize);
    }
}
