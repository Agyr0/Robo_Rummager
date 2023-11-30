using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private GameObject _contract_Information_Promt;

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


    public Contract_Data Contract_Data
    {
        get { return _contract_Data; }
        set { _contract_Data = value; }
    }

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.BOARD_CONTRACTUPDATE, OnDisplayUpdate_Contract);
        EventBus.Subscribe<GameObject>(EventType.BOARD_RESETCONTRACT, OnButtonPromtPress);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.BOARD_CONTRACTUPDATE, OnDisplayUpdate_Contract);
        EventBus.Unsubscribe<GameObject>(EventType.BOARD_RESETCONTRACT, OnButtonPromtPress);
    }

    private void OnDisplayUpdate_Contract()
    {
        Set_RobotTier_Text(_contract_Data.RobotTier);
        Set_RobotType_Text(_contract_Data.RobotType);
        Set_Payout_Text();
        Set_CountTimer_Text();
        Set_RobotImage(_contract_Data.RobotSprite);
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
                _contract_RobotTier_Text.text = "Tier: I";
                break;
            case RobotTier.II:
                _contract_RobotTier_Text.text = "Tier: II";
                break;
            case RobotTier.III:
                _contract_RobotTier_Text.text = "Tier: III";
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
            case RobotType.HouseKeeper:
                _contract_RobotType_Text.text = "House Keeper";
                break;
            case RobotType.PoliceBot:
                _contract_RobotType_Text.text = "Police Bot";
                break;
            case RobotType.FootBall:
                _contract_RobotType_Text.text = "Football Bot";
                break;
            case RobotType.YardMaintenance:
                _contract_RobotType_Text.text = "Yard Maintenance Bot";
                break;
            default:
                break;
        }

    }

    public void Set_Payout_Text()
    {
        _contract_Payout_Text.text = "Credit: " + _contract_Data.Value_Credit;
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

    public void OnContractButtonPress()
    {
        _contract_Button_Promt.SetActive(true);
        _contract_Information_Promt.SetActive(false);
        EventBus.Publish(EventType.BOARD_RESETCONTRACT, this.gameObject);
    }

    public void OnButtonPromtPress(bool isContractAccepted)
    {
        if (isContractAccepted)
        {
            ContractBoard_Manager.Instance.OnContractAccepted();
            Debug.Log("adding contract");
            EventBus.Publish(EventType.PLAYER_ADDCONTRACT, this.gameObject);
            EventBus.Publish(EventType.BOARD_RESETCONTRACT, this.gameObject);
            EventBus.Publish(EventType.PLAYER_CONTRACTUPDATE);
            Destroy(this.gameObject);
        }
        else
        {
            _contract_Button_Promt.SetActive(false);
            _contract_Information_Promt.SetActive(true);
        }
    }

    public void OnButtonPromtPress(GameObject activeContract)
    {
        if (this.gameObject != activeContract)
        {
            _contract_Button_Promt.SetActive(false);
            _contract_Information_Promt.SetActive(true);
        }
    }
}
