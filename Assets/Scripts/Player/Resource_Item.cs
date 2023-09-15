using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource_Item : MonoBehaviour
{
    [SerializeField]
    private Resource_ItemData _itemData;

    [SerializeField]
    private int _resourceAmount;

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

}
