using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractBoard_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _contract_BlankTemplate_Prefab;

    [SerializeField]
    private GameObject _bulletinBoard_Container;

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.BOARD_ADDCONTRACT, CreateContract);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.BOARD_ADDCONTRACT, CreateContract);
    }

    public void CreateContract()
    {
        GameObject Contract = Instantiate(_contract_BlankTemplate_Prefab, _bulletinBoard_Container.transform);
    }
}
