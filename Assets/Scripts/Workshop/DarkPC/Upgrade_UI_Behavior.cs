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
    public GameObject _textBuy;
    [SerializeField]
    public GameObject _textSold;

    public void UpdateText(int _upgradeCost, string _upgradeDesc, int _upgradeCountCurrent, int _upgradeCountMax)
    {
        _textCost.text = _upgradeCost.ToString();
        _textDesc.text = _upgradeDesc;
        _textLevel.text = "Level " + _upgradeCountCurrent + " of " + _upgradeCountMax;

        if (_upgradeCountCurrent == _upgradeCountMax)
        {
            _button.interactable = false;
            _textSold.SetActive(true);
            _textBuy.SetActive(false);
        }
    }
}
