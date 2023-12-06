using Agyr.Workshop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Upgrade_ScannerSpeed : Upgrade_UI_Behavior
{
    [SerializeField]
    private int _upgradeCurrentLevel = 1;
    [SerializeField]
    private int _upgradeMaxLevel = 5;
    [SerializeField]
    private int _upgradeCost = 5;
    [SerializeField]
    private string _upgradeDesc = "";

    [SerializeField]
    private VisualEffect scannerVFX;

    [SerializeField]
    private float _speedIncreaseValue;

    private float _speedCurrent;

    private void Start()
    {
        UpdateText(_upgradeCost, _upgradeDesc, _upgradeCurrentLevel, _upgradeMaxLevel);
    }

    public void Upgrade()
    {
        Debug.Log("Upgrade Button was pressed");
        if (_upgradeCurrentLevel < _upgradeMaxLevel)
        {
            if (WorkshopManager.Instance.WorkshopStorage.CreditCount >= _upgradeCost)
            {
                AudioManager.Instance.PlayClip(this.GetComponent<AudioSource>(), AudioManager.Instance.effectAudio[6].myControllers[1]);
                WorkshopManager.Instance.WorkshopStorage.CreditCount -= _upgradeCost;
                GameManager.Instance.inventoryManager.CreditText = WorkshopManager.Instance.WorkshopStorage.CreditCount.ToString();
                _upgradeCurrentLevel++;
                UpdateText(_upgradeCost, _upgradeDesc, _upgradeCurrentLevel, _upgradeMaxLevel);

                _speedCurrent = scannerVFX.GetFloat("Lifetime");
                scannerVFX.SetFloat("Lifetime", _speedCurrent - _speedIncreaseValue);
            }
        }
    }
}
