using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractBoard_Manager : Singleton<ContractBoard_Manager>
{
    [SerializeField]
    private GameObject _contract_BlankTemplate_Prefab;

    [SerializeField]
    private GameObject _bulletinBoard_Container;

    [SerializeField]
    private List<Robot_RecipeData> _robot_RecipeDataList;

    [SerializeField]
    private List<Contract_Data> _contract_Board_DataList;

    public List<Robot_RecipeData> Robot_RecipeDataList
    {
        get { return _robot_RecipeDataList; }
        set { _robot_RecipeDataList = value; }
    }

    public List<Contract_Data> Contract_DataList
    {
        get { return _contract_Board_DataList; }
        set { _contract_Board_DataList = value; }
    }

    private void OnEnable()
    {
        EventBus.Subscribe<int,float>(EventType.BOARD_ADDCONTRACT, CreateContract);
        EventBus.Subscribe<Robot_RecipeData, float>(EventType.BOARD_ADDLOADCONTRACT, CreateContract);
        //EventBus.Subscribe(EventType.ONLOAD, OnLoad);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe<int, float>(EventType.BOARD_ADDCONTRACT, CreateContract);
    }

    
    public void CreateContract(int robot, float TimeCount)
    {
        
        Contract_Data newContract = new Contract_Data(Robot_RecipeDataList[robot], TimeCount);
        newContract.Contract_Status = ContractStatus.Available;
        Contract_DataList.Add(newContract);
        
        GameObject Contract = Instantiate(_contract_BlankTemplate_Prefab, _bulletinBoard_Container.transform);
        Contract.GetComponent<BoardContract_UI_Behavior>().Contract_Data = newContract;

        EventBus.Publish(EventType.BOARD_CONTRACTUPDATE);
    }

    public void CreateContract(Robot_RecipeData robot, float TimeCount)
    {

        Contract_Data newContract = new Contract_Data(robot, TimeCount);
        newContract.Contract_Status = ContractStatus.Available;
        Contract_DataList.Add(newContract);

        GameObject Contract = Instantiate(_contract_BlankTemplate_Prefab, _bulletinBoard_Container.transform);
        Contract.GetComponent<BoardContract_UI_Behavior>().Contract_Data = newContract;

        EventBus.Publish(EventType.BOARD_CONTRACTUPDATE);
    }
}
