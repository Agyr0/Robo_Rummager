using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
public class PlayerContract_UI_Behavior : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _contract_RobotTier_Text;
    [SerializeField]
    private TextMeshProUGUI _contract_RobotType_Text;
    [SerializeField]
    private TextMeshProUGUI _contract_CountTimer_Text;
    [SerializeField]
    private TextMeshProUGUI _contract_Payout_Text;
    [SerializeField]
    private TextMeshProUGUI _contract_Resource_Scrap_Text;
    [SerializeField]
    private TextMeshProUGUI _contract_Resource_Wires_Text;
    [SerializeField]
    private TextMeshProUGUI _contract_Resource_Oilcan_Text;
    [SerializeField]
    private TextMeshProUGUI _contract_Resource_Motherboards_Text;
    [SerializeField]
    private TextMeshProUGUI _contract_Resource_AdvSensors_Text;
    [SerializeField]
    private TextMeshProUGUI _contract_Resource_BlackMatter_Text;
    [SerializeField]
    private TextMeshProUGUI _contract_Resource_RadWaste_Text;
    [SerializeField]
    private TextMeshProUGUI _contract_Resource_ZCrystal_Text;
    [SerializeField]
    private Image _contract_Image;
    public GameObject contract_Failed;
    public GameObject contract_Info;
    public GameObject contract_Unassigned;
    public string Contract_RobotTier_Text
    {
        get { return _contract_RobotTier_Text.text; }
        set { _contract_RobotTier_Text.text = value; }
    }
    public string Contract_RobotType_Text
    {
        get { return _contract_RobotType_Text.text; }
        set { _contract_RobotType_Text.text = value; }
    }
    public string Contract_CountTimer_Text
    {
        get { return _contract_CountTimer_Text.text; }
        set { _contract_CountTimer_Text.text = value; }
    }
    public string Contract_Payout_Text
    {
        get { return _contract_Payout_Text.text; }
        set { _contract_Payout_Text.text = value; }
    }
    public Sprite Contract_Image
    {
        get { return _contract_Image.sprite; }
        set { _contract_Image.sprite = value; }
    }
    public string Contract_Resource_Scrap_Text
    {
        set
        {
            if (value.Split('/')[0] == "0")
            {
                _contract_Resource_Scrap_Text.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                _contract_Resource_Scrap_Text.text = value;
            }
        }
    }
    public string Contract_Resource_Wires_Text
    {
        set
        {
            if (value.Split('/')[0] == "0")
            {
                _contract_Resource_Wires_Text.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                _contract_Resource_Wires_Text.text = value;
            }
        }
    }
    public string Contract_Resource_Oilcan_Text
    {
        set
        {
            if (value.Split('/')[0] == "0")
            {
                _contract_Resource_Oilcan_Text.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                _contract_Resource_Oilcan_Text.text = value;
            }
        }
    }
    public string Contract_Resource_Motherboards_Text
    {
        set
        {
            if (value.Split('/')[0] == "0")
            {
                _contract_Resource_Motherboards_Text.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                _contract_Resource_Motherboards_Text.text = value;
            }
        }
    }
    public string Contract_Resource_AdvSensor_Text
    {
        set
        {
            if (value.Split('/')[0] == "0")
            {
                _contract_Resource_AdvSensors_Text.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                _contract_Resource_AdvSensors_Text.text = value;
            }
        }
    }
    public string Contract_Resource_RadWaste_Text
    {
        set
        {
            if (value.Split('/')[0] == "0")
            {
                _contract_Resource_RadWaste_Text.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                _contract_Resource_RadWaste_Text.text = value;
            }
        }
    }
    public string Contract_Resource_ZCrystal_Text
    {
        set
        {
            if (value.Split('/')[0] == "0")
            {
                _contract_Resource_ZCrystal_Text.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                _contract_Resource_ZCrystal_Text.text = value;
            }
        }
    }
    public string Contract_Resource_BlackMatter_Text
    {
        set
        {
            if (value.Split('/')[0] == "0")
            {
                _contract_Resource_BlackMatter_Text.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                _contract_Resource_BlackMatter_Text.text = value;
            }
        }
    }
}