using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading;
using UnityEngine;

public class Player_Contract_Manager : Singleton<Player_Contract_Manager>
{
    //public List<Contract_Data> _contract_Player_DataList;

    //public List<Contract_Data> _contract_DataCullList;

    public Contract_Data _contractData;

    [SerializeField]
    private float _contract_TimerTickRate = 1;

    [SerializeField]
    private bool isContractTimerActive = false;

    [SerializeField]
    private PlayerContract_UI_Behavior _contract_UI;

    [SerializeField]
    private ContractHolder _contractHolder;

    public Contract_Data Contract_Data
    {
        get { return _contractData; }
        set 
        {
            if (_contractHolder == ContractHolder.Occupied)
            {
                _contractData = value;
                Contract_Image = _contractData.RobotSprite;

                switch (_contractData.RobotTier)
                {
                    case RobotTier.I:
                        Contract_RobotTier_Text = "Tier I";
                        break;
                    case RobotTier.II:
                        Contract_RobotTier_Text = "Tier II";
                        break;
                    case RobotTier.III:
                        Contract_RobotTier_Text = "Tier III";
                        break;
                    default:
                        break;
                }

                switch (_contractData.RobotType)
                {
                    case RobotType.Dog:
                        Contract_RobotType_Text = "Dog";
                        break;
                    case RobotType.Cat:
                        Contract_RobotType_Text = "Cat";
                        break;
                    case RobotType.Rat:
                        Contract_RobotType_Text = "Rat";
                        break;
                    case RobotType.Nurse:
                        Contract_RobotType_Text = "Nurse";
                        break;
                    case RobotType.ServiceWorker:
                        Contract_RobotType_Text = "Service Worker";
                        break;
                    case RobotType.PoliceBot:
                        Contract_RobotType_Text = "Police Bot";
                        break;
                    case RobotType.FootBall:
                        Contract_RobotType_Text = "Foot Ball";
                        break;
                    case RobotType.Handyman:
                        Contract_RobotType_Text = "Handyman";
                        break;
                    default:
                        break;
                }

                Contract_Payout_Text = _contractData.Value_Credit.ToString();

                Contract_CountTimer_Text = _contractData.Contract_TimerCount.ToString();

                Contract_Status = _contractData.Contract_Status;
            }
            else
            {
                _contract_UI.contract_Info.SetActive(false);
                _contract_UI.contract_Failed.SetActive(false);
                _contract_UI.contract_Unassigned.SetActive(true);
            }
        }
    }

    public ContractStatus Contract_Status
    {
        get { return _contractData.Contract_Status; }
        set 
        { 
            _contractData.Contract_Status = value;
            if (_contractData.Contract_Status == ContractStatus.InProgress)
            {
                _contract_UI.contract_Info.SetActive(true);
                _contract_UI.contract_Failed.SetActive(false);
                _contract_UI.contract_Unassigned.SetActive(false);
            }
            else if (_contractData.Contract_Status == ContractStatus.Failed)
            {
                _contract_UI.contract_Info.SetActive(false);
                _contract_UI.contract_Failed.SetActive(true);
                _contract_UI.contract_Unassigned.SetActive(false);
            }
        }
    }

    public string Contract_RobotTier_Text
    {
        get { return _contract_UI.Contract_RobotTier_Text; }
        set { _contract_UI.Contract_RobotTier_Text = value; }
    }

    public string Contract_RobotType_Text
    {
        get { return _contract_UI.Contract_RobotType_Text; }
        set { _contract_UI.Contract_RobotType_Text = value; }
    }

    public string Contract_CountTimer_Text
    {
        get { return _contract_UI.Contract_CountTimer_Text; }
        set 
        { 
            if (_contractData.Contract_TimerCount > 0)
            {
                string mintutes = ((float)_contractData.Contract_TimerCount / 60).ToString().Split('.')[0];
                string seconds = (_contractData.Contract_TimerCount % 60).ToString();
                if (seconds.Length == 2)
                {
                    _contract_UI.Contract_CountTimer_Text = "Timer: " + mintutes + ':' + seconds;
                }
                else
                {
                    _contract_UI.Contract_CountTimer_Text = "Timer: " + mintutes + ":0" + seconds;
                }
            }
            else
            {
                _contract_UI.Contract_CountTimer_Text = "";
            }
        }
    }

