using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Resource/Item")]
public class Resource_ItemData : ScriptableObject
{
    [SerializeField]
    private Sprite _resourceIcon;

    [SerializeField]
    private ResourceType _resourceName;

    [SerializeField]
    private Material _resourceMaterial;

    [SerializeField]
    private Mesh _resourceMesh;

    [SerializeField]
    private int _resourcePrintTime;

    public Sprite ResourceIcon
    {
        get { return _resourceIcon; }
    }

    public ResourceType ResourceName
    {
        get { return _resourceName; }
    }

    public Material ResourceMaterial
    {
        get { return _resourceMaterial; }
    }

    public Mesh ResourceMesh
    {
        get { return _resourceMesh; }
    }

    public int ResourcePrintTime
    {
        get { return _resourcePrintTime; }
    }
}
