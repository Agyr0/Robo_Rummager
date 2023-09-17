using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WorkshopStorage
{
    #region Storage fields
    private int _creditCount;

    private int _motherBoardCount;
    private int _wireCount;
    private int _oilCount;
    private int _metalScrapCount;
    private int _sensorCount;
    private int _zCrystalCount;
    private int _radioactiveWasteCount;
    private int _blackMatterCount;
    [SerializeField,Tooltip("This is more as a debug to see what you have in the storage"), NamedArray(new string[] { "Credits", "Mother Boards", "Wires", "Oil", "Metal Scrap", "Sensors", "Z Crystals", "Radioactive Waste", "Black Matter" })]
    private int[] storage = new int[9];

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
    public int CreditCount
    {
        get 
        {
            //_creditCount = storage[0];
            return _creditCount; 
        }
        set
        {
            if (_creditsText != null)
                _creditsText.text = _creditCount.ToString();
            _creditCount = value;
            storage[0] = _creditCount;
        }
    }
    public int MotherBoardCount
    {
        get 
        {
            //_motherBoardCount = storage[1];
            return _motherBoardCount; 
        }
        set
        {
            if (_motherBoardText != null)
                _motherBoardText.text = _motherBoardCount.ToString();
            _motherBoardCount = value;
            storage[1] = _motherBoardCount;
        }
    }
    public int WireCount
    {
        get 
        {
            //_wireCount = storage[2];
            return _wireCount; 
        }
        set
        {
            if (_wiresText != null)
                _wiresText.text = _wireCount.ToString();
            _wireCount = value;
            storage[2] = _wireCount;
        }
    }
    public int OilCount
    {
        get 
        {
            //_oilCount = storage[3];
            return _oilCount; 
        }
        set
        {
            if (_oilText != null)
                _oilText.text = _oilCount.ToString();
            _oilCount = value;
            storage[3] = _oilCount;
        }
    }
    public int MetalScrapCount
    {
        get 
        {
            //_metalScrapCount = storage[4];
            return _metalScrapCount; 
        }
        set
        {
            if (_metalScrapText != null)
                _metalScrapText.text = _metalScrapCount.ToString();
            _metalScrapCount = value;
            storage[4] = _metalScrapCount;
        }
    }
    public int SensorCount
    {
        get 
        {
            //_sensorCount = storage[5];
            return _sensorCount; 
        }
        set
        {
            if (_sensorsText != null)
                _sensorsText.text = _sensorCount.ToString();
            _sensorCount = value;
            storage[5] = _sensorCount;
        }
    }
    public int ZCrystalCount
    {
        get 
        {
            //_zCrystalCount = storage[6];
            return _zCrystalCount; 
        }
        set
        {
            if (_zCrystalText != null)
                _zCrystalText.text = _zCrystalCount.ToString();
            _zCrystalCount = value;
            storage[6] = _zCrystalCount;
        }
    }
    public int RadioactiveWasteCount
    {
        get 
        {
            //_radioactiveWasteCount = storage[7];    
            return _radioactiveWasteCount; 
        }
        set
        {
            if (_radioactiveWasteText != null)
                _radioactiveWasteText.text = _radioactiveWasteCount.ToString();
            _radioactiveWasteCount = value;
            storage[7] = _radioactiveWasteCount;
        }
    }
    public int BlackMatterCount
    {
        get 
        {
            //_blackMatterCount = storage[8];
            return _blackMatterCount; 
        }
        set
        {
            if (_blackMatterText != null)
                _blackMatterText.text = _blackMatterCount.ToString();
            _blackMatterCount = value;
            storage[8] = _blackMatterCount;
        }
    }

    #endregion


}
