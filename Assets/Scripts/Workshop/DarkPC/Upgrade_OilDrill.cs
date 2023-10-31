using Agyr.Workshop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_OillDrill : Upgrade_UI_Behavior
{
    [SerializeField]
    private int _upgradeCurrentLevel = 0;
    [SerializeField]
    private int _upgradeMaxLevel = 1;
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
        if (_upgradeCurrentLevel < _upgradeMaxLevel)
        {
            if (WorkshopManager.Instance.WorkshopStorage.CreditCount >= _upgradeCost)
            {
                WorkshopManager.Instance.WorkshopStorage.CreditCount -= _upgradeCost;
                PrinterManager.Instance.CanPrintOil = true;
                _upgradeCurrentLevel++;
                UpdateText(_upgradeCost, _upgradeDesc, _upgradeCurrentLevel, _upgradeMaxLevel);
            }
        }
    }
}