    public string Contract_Payout_Text
    {
        get { return _contract_UI.Contract_Payout_Text; }
        set { _contract_UI.Contract_Payout_Text = value; }
    }

    public Sprite Contract_Image
    {
        get { return _contract_UI.Contract_Image; }
        set { _contract_UI.Contract_Image = value; }
    }



    private void OnEnable()
    {
        EventBus.Subscribe<Robot_RecipeData, float>(EventType.PLAYER_LOADCONTRACT, CreateContract);
        EventBus.Subscribe<GameObject>(EventType.PLAYER_ADDCONTRACT, CreateContract);
        EventBus.Subscribe(EventType.SAVECONTRACTPURGE, PurgeContracts);
        EventBus.Subscribe<Robot_RecipeData>(EventType.ROBOT_SOLD, OnContractCheckForCompleation);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe<Robot_RecipeData, float>(EventType.PLAYER_LOADCONTRACT, CreateContract);
        EventBus.Unsubscribe<GameObject>(EventType.PLAYER_ADDCONTRACT, CreateContract);
        EventBus.Unsubscribe(EventType.SAVECONTRACTPURGE, PurgeContracts);
        EventBus.Unsubscribe<Robot_RecipeData>(EventType.ROBOT_SOLD, OnContractCheckForCompleation);
    }

    private void Start()
    {
        _contractHolder = ContractHolder.Unoccupied;
        StartCoroutine(ContractTimerTickCoroutine());
    }

    public void PurgeContracts()
    {
        /*
        foreach (Transform contract in _playerContract_Container.GetComponentInChildren<Transform>())
        {
            _purgeContractList.Add(contract.gameObject);
        }
        List<GameObject> tempList = _purgeContractList;
        for (int i = 0; i < _purgeContractList.Count; i++)
        {
            Destroy(tempList[i].gameObject);
        }
        */
    }

    IEnumerator ContractTimerTickCoroutine()
    {
        while (true)
        {
            while (_contractHolder == ContractHolder.Occupied)
            {
                OnContractTimerTick();

                yield return new WaitForSeconds(1f);
            }

            yield return new WaitUntil(() => _contractHolder == ContractHolder.Occupied);
        }
    }

    public void OnContractRemove()
    {
        
        _contractHolder = ContractHolder.Unoccupied;

        Contract_Data = null;

        ContractBoard_Manager.Instance._bulletinBoard_InProgress_UI.SetActive(false);
    }
    
    private void OnContractTimerTick()
    {
        if (Contract_Data.Contract_TimerCount < 0 &&
            Contract_Data.Contract_Status != ContractStatus.Completed)
        {
            Contract_Status = ContractStatus.Failed;
        }
        else if (Contract_Data.Contract_IsTimed)
        {
            Contract_Data.Contract_TimerCount--;
            Contract_CountTimer_Text = Contract_Data.Contract_TimerCount.ToString();
        }
    }

    public void OnContractCheckForCompleation(Robot_RecipeData robot)
    {
        if (Contract_Data.Robot_RecipeData == robot)
        {
            Contract_Status = ContractStatus.Completed;

            _contractHolder = ContractHolder.Unoccupied;

            OnContractRemove();

            ContractBoard_Manager.Instance._bulletinBoard_InProgress_UI.SetActive(false);
            return;
        }
    }
    

    public void CreateContract(GameObject contract_DataHolder)
    {
        Debug.Log("Adding contract");
        Contract_Data newContract = contract_DataHolder.GetComponent<BoardContract_UI_Behavior>().Contract_Data;

        _contractHolder = ContractHolder.Occupied;

        newContract.Contract_Status = ContractStatus.InProgress;
        Contract_Data = newContract;

        ContractBoard_Manager.Instance._bulletinBoard_InProgress_UI.SetActive(true);

        EventBus.Publish(EventType.PLAYER_CONTRACTUPDATE);
    }

    public void CreateContract(Robot_RecipeData robot, float TimeCount)
    {
        Contract_Data newContract = new Contract_Data(robot, TimeCount);

        _contractHolder = ContractHolder.Occupied;

        newContract.Contract_Status = ContractStatus.InProgress;
        Contract_Data = newContract;

        ContractBoard_Manager.Instance._bulletinBoard_InProgress_UI.SetActive(true);

        EventBus.Publish(EventType.PLAYER_CONTRACTUPDATE);
    }

    public enum ContractHolder
    {
        Occupied,
        Unoccupied
    }
}
