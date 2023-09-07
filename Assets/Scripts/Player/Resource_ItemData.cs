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

    [SerializeField]
    private int _maxStackSize;

    public Sprite ResourceIcon
    {
        get { return _ResourceIcon; }
    }

    public ResourceType ResourceName
    {
        get { return _resourceName; }
    }

    public int MaxStackSize
    {
        get { return _maxStackSize; }
    }

}
