using Agyr.Workshop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_WasteFabricator : Upgrade_UI_Behavior
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
                AudioManager.Instance.PlayClip(this.GetComponent<AudioSource>(), AudioManager.Instance.effectAudio[6].myControllers[1]);
                WorkshopManager.Instance.WorkshopStorage.CreditCount -= _upgradeCost;
                GameManager.Instance.inventoryManager.CreditText = WorkshopManager.Instance.WorkshopStorage.CreditCount.ToString();
                foreach (var item in DarkWebPC_Manager.Instance._3DPrinterList)
                {
                    item.GetComponent<PrinterManager>().CanPrintWaste = true;
                }
                _upgradeCurrentLevel++;
                UpdateText(_upgradeCost, _upgradeDesc, _upgradeCurrentLevel, _upgradeMaxLevel);
            }
        }
    }
}
