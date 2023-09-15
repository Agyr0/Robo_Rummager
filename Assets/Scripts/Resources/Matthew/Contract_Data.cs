using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Contract_Data
{
    [SerializeField]
    private Robot_RecipeData _robot_RecipeData;

    [SerializeField]
    private float _contract_TimerCount;

    [SerializeField]
    private int _contract_PayOut;

    public Robot_RecipeData Robot_RecipeData
    {
        get { return _robot_RecipeData; }
    }

    public RobotTier RobotTier
    {
        get {  return _robot_RecipeData.RobotTier;}
    }

    public RobotType RobotType
    {
        get { return _robot_RecipeData.Robot; }
    }

    public int Value_Credit
    {
        get { return _robot_RecipeData.Value_Credit;  }
    }

    public float Contract_TimerCount
    {
        get { return _contract_TimerCount; }
    }

    public int Contract_PayOut
    {
        get { return _contract_PayOut; }
    }

    /*
    public Contract_Data(Robot_RecipeData robot, float timerCount) 
    {
        _robot_RecipeData = robot;
        _contract_TimerCount = timerCount;
    }
    */

    public Contract_Data(Robot_RecipeData robot, float timerCount, int contractPayment)
    {
        _robot_RecipeData = robot;
        _contract_TimerCount = timerCount;
        _contract_PayOut = contractPayment;
    }
}