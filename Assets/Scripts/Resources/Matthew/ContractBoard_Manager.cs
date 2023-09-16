using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractBoard_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _contract_BlankTemplate_Prefab;

    [SerializeField]
    private GameObject _bulletinBoard_Container;

    [SerializeField]
    private List<Robot_RecipeData> Robot_RecipeDataList;

    [SerializeField]
    private List<Contract_Data> Contract_DataList;

    private void OnEnable()
    {
        EventBus.Subscribe<int,float>(EventType.BOARD_ADDCONTRACT, CreateContract);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe<int, float>(EventType.BOARD_ADDCONTRACT, CreateContract);
    }

    
    public void CreateContract(int robot, float TimeCount)
    {
        
        Contract_Data newContract = new Contract_Data(Robot_RecipeDataList[robot], TimeCount);
        Contract_DataList.Add(newContract);
        
        GameObject Contract = Instantiate(_contract_BlankTemplate_Prefab, _bulletinBoard_Container.transform);
        Contract.GetComponent<BoardContract_UI_Behavior>().Contract_Data = newContract;

        EventBus.Publish(EventType.BOARD_CONTRACTUPDATE);
    }
    
    /*
    public void CreateContract(int robot, float timeCount, int contractPayment)
    {
        Contract_Data newContract = new Contract_Data(Robot_RecipeDataList[robot], timeCount, contractPayment);
        Contract_DataList.Add(newContract);

        GameObject Contract = Instantiate(_contract_BlankTemplate_Prefab, _bulletinBoard_Container.transform);
        Contract.GetComponent<BoardContract_UI_Behavior>().Contract_Data = newContract;

        EventBus.Publish(EventType.BOARD_CONTRACTUPDATE);
    }
    */
}
