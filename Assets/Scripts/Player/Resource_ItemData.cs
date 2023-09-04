using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Resource/Item")]
public class Resource_ItemData : ScriptableObject
{
    [SerializeField]
    private Sprite _ResourceIcon;

    [SerializeField]
    private ResourceType _resourceName;

    /*
    [SerializeField]
    private int _resourceAmount;
    */

    public Sprite ResourceIcon
    {
        get { return _ResourceIcon; }
    }

    public ResourceType ResourceName
    {
        get { return _resourceName; }
    }

    /*
    public int ResourceAmount
    {
        get { return _resourceAmount; }
        set { _resourceAmount = value; }
    }
    */
}
