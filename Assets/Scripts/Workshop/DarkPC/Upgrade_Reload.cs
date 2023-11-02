using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Agyr.Workshop;

public class Upgrade_Reload : Upgrade_UI_Behavior
{
    [SerializeField]
    private int _upgradeCurrentLevel = 1;
    [SerializeField]
    private int _upgradeMaxLevel = 3;
    [SerializeField]
    private int _upgradeCost = 150;
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
                _upgradeCurrentLevel++;
                UpdateText(_upgradeCost, _upgradeDesc, _upgradeCurrentLevel, _upgradeMaxLevel);
                DarkWebPC_Manager.Instance.Gun.ReloadTime -= .2f;
            }
        }
    }
}
