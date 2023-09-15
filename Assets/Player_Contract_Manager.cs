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

    private void OnEnable()
    {
        EventBus.Subscribe<GameObject>(EventType.PLAYER_ADDCONTRACT, CreateContract);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe<GameObject>(EventType.PLAYER_ADDCONTRACT, CreateContract);
    }

    public void CreateContract(GameObject contract_DataHolder)
    {
        Debug.Log("Ading contract");
        Contract_Data newContract = contract_DataHolder.GetComponent<BoardContract_UI_Behavior>().Contract_Data;
        Contract_DataList.Add(newContract);

        GameObject Contract = Instantiate(_contract_BlankTemplate_Prefab, _playerContract_Container.transform);
        Contract.GetComponent<PlayerContract_UI_Behavior>().Contract_Data = newContract;

        EventBus.Publish(EventType.PLAYER_CONTRACTUPDATE);
    }
}
