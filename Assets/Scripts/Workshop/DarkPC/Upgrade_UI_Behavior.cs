using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Agyr.Workshop;

public class Upgrade_UI_Behavior : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI _textDesc;
    [SerializeField]
    public TextMeshProUGUI _textCost;
    [SerializeField]
    public TextMeshProUGUI _textLevel;
    [SerializeField]
    public Button _button;
    [SerializeField]
    public TextMeshProUGUI _textBuy;

    public void UpdateText(int _upgradeCost, string _upgradeDesc, int _upgradeCountCurrent, int _upgradeCountMax)
    {
        _textCost.text = _upgradeCost.ToString();
        _textDesc.text = _upgradeDesc;
        _textLevel.text = "Level " + _upgradeCountCurrent + " of " + _upgradeCountMax;

        if (_upgradeCountCurrent == _upgradeCountMax)
        {
            _textBuy.text = "Sold Out";
            _button.interactable = false;
        }
    }
}
