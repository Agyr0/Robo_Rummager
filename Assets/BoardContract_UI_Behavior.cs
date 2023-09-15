using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BoardContract_UI_Behavior : MonoBehaviour
{
    [SerializeField]
    private Contract_Data _contract_Data;

    [SerializeField]
    private GameObject _contract_Button_Promt;

    [SerializeField]
    private Text _contract_RobotTier_Text;

    [SerializeField]
    private Text _contract_RobotType_Text;

    [SerializeField]
    private Text _contract_CountTimer_Text;

    [SerializeField]
    private Text _contract_Payout_Text;


    public Contract_Data Contract_Data
    {
        get { return _contract_Data; }
        set { _contract_Data = value; }
    }

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.BOARD_CONTRACTUPDATE, OnDisplayUpdate_Contract);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.BOARD_CONTRACTUPDATE, OnDisplayUpdate_Contract);
    }

    private void OnDisplayUpdate_Contract()
    {
        Set_RobotTier_Text(_contract_Data.RobotTier);
        Set_RobotType_Text(_contract_Data.RobotType);
        Set_Payout_Text();
        Set_CountTimer_Text();
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
            case RobotType.Rabbit:
                _contract_RobotType_Text.text = "Rabbit";
                break;
            case RobotType.Turtle:
                _contract_RobotType_Text.text = "Turtle";
                break;
            case RobotType.GuinneaPig:
                _contract_RobotType_Text.text = "Guinnea Pig";
                break;
            case RobotType.HouseKeep:
                _contract_RobotType_Text.text = "House Keep";
                break;
            case RobotType.ElderCare:
                _contract_RobotType_Text.text = "Elder Care";
                break;
            case RobotType.YardMaintenance:
                _contract_RobotType_Text.text = "Yard Maintenance";
                break;
            case RobotType.Nurse:
                _contract_RobotType_Text.text = "Nurse";
                break;
            case RobotType.Retail:
                _contract_RobotType_Text.text = "Retail";
                break;
            case RobotType.BodyGuard:
                _contract_RobotType_Text.text = "Body Gaurd";
                break;
            case RobotType.HouseProtector:
                _contract_RobotType_Text.text = "House Protector";
                break;
            case RobotType.PoliceBot:
                _contract_RobotType_Text.text = "Police Bot";
                break;
            case RobotType.MilitaryBot:
                _contract_RobotType_Text.text = "Military Bot";
                break;
            case RobotType.FootBall:
                _contract_RobotType_Text.text = "Foot Ball";
                break;
            default:
                break;
        }
    }

    public void Set_Payout_Text()
    {
        _contract_Payout_Text.text = "" + _contract_Data.Contract_PayOut;
    }

    public void Set_CountTimer_Text()
    {
        _contract_CountTimer_Text.text = "" + _contract_Data.Contract_TimerCount;
    }

    public void OnContractButtonPress()
    {
        _contract_Button_Promt.SetActive(true);
    }

    public void OnButtonPromtPress(bool isContractAccepted)
    {
        if (isContractAccepted)
        {
            EventBus.Publish(EventType.PLAYER_ADDCONTRACT, this.gameObject);
            EventBus.Publish(EventType.PLAYER_CONTRACTUPDATE);
            this.gameObject.SetActive(false);
        }
        else
        {
            _contract_Button_Promt.SetActive(false);
        }
    }
}
