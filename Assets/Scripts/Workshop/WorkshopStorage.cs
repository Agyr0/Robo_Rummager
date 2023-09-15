using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WorkshopStorage
{
    #region Storage fields
    private float _creditCount;

    private float _motherBoardCount;
    private float _wireCount;
    private float _oilCount;
    private float _metalScrapCount;
    private float _sensorCount;
    private float _zCrystalCount;
    private float _radioactiveWasteCount;
    private float _blackMatterCount;

    #endregion

    #region Text Components
    [SerializeField]
    private Text _creditsText;
    [SerializeField]
    private Text _motherBoardText;
    [SerializeField]
    private Text _wiresText;
    [SerializeField]
    private Text _oilText;
    [SerializeField]
    private Text _metalScrapText;
    [SerializeField]
    private Text _sensorsText;
    [SerializeField]
    private Text _zCrystalText;
    [SerializeField]
    private Text _radioactiveWasteText;
    [SerializeField]
    private Text _blackMatterText;

    #endregion

    #region Properties
    public float CreditCount
    {
        get { return _creditCount; }
        set
        {
            if (_creditsText != null)
                _creditsText.text = _creditCount.ToString();
            _creditCount = value;
        }
    }
    public float MotherBoardCount
    {
        get { return _motherBoardCount; }
        set
        {
            if (_motherBoardText != null)
                _motherBoardText.text = _motherBoardCount.ToString();
            _motherBoardCount = value;
        }
    }
    public float WireCount
    {
        get { return _wireCount; }
        set
        {
            if (_wiresText != null)
                _wiresText.text = _wireCount.ToString();
            _wireCount = value;
        }
    }
    public float OilCount
    {
        get { return _oilCount; }
        set
        {
            if (_oilText != null)
                _oilText.text = _oilCount.ToString();
            _oilCount = value;
        }
    }
    public float MetalScrapCount
    {
        get { return _metalScrapCount; }
        set
        {
            if (_metalScrapText != null)
                _metalScrapText.text = _metalScrapCount.ToString();
            _metalScrapCount = value;
        }
    }
    public float SensorCount
    {
        get { return _sensorCount; }
        set
        {
            if (_sensorsText != null)
                _sensorsText.text = _sensorCount.ToString();
            _sensorCount = value;
        }
    }
    public float ZCrystalCount
    {
        get { return _zCrystalCount; }
        set
        {
            if (_zCrystalText != null)
                _zCrystalText.text = _zCrystalCount.ToString();
            _zCrystalCount = value;
        }
    }
    public float RadioactiveWasteCount
    {
        get { return _radioactiveWasteCount; }
        set
        {
            if (_radioactiveWasteText != null)
                _radioactiveWasteText.text = _radioactiveWasteCount.ToString();
            _radioactiveWasteCount = value;
        }
    }
    public float BlackMatterCount
    {
        get { return _blackMatterCount; }
        set
        {
            if (_blackMatterText != null)
                _blackMatterText.text = _blackMatterCount.ToString();
            _blackMatterCount = value;
        }
    }

    #endregion


}
