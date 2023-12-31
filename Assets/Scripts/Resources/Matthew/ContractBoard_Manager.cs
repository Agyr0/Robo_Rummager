using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractBoard_Manager : Singleton<ContractBoard_Manager>, IInteractable
{
    [SerializeField]
    private GameObject _contract_BlankTemplate_Prefab;

    [SerializeField]
    private GameObject _bulletinBoard_Container;

    public GameObject _bulletinBoard_InProgress_UI;

    [SerializeField]
    private List<GameObject> _purgeContractList;

    [SerializeField]
    private List<Robot_RecipeData> _robot_RecipeDataList;

    [SerializeField]
    private List<Contract_Data> _contract_Board_DataList;

    [SerializeField]
    private int _timedContractChance;

    [SerializeField]
    private GameObject selectionCanvas;
    private BilboardScaler scaler;
    private int originalWeaponIndex;

    private Coroutine handleUI;

    private bool isOn = false;
    private bool hasPlayedSingleContractTutorial = false;

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
        EventBus.Subscribe<GameObject>(EventType.PLAYER_ADDCONTRACT, PurgeContract);
        EventBus.Subscribe<Robot_RecipeData>(EventType.PICKED_UP_CHASSIS, AddChassis);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe<GameObject>(EventType.PLAYER_ADDCONTRACT, PurgeContract);
        EventBus.Unsubscribe<int, float>(EventType.BOARD_ADDCONTRACT, CreateContract);
        EventBus.Unsubscribe<Robot_RecipeData>(EventType.PICKED_UP_CHASSIS, AddChassis);
    }

    private void Start()
    {
        EventBus.Publish(EventType.BOARD_ADDCONTRACT, 0, 0f);
    }

    public void StartContracts()
    {
        StartCoroutine(MakeContracts());
    }

    public void LeavePC()
    {
        HandleInteract();
    }

    public void AddChassis(Robot_RecipeData chassisCollcted)
    {
        Robot_RecipeDataList.Add(chassisCollcted);
    }

    IEnumerator MakeContracts()
    {
        while (true)
        {
            Debug.Log("Spawning contract");
            if (Contract_DataList.Count < 3)
            {
                int tempChance = Random.Range(0, _timedContractChance);
                //tier I 8:00 minutes
                //go up by 2 minutes every tier
                if (tempChance == 0)
                {
                    Robot_RecipeData tempData = _robot_RecipeDataList[Random.Range(0, _robot_RecipeDataList.Count)];

                    if (tempData.RobotTier == RobotTier.I)
                    {
                        CreateContract(tempData, 480f);
                    }
                    else if (tempData.RobotTier == RobotTier.II)
                    {
                        CreateContract(tempData, 600f);
                    }
                    else if (tempData.RobotTier == RobotTier.III)
                    {
                        CreateContract(tempData, 720f);
                    }

                }
                else
                {
                    EventBus.Publish(EventType.BOARD_ADDCONTRACT, Random.Range(0, _robot_RecipeDataList.Count), 0f);
                }
            }
            yield return new WaitForSeconds(5f);
        }
    }


    public void PurgeContract(GameObject contractToRemove)
    {
        _contract_Board_DataList.Remove(contractToRemove.GetComponent<BoardContract_UI_Behavior>().Contract_Data);
    }

    public void PurgeContracts()
    {
        foreach (Transform contract in _bulletinBoard_Container.GetComponentInChildren<Transform>())
        {
            _purgeContractList.Add(contract.gameObject);
        }
        List<GameObject> tempList = _purgeContractList;
        for (int i = 0; i < _purgeContractList.Count; i++)
        {
            Destroy(tempList[i].gameObject);
        }
    }

    public void OnContractAccepted()
    {
        AudioManager.Instance.PlayClip(this.GetComponent<AudioSource>(), AudioManager.Instance.effectAudio[6].myControllers[0]);
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

    public void HandleInteract()
    {
        /*
        if (Player_Contract_Manager.Instance.Contract_Data.Contract_Status == ContractStatus.Unassigned)
        {
            _bulletinBoard_InProgress_UI.gameObject.SetActive(false);
        }
        */
        if (!isOn)
        {
            originalWeaponIndex = GameManager.Instance.weaponController.WeaponIndex;
            if(hasPlayedSingleContractTutorial == false)
            {
                EventBus.Publish(EventType.CONTRACTS_OPENED_TUTORIALS);
                hasPlayedSingleContractTutorial = true;
            }
        }
        originalWeaponIndex = GameManager.Instance.weaponController.WeaponIndex;
        isOn = !isOn;


        //Force Weapon switch to hands
        if (isOn)
        {
            GameManager.Instance.weaponController.SwitchWeapon(2);
            GameManager.Instance.InUI = !GameManager.Instance.InUI;
        }
        else if (!isOn)
        {
            GameManager.Instance.InUI = !GameManager.Instance.InUI;
            GameManager.Instance.weaponController.SwitchWeapon(originalWeaponIndex);
        }

        if (scaler == null)
        {
            scaler = GetComponentInChildren<BilboardScaler>();
        }



        if (isOn)
            handleUI = StartCoroutine(scaler.HandleUI());
        else if (handleUI != null)
            StopCoroutine(handleUI);

        EventBus.Publish(EventType.TOGGLE_BULLETIN_CAM_BLEND);
    }
}
