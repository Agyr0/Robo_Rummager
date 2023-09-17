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

    [SerializeField]
    private bool _contract_IsTimed;

    [SerializeField]
    private ContractStatus _contract_Status;

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
        set { _contract_TimerCount = value; }
    }

    public int Contract_PayOut
    {
        get { return _robot_RecipeData.Value_Credit; }
    }

    public bool Contract_IsTimed
    {
        get { return Contract_IsTimed; }
    }

    public ContractStatus Contract_Status
    {
        get { return _contract_Status;  }
        set { _contract_Status = value; }
    }

    /*
    public Contract_Data(Robot_RecipeData robot, float timerCount) 
    {
        _robot_RecipeData = robot;
        _contract_TimerCount = timerCount;
    }
    */

    public Contract_Data(Robot_RecipeData robot, float timerCount)
    {
        _robot_RecipeData = robot;
        _contract_TimerCount = timerCount;

        if (timerCount == 0)
            _contract_IsTimed = false;
        else
            _contract_IsTimed = true;

        _contract_Status = ContractStatus.Unassigned;
        //_contract_PayOut = contractPayment;
    }
}