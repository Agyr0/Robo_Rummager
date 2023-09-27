using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Agyr.Workshop
{
    public class WorkshopStorageUIController : MonoBehaviour
    {
        private WorkshopManager workshopManager;
        private WorkshopStorage workshopStorage;


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

        [Space(20)]

        #region Icons
        [SerializeField]
        private Image _creditsIcon;
        [SerializeField]
        private Image _motherBoardIcon;
        [SerializeField]
        private Image _wiresIcon;
        [SerializeField]
        private Image _oilIcon;
        [SerializeField]
        private Image _metalScrapIcon;
        [SerializeField]
        private Image _sensorsIcon;
        [SerializeField]
        private Image _zCrystalIcon;
        [SerializeField]
        private Image _radioactiveWasteIcon;
        [SerializeField]
        private Image _blackMatterIcon;
        #endregion



        private void Start()
        {
            workshopManager = WorkshopManager.Instance;
            workshopStorage = workshopManager.WorkshopStorage;

            #region Bind Properties
            RuntimeBindingExtensions.BindProperty(_creditsText, workshopStorage.CreditProp);
            RuntimeBindingExtensions.BindProperty(_motherBoardText, workshopStorage.MotherBoardProp);
            RuntimeBindingExtensions.BindProperty(_wiresText, workshopStorage.WireProp);
            RuntimeBindingExtensions.BindProperty(_oilText, workshopStorage.OilProp);
            RuntimeBindingExtensions.BindProperty(_metalScrapText, workshopStorage.MetalScrapProp);
            RuntimeBindingExtensions.BindProperty(_sensorsText, workshopStorage.SensorProp);
            RuntimeBindingExtensions.BindProperty(_zCrystalText, workshopStorage.ZCrystalProp);
            RuntimeBindingExtensions.BindProperty(_radioactiveWasteText, workshopStorage.RadioactiveWasteProp);
            RuntimeBindingExtensions.BindProperty(_blackMatterText, workshopStorage.BlackMatterProp);
            #endregion

            #region Assign Sprites
            _creditsIcon.sprite = workshopStorage.creditIcon;
            _motherBoardIcon.sprite = workshopStorage.motherBoardIcon;
            _wiresIcon.sprite = workshopStorage.wireIcon;
            _oilIcon.sprite = workshopStorage.oilIcon;
            _metalScrapIcon.sprite = workshopStorage.metalScrapIcon;
            _sensorsIcon.sprite = workshopStorage.sensorIcon;
            _zCrystalIcon.sprite = workshopStorage.zCrystalIcon;
            _radioactiveWasteIcon.sprite = workshopStorage.radioactiveWasteIcon;
            _blackMatterIcon.sprite = workshopStorage.blackMatterIcon;
            #endregion
        }
    }





}