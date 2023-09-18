using System;
using UnityEngine;

[System.Serializable]
public class DropTableItem
{
    [SerializeField, Tooltip("The resource that will be dropping")]
    private GameObject resource;
    [SerializeField, Range(0, 100), Tooltip("Percentage of how likely this item is to drop")]
    private float dropChance = 0f;
    [SerializeField, Range(0, 30), Tooltip("Minimum amount that can drop")]
    private int _minDropCount = 0;
    [SerializeField, Range(0, 30), Tooltip("Maximum amount that can drop")]
    private int _maxDropCount = 0;
    
    [Space(10), Tooltip("This will set the name of the element for organization purposes")]
    public string elementName = "Change Me";

    public GameObject Resource
    { get { return resource; } }
    public float DropPercentage
    {
        get 
        {
            return dropChance / 100f;
        } 
    }
    public int MinDropCount
    { get { return _minDropCount; } }
    public int MaxDropCount
    { get { return _maxDropCount; } }

    public bool CheckDrop()
    {
        int chance = UnityEngine.Random.Range(0, 2);

        if (DropPercentage >= chance)
            return true;
        return false;
    }
}
