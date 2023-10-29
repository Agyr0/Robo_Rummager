using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Agyr.Workshop;

public class Upgrade_UI_Behavior : MonoBehaviour
{
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

    public void UpdateText(int _upgradeCost, string _upgradeDesc, int _upgradeCountCurrent, int _upgradeCountMax)
    {
        _textCost.text = "Cost: " + _upgradeCost;
        _textDesc.text = _upgradeDesc;
        _textLevel.text = "Level " + _upgradeCountCurrent + " of " + _upgradeCountMax;

        if (_upgradeCountCurrent == _upgradeCountMax)
        {
            _button.interactable = false;
            _textSold.SetActive(true);
        }
    }
}
