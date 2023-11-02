using Agyr.Workshop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_Dash : Upgrade_UI_Behavior
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
        Debug.Log("Upgrade Button was pressed");
        if (_upgradeCurrentLevel < _upgradeMaxLevel)
        {
            if (WorkshopManager.Instance.WorkshopStorage.CreditCount >= _upgradeCost)
            {
                AudioManager.Instance.PlayClip(this.GetComponent<AudioSource>(), AudioManager.Instance.effectAudio[6].myControllers[1]);
                WorkshopManager.Instance.WorkshopStorage.CreditCount -= _upgradeCost;
                _upgradeCurrentLevel++;
                UpdateText(_upgradeCost, _upgradeDesc, _upgradeCurrentLevel, _upgradeMaxLevel);
                GameManager.Instance.playerController.AdjustMaxDash(2f);
            }
        }
    }
}
