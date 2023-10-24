using Agyr.Workshop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_WrenchDamage : Upgrade_UI_Behavior
{
    [SerializeField]
    private int _upgradeCurrentLevel = 1;
    [SerializeField]
    private int _upgradeMaxLevel = 5;
    [SerializeField]
    private int _upgradeCost = 5;
    [SerializeField]
    private string _upgradeDesc = "";

    private void Start()
    {
        UpdateText(_upgradeCost, _upgradeDesc, _upgradeCurrentLevel, _upgradeMaxLevel);
    }

    public void Upgrade()
    {
        if (_upgradeCountCurrentDamage < _upgradeCountMaxDamage)
        {
            if (WorkshopManager.Instance.WorkshopStorage.CreditCount >= _upgradeCostDamage)
            {
                WorkshopManager.Instance.WorkshopStorage.CreditCount -= _upgradeCostDamage;
                _wrench.Damage += 10;
                _upgradeCountCurrentDamage++;
                _textDamageCost.text = _upgradeCostDamage.ToString();
                _textDamageDesc.text = "+10 to Wrench Swing Damage";
                _textDamageLevel.text = "Level " + _upgradeCountCurrentDamage + " of " + _upgradeCountMaxDamage;
            }

            if (_upgradeCountCurrentDamage == _upgradeCountMaxDamage)
            {
                _buttonDamage.interactable = false;
                _textDamageSold.SetActive(true);
            }
        }
    }
}
