using Agyr.Workshop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_3DPrinter : Upgrade_UI_Behavior
{

    private int _upgradeCurrentLevel;
    private int _upgradeMaxLevel;

    [SerializeField]
    private int _upgradeCost = 5;
    [SerializeField]
    private string _upgradeDesc = "";

    

    private void Start()
    {
        //WorkshopManager.Instance.WorkshopStorage.CreditCount += 100;

        _upgradeCurrentLevel = 0;
        _upgradeMaxLevel = DarkWebPC_Manager.Instance._3DPrinterList.Count;
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
                DarkWebPC_Manager.Instance._3DPrinterList[_upgradeCurrentLevel].SetActive(true);
                _upgradeCurrentLevel++;
                UpdateText(_upgradeCost, _upgradeDesc, _upgradeCurrentLevel, _upgradeMaxLevel);
            }
        }
    }
}
