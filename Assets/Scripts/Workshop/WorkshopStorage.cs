using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Agyr.Workshop
{
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
        [SerializeField, Tooltip("This is more as a debug to see what you have in the storage"), NamedArray(new string[] { "Credits", "Mother Boards", "Wires", "Oil", "Metal Scrap", "Sensors", "Z Crystals", "Radioactive Waste", "Black Matter" })]
        private int[] storage = new int[9];

        #endregion


        #region Parent Icons

        public Sprite creditIcon;
        public Sprite motherBoardIcon;
        public Sprite wireIcon;
        public Sprite oilIcon;
        public Sprite metalScrapIcon;
        public Sprite sensorIcon;
        public Sprite zCrystalIcon;
        public Sprite radioactiveWasteIcon;
        public Sprite blackMatterIcon;
        #endregion

        #region Binding Properties
        public Property<int> CreditProp;
        public Property<int> MotherBoardProp;
        public Property<int> WireProp;
        public Property<int> OilProp;
        public Property<int> MetalScrapProp;
        public Property<int> SensorProp;
        public Property<int> ZCrystalProp;
        public Property<int> RadioactiveWasteProp;
        public Property<int> BlackMatterProp;
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
                _creditCount = value;
                CreditProp.Value = value;
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
                
                _motherBoardCount = value;
                MotherBoardProp.Value = value;
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
                _wireCount = value;
                WireProp.Value = value;
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
               
                _oilCount = value;
                OilProp.Value = value;
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
                
                _metalScrapCount = value;
                MetalScrapProp.Value = value;
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
                
                _sensorCount = value;
                SensorProp.Value = value;   
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
                
                _zCrystalCount = value;
                ZCrystalProp.Value = value;
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
                
                _radioactiveWasteCount = value;
                RadioactiveWasteProp.Value = value;
                storage[7] = _radioactiveWasteCount;
            }
        }
        public int BlackMatterCount
        {
            get
            {
               // _blackMatterCount = storage[8];
                return _blackMatterCount;
            }
            set
            {
                
                _blackMatterCount = value;
                BlackMatterProp.Value = value;
                storage[8] = _blackMatterCount;
            }
        }

        #endregion


    }
}