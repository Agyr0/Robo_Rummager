using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlayerContract_UI_Behavior : MonoBehaviour
{
    [SerializeField]
    private Contract_Data _contract_Data;

    [SerializeField]
    private TextMeshProUGUI _contract_RobotTier_Text;

    [SerializeField]
    private TextMeshProUGUI _contract_RobotType_Text;

    [SerializeField]
    private TextMeshProUGUI _contract_CountTimer_Text;

    [SerializeField]
    private TextMeshProUGUI _contract_Payout_Text;

    [SerializeField]
    private Image _contract_Image;

    [SerializeField]
    private GameObject _contract_Failed;


    public Contract_Data Contract_Data
    {
        get { return _contract_Data; }
        set { _contract_Data = value; }
    }

    private void Start()
    {
        EventBus.Subscribe(EventType.PLAYER_CONTRACTUPDATE, OnDisplayUpdate_Contract);
    }

    private void OnDisplayUpdate_Contract()
    {
        if (_contract_Data.Contract_Status != ContractStatus.Completed)
        {
            Set_RobotTier_Text(_contract_Data.RobotTier);
            Set_RobotType_Text(_contract_Data.RobotType);
            Set_Payout_Text();
            Set_CountTimer_Text();
            Set_RobotImage(_contract_Data.RobotSprite);
        }

        if(_contract_Data.Contract_Status == ContractStatus.Failed)
        {
            _contract_Failed.SetActive(true);
        }

        if (_contract_Data.Contract_Status == ContractStatus.Completed)
        {
            DeleteContract();
        }
    }
    
    public void DeleteContract()
    {
        ContractBoard_Manager.Instance._bulletinBoard_InProgress_UI.SetActive(false);
        Player_Contract_Manager.Instance.OnContractRemove();
        Destroy(this.gameObject);
    }

    public void Set_RobotImage(Sprite robotSprite)
    {
        _contract_Image.sprite = robotSprite;
    }

    public void Set_RobotTier_Text(RobotTier robotTier)
    {
        switch (robotTier)
        {
            case RobotTier.I:
                _contract_RobotTier_Text.text = "Tier I";
                break;
            case RobotTier.II:
                _contract_RobotTier_Text.text = "Tier II";
                break;
            case RobotTier.III:
                _contract_RobotTier_Text.text = "Tier III";
                break;
            default:
                break;
        }
    }

    public void Set_RobotType_Text(RobotType robot)
    {
        
        switch (robot)
        {
            case RobotType.Dog:
                _contract_RobotType_Text.text = "Dog";
                break;
            case RobotType.Cat:
                _contract_RobotType_Text.text = "Cat";
                break;
            case RobotType.Rat:
                _contract_RobotType_Text.text = "Rat";
                break;
            case RobotType.Nurse:
                _contract_RobotType_Text.text = "Nurse";
                break;
            case RobotType.ServiceWorker:
                _contract_RobotType_Text.text = "Service Worker";
                break;
            case RobotType.PoliceBot:
                _contract_RobotType_Text.text = "Police Bot";
                break;
            case RobotType.FootBall:
                _contract_RobotType_Text.text = "Foot Ball";
                break;
            case RobotType.Handyman:
                _contract_RobotType_Text.text = "Handyman";
                break;
            default:
                break;
        }
    }

    public void Set_Payout_Text()
    {
        _contract_Payout_Text.text = "" + _contract_Data.Value_Credit;
    }

    public void Set_CountTimer_Text()
    {
        if (_contract_Data.Contract_TimerCount > 0)
        {
            string mintutes = ((float)_contract_Data.Contract_TimerCount / 60).ToString().Split('.')[0];
            string seconds = (_contract_Data.Contract_TimerCount % 60).ToString();
            if (seconds.Length == 2)
            {
                _contract_CountTimer_Text.text = "Timer: " + mintutes + ':' + seconds;
            }
            else
            {
                _contract_CountTimer_Text.text = "Timer: " + mintutes + ":0" + seconds;
            }
        }
        else
        {
            _contract_CountTimer_Text.text = "";
        }
    }
}
