using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Upgrade_UI_Behavior : MonoBehaviour
{
    /*
    [SerializeField]
    private int _upgradeCountCurrent;
    [SerializeField]
    private int _upgradeCountMax;
    [SerializeField]
    private int _upgradeCost;
    [SerializeField]
    private TextMeshProUGUI _textDesc;
    [SerializeField]
    private TextMeshProUGUI _textCost;
    [SerializeField]
    private TextMeshProUGUI _textLevel;
    [SerializeField]
    private TextMeshProUGUI _textButton;
    [SerializeField]
    private Button _button;
    [SerializeField]
    private GameObject _textSold;

    public void Upgrade()
    {
        if (_upgradeCountCurrent < _upgradeCountMax)
        {
            if (WorkshopManager.Instance.WorkshopStorage.CreditCount >= _upgradeCostHealth)
            {
                WorkshopManager.Instance.WorkshopStorage.CreditCount -= _upgradeCostHealth;
                _upgradeCountCurrentHealth++;
                EventBus.Publish(EventType.UPGRADE_HEALTH, 5f);
                _textHealthCost.text = "Cost: " + _upgradeCostHealth;
                _textHealthDesc.text = "+25 to Max Health";
                _textHealthLevel.text = "Level " + _upgradeCountCurrentHealth + " of " + _upgradeCountMaxHealth;
            }

            if (_upgradeCountCurrentHealth == _upgradeCountMaxHealth)
            {
                _buttonHealth.interactable = false;
                _textHealthSold.SetActive(true);
            }
        }
    }
    */

}
