using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventoy System/Items/Database")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    private Resource_ItemData[] _resourceData_Array;
    [SerializeField]
    private Dictionary<Resource_ItemData, int> GetID = new Dictionary<Resource_ItemData, int>();
    [SerializeField]
    private Dictionary<int, Resource_ItemData> GetItem = new Dictionary<int, Resource_ItemData>();

    public void OnAfterDeserialize()
    {
        GetID = new Dictionary<Resource_ItemData, int>();
        GetItem = new Dictionary<int, Resource_ItemData>();
        for (int i = 0; i < _resourceData_Array.Length; i++)
        {
            GetID.Add(_resourceData_Array[i], i);
            GetItem.Add(i, _resourceData_Array[i]);
        }
    }

    
    public void OnBeforeSerialize()
    {
    }
    
}
