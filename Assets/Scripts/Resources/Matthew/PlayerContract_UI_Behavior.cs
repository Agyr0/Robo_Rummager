using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Agyr.Workshop;
using UnityEngine.UIElements;

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
    private UnityEngine.UI.Image _contract_Image;
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
    public int Contract_Resource_Scrap_Text
    {
        set
        {
            if (value == 0)
            {
                _contract_Resource_Scrap_Text.transform.parent.parent.gameObject.SetActive(false);
            }
            else
            {
                string tempValue = "";
                tempValue += value.ToString() + "/" + (WorkshopManager.Instance.WorkshopStorage.MetalScrapCount);

                Debug.Log(value);
                Debug.Log(WorkshopManager.Instance.WorkshopStorage.MetalScrapCount);

                if (value > WorkshopManager.Instance.WorkshopStorage.MetalScrapCount)
                {
                    _contract_Resource_Scrap_Text.transform.parent.parent.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.red;
                }
                else
                {
                    _contract_Resource_Scrap_Text.transform.parent.parent.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.green;
                }

                _contract_Resource_Scrap_Text.text = tempValue;
            }
        }
    }
    public int Contract_Resource_Wires_Text
    {
        set
        {
            if (value == 0)
            {
                _contract_Resource_Wires_Text.transform.parent.parent.gameObject.SetActive(false);
            }
            else
            {
                string tempValue = "";
                tempValue += value.ToString() + "/" + (WorkshopManager.Instance.WorkshopStorage.WireCount);

                if (value > WorkshopManager.Instance.WorkshopStorage.WireCount)
                {
                    _contract_Resource_Wires_Text.transform.parent.parent.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.red;
                }
                else
                {
                    _contract_Resource_Wires_Text.transform.parent.parent.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.green;
                }

                _contract_Resource_Wires_Text.text = tempValue;
            }
        }
    }
    public int Contract_Resource_Oilcan_Text
    {
        set
        {
            if (value == 0)
            {
                _contract_Resource_Oilcan_Text.transform.parent.parent.gameObject.SetActive(false);
            }
            else
            {
                string tempValue = "";
                tempValue += value.ToString() + "/" + (WorkshopManager.Instance.WorkshopStorage.OilCount);

                if (value > WorkshopManager.Instance.WorkshopStorage.OilCount)
                {
                    _contract_Resource_Oilcan_Text.transform.parent.parent.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.red;
                }
                else
                {
                    _contract_Resource_Oilcan_Text.transform.parent.parent.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.green;
                }

                _contract_Resource_Oilcan_Text.text = tempValue;
            }
        }
    }
    public int Contract_Resource_Motherboards_Text
    {
        set
        {
            if (value == 0)
            {
                _contract_Resource_Motherboards_Text.transform.parent.parent.gameObject.SetActive(false);
            }
            else
            {
                string tempValue = "";
                tempValue += value.ToString() + "/" + (WorkshopManager.Instance.WorkshopStorage.MotherBoardCount);

                if (value > WorkshopManager.Instance.WorkshopStorage.MotherBoardCount)
                {
                    _contract_Resource_Motherboards_Text.transform.parent.parent.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.red;
                }
                else
                {
                    _contract_Resource_Motherboards_Text.transform.parent.parent.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.green;
                }

                _contract_Resource_Motherboards_Text.text = tempValue;
            }
        }
    }
    public int Contract_Resource_AdvSensor_Text
    {
        set
        {
            if (value == 0)
            {
                _contract_Resource_AdvSensors_Text.transform.parent.parent.gameObject.SetActive(false);
            }
            else
            {
                string tempValue = "";
                tempValue += value.ToString() + "/" + (WorkshopManager.Instance.WorkshopStorage.SensorCount);

                if (value > WorkshopManager.Instance.WorkshopStorage.SensorCount)
                {
                    _contract_Resource_AdvSensors_Text.transform.parent.parent.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.red;
                }
                else
                {
                    _contract_Resource_AdvSensors_Text.transform.parent.parent.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.green;
                }

                _contract_Resource_AdvSensors_Text.text = tempValue;
            }
        }
    }
    public int Contract_Resource_RadWaste_Text
    {
        set
        {
            if (value == 0)
            {
                _contract_Resource_RadWaste_Text.transform.parent.parent.gameObject.SetActive(false);
            }
            else
            {
                string tempValue = "";
                tempValue += value.ToString() + "/" + (WorkshopManager.Instance.WorkshopStorage.RadioactiveWasteCount);

                

                if (value > WorkshopManager.Instance.WorkshopStorage.RadioactiveWasteCount)
                {
                    _contract_Resource_RadWaste_Text.transform.parent.parent.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.red;
                }
                else
                {
                    _contract_Resource_RadWaste_Text.transform.parent.parent.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.green;
                }

                _contract_Resource_RadWaste_Text.text = tempValue;
            }
        }
    }
    public int Contract_Resource_ZCrystal_Text
    {
        set
        {
            if (value == 0)
            {
                _contract_Resource_ZCrystal_Text.transform.parent.parent.gameObject.SetActive(false);
            }
            else
            {
                string tempValue = "";
                tempValue += value.ToString() + "/" + (WorkshopManager.Instance.WorkshopStorage.ZCrystalCount);

                if (value > WorkshopManager.Instance.WorkshopStorage.ZCrystalCount)
                {
                    _contract_Resource_ZCrystal_Text.transform.parent.parent.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.red;
                }
                else
                {
                    _contract_Resource_ZCrystal_Text.transform.parent.parent.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.green;
                }

                _contract_Resource_ZCrystal_Text.text = tempValue;
            }
        }
    }
    public int Contract_Resource_BlackMatter_Text
    {
        set
        {
            if (value == 0)
            {
                _contract_Resource_BlackMatter_Text.transform.parent.parent.gameObject.SetActive(false);
            }
            else
            {
                string tempValue = "";
                tempValue += value.ToString() + "/" + (WorkshopManager.Instance.WorkshopStorage.BlackMatterCount);

                if (value > WorkshopManager.Instance.WorkshopStorage.BlackMatterCount)
                {
                    _contract_Resource_BlackMatter_Text.transform.parent.parent.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.red;
                }
                else
                {
                    _contract_Resource_BlackMatter_Text.transform.parent.parent.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.green;
                }

                _contract_Resource_BlackMatter_Text.text = tempValue;
            }
        }
    }
}