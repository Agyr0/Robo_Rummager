using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Agyr.Workshop;

public class Upgrade_UI_Behavior : MonoBehaviour
{
    private TextMeshProUGUI _textDesc;
    private TextMeshProUGUI _textCost;
    private TextMeshProUGUI _textLevel;
    private Button _button;
    private TextMeshProUGUI _textBuy;

    protected void OnEnable()
    {
        _textCost = this.gameObject.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        _textLevel = this.gameObject.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();
        _textDesc = this.gameObject.transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>();
        _button = this.gameObject.transform.GetChild(0).GetChild(4).GetComponent<Button>();
        _textBuy = this.gameObject.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>();
    }

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
