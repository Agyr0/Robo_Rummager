using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Contract_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _contract_BlankTemplate_Prefab;

    [SerializeField]
    private GameObject _playerContract_Container;

    [SerializeField]
    private List<Contract_Data> Contract_DataList;

    [SerializeField]
    private List<Contract_Data> Contract_DataCullList;

    [SerializeField]
    private float _contract_TimerTickRate = 1;

    [SerializeField]
    private bool isContractTimerActive = false;

    private void OnEnable()
    {
        EventBus.Subscribe<GameObject>(EventType.PLAYER_ADDCONTRACT, CreateContract);
        EventBus.Subscribe(EventType.CONTRACT_TIMERTICK, OnContractTimerTick);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe<GameObject>(EventType.PLAYER_ADDCONTRACT, CreateContract);
        EventBus.Subscribe(EventType.CONTRACT_TIMERTICK, OnContractTimerTick);
    }
    
    IEnumerator ContractTimerTickCoroutine()
    {
        while (Contract_DataList.Count != 0)
        {
            EventBus.Publish(EventType.CONTRACT_TIMERTICK);

            yield return new WaitForSeconds(_contract_TimerTickRate);
        }
    }
    
    private void OnContractTimerTick()
    {
        
        for (int i = 0; i < Contract_DataList.Count; i++)
        {
            
            if (Contract_DataList[i].Contract_IsTimed)
            {
                
                if (!isContractTimerActive)
                {
                    isContractTimerActive = true;
                    StartCoroutine(ContractTimerTickCoroutine());
                }

                Contract_DataList[i].Contract_TimerCount--;
                if (Contract_DataList[i].Contract_TimerCount < 0 &&
                    Contract_DataList[i].Contract_Status != ContractStatus.Completed)
                {
                    Contract_DataList[i].Contract_Status = ContractStatus.Failed;
                    Contract_DataCullList.Add(Contract_DataList[i]);
                }
                
            }
            
        }
        
        if (Contract_DataCullList.Count > 0)
        {
            for (int i = 0; i < Contract_DataCullList.Count; i++)
            {
                Contract_DataList.Remove(Contract_DataCullList[i]);
            }
            Contract_DataCullList.Clear();
        }

        EventBus.Publish(EventType.PLAYER_CONTRACTUPDATE);

    }

    public void OnContractCheckForCompleation(GameObject robot)
    {
        for (int i = 0; i < Contract_DataList.Count; i++)
        {
            /*
            if (Contract_DataList[i].Robot_RecipeData == robot.GetComponent<>)
            {
                EventBus.Publish(EventType.CONTRACT_COMPLETED, Contract_DataList[i].Value_Credit);
                Contract_DataList[i].Contract_Status = ContractStatus.Completed;
                Contract_DataCullList.Add(Contract_DataList[i]);
            }
            */
        }
    }
    

    public void CreateContract(GameObject contract_DataHolder)
    {
        Debug.Log("Ading contract");
        Contract_Data newContract = contract_DataHolder.GetComponent<BoardContract_UI_Behavior>().Contract_Data;
        newContract.Contract_Status = ContractStatus.InProgress;
        Contract_DataList.Add(newContract);

        if (newContract.Contract_IsTimed)
        {
            EventBus.Publish(EventType.CONTRACT_TIMERTICK);
        }

        GameObject Contract = Instantiate(_contract_BlankTemplate_Prefab, _playerContract_Container.transform);
        Contract.GetComponent<PlayerContract_UI_Behavior>().Contract_Data = newContract;

        EventBus.Publish(EventType.PLAYER_CONTRACTUPDATE);
    }
}
