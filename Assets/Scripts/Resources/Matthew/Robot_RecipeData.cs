using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Resource/RobotRecipe")]
public class Robot_RecipeData : ScriptableObject
{
    [SerializeField]
    private RobotType _robot;

    [SerializeField]
    private RobotTier _tier;

    [SerializeField]
    private int _resource_MotherBoard;

    [SerializeField]
    private int _resource_Oil;

    [SerializeField]
    private int _resource_Wire;

    [SerializeField]
    private int _resource_MetalScrap;

    [SerializeField]
    private int _resource_AdvancedSensor;

    [SerializeField]
    private int _resource_Z_Crystal;

    [SerializeField]
    private int _resource_RadioactiveWaste;

    [SerializeField]
    private int _resource_BlackMatter;

    [SerializeField]
    private int _value_Credit;

    public RobotType Robot
    {
        get { return _robot; }
    }

    public RobotTier RobotTier
    {
        get { return _tier; }
    }

    public int Resource_MotherBoard
    {
        get { return _resource_MotherBoard; }
    }

    public int Resource_Oil
    {
        get { return _resource_Oil; }
    }

    public int Resource_Wire
    {
        get { return _resource_Wire; }
    }

    public int Resource_MetalScrap
    {
        get { return _resource_MetalScrap;}
    }

    public int Resource_AdvancedSensor
    {
        get { return _resource_AdvancedSensor; }
    }

    public int Z_Crystal
    {
        get { return _resource_Z_Crystal; }
    }

    public int Resource_RadioactiveWaste
    {
        get { return _resource_RadioactiveWaste; }
    }

    public int Resource_BlackMatter
    {
        get { return _resource_BlackMatter;}
    }

    public int Value_Credit
    {
        get { return _value_Credit; }
    }
}
