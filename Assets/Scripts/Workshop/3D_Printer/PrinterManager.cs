using Agyr.Workshop;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
using Image = UnityEngine.UI.Image;

public class PrinterManager : MonoBehaviour, IInteractable
{

    [SerializeField]
    private int printerID;

    [SerializeField]
    private PrinterState _printerState = PrinterState.Available;

    public int clock_PrintTime = 0;

    public int printedAmount = 0;
    public int printedMaxAmount = 10;

    [SerializeField]
    public Button printResourceCollectionButton;

    [SerializeField]
    public Button printHaltButton;

    public List<Sprite> ResourceImageList;

    public List<GameObject> ResourceDataList;

    [SerializeField]
    private VisualEffect _printingEffect_VFX;

    [SerializeField]
    private GameObject _itemSpawnLocation;

    [SerializeField]
    private GameObject printMenuUI;

    [SerializeField]
    private GameObject printerCountDownUI;

    [SerializeField]
    private GameObject printerTimerTextUI;

    [SerializeField]
    private GameObject printerCompleteUI;

    [SerializeField]
    private Image printerResourceImage;

    [SerializeField]
    private TextMeshProUGUI printerTime_Text;

    [SerializeField]
    private TextMeshProUGUI printerCollect_Text;

    [SerializeField]
    private TextMeshProUGUI printerHalt_Text;

    [SerializeField]
    private Button printButtonOil;

    [SerializeField]
    private Button printButtonMotherboard;

    [SerializeField]
    private Button printButtonAdvSensor;

    [SerializeField]
    private Button printButtonWaste;

    [SerializeField]
    private Button printButtonDarkMatter;

    [SerializeField]
    private Button printButtonZ_Crystal;

    #region PrintsUnlocked
    [SerializeField]
    private bool _canPrintOil = false;
    [SerializeField]
    private bool _canPrintAdvSensor = false;
    [SerializeField]
    private bool _canPrintMotherboard = false;
    [SerializeField]
    private bool _canPrintWaste = false;
    [SerializeField]
    private bool _canPrintZ_Crystal = false;
    [SerializeField]
    private bool _canPrintDarkMatter = false;

    public bool CanPrintOil
    {
        set
        {
            _canPrintOil = value;
            printButtonOil.interactable = value;
            printButtonOil.transform.GetChild(1).gameObject.SetActive(!value);
        }
    }

    public bool CanPrintAdvSensor
    {
        set
        {
            _canPrintAdvSensor = value;
            printButtonAdvSensor.interactable = value;
            printButtonAdvSensor.transform.GetChild(1).gameObject.SetActive(!value);
        }
    }

    public bool CanPrintMotherboard
    {
        set
        {
            _canPrintMotherboard = value;
            printButtonMotherboard.interactable = value;
            printButtonMotherboard.transform.GetChild(1).gameObject.SetActive(!value);
        }
    }

    public bool CanPrintWaste
    {
        set
        {
            _canPrintWaste = value;
            printButtonWaste.interactable = value;
            printButtonWaste.transform.GetChild(1).gameObject.SetActive(!value);
        }
    }

    public bool CanPrintZCrystal
    {
        set
        {
            _canPrintZ_Crystal = value;
            printButtonZ_Crystal.interactable = value;
            printButtonZ_Crystal.transform.GetChild(1).gameObject.SetActive(!value);
        }
    }

    public bool CanPrintDarkMatter
    {
        set
        {
            _canPrintDarkMatter = value;
            printButtonDarkMatter.interactable = value;
            printButtonDarkMatter.transform.GetChild(1).gameObject.SetActive(!value);
        }
    }

    #endregion
    [SerializeField]
    private GameObject selectionCanvas;

    private BilboardScaler scaler;

    private int originalWeaponIndex;

    [SerializeField]
    private ResourceType printingResource;

    private Coroutine handleUI;

    private bool isOn = false;

    public void Awake()
    {
        DarkWebPC_Manager.Instance._3DPrinterList.Add(this.gameObject);
        this.gameObject.SetActive(false);
    }

    private void Start()
    {
        CanPrintOil = _canPrintOil;
        CanPrintMotherboard = _canPrintMotherboard;
        CanPrintAdvSensor = _canPrintAdvSensor;
        CanPrintDarkMatter = _canPrintDarkMatter;
        CanPrintWaste = _canPrintWaste;
        CanPrintZCrystal = _canPrintZ_Crystal;
    }

    public int Clock_PrintTime
    {
        get
        {
            return clock_PrintTime;
        }
        set
        {
            clock_PrintTime = value;
            string mintutes = ((float)clock_PrintTime / 60).ToString().Split('.')[0];
            string seconds = (clock_PrintTime % 60).ToString();

            if (seconds.Length == 2)
            {
                printerTime_Text.text = "Printing: " + mintutes + ':' + seconds;
            }
            else
            {
                printerTime_Text.text = "Printing: " + mintutes + ":0" + seconds;
            }

        }
    }

    public void LeavePrinter()
    {
        HandleInteract();
    }

    public void StartPrintOrder(int order)
    {
        if (_printerState == PrinterState.Available)
        {
            printMenuUI.SetActive(false);
            printerCountDownUI.SetActive(true);
            AudioManager.Instance.PlayClip(this.GetComponent<AudioSource>(), AudioManager.Instance.effectAudio[6].myControllers[2]);
            _printerState = PrinterState.Printing;
            printHaltButton.interactable = true;
            printResourceCollectionButton.interactable = false;
            printedAmount = 0;
            printerCollect_Text.text = "COLLECT " + "[" + printedAmount + "]";
            printerHalt_Text.text = "HALT";
            switch (order)
            {
                case 0:
                    Clock_PrintTime = ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourcePrintTime;
                    StartCoroutine(PrintOrder(ResourceDataList[order].GetComponent<Resource_Item>().ItemData, ResourceImageList[order]));
                    break;
                case 1:
                    Clock_PrintTime = ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourcePrintTime;
                    StartCoroutine(PrintOrder(ResourceDataList[order].GetComponent<Resource_Item>().ItemData, ResourceImageList[order]));
                    break;
                case 2:
                    Clock_PrintTime = ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourcePrintTime;
                    StartCoroutine(PrintOrder(ResourceDataList[order].GetComponent<Resource_Item>().ItemData, ResourceImageList[order]));
                    break;
                case 3:
                    Clock_PrintTime = ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourcePrintTime;
                    StartCoroutine(PrintOrder(ResourceDataList[order].GetComponent<Resource_Item>().ItemData, ResourceImageList[order]));
                    break;
                case 4:
                    Clock_PrintTime = ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourcePrintTime;
                    StartCoroutine(PrintOrder(ResourceDataList[order].GetComponent<Resource_Item>().ItemData, ResourceImageList[order]));
                    break;
                case 5:
                    Clock_PrintTime = ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourcePrintTime;
                    StartCoroutine(PrintOrder(ResourceDataList[order].GetComponent<Resource_Item>().ItemData, ResourceImageList[order]));
                    //_printingEffect_VFX.SetMesh("MaterializingMesh", ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourceMesh);
                    //_printingEffect_VFX.SetFloat("MeshLifeTime", ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourcePrintTime);
                    break;
            }
        }
    }

    public void HaltPrinting()
    {
        _printerState = PrinterState.Halting;
        printerHalt_Text.text = "HALTING...";
        printHaltButton.interactable = false;
    }

    public void CollectPrint()
    {
        GameObject tempResource;

        switch (printingResource)
        {
            case ResourceType.Oil:
                //WorkshopManager.Instance.WorkshopStorage.OilCount++;
                tempResource = Instantiate(ResourceDataList[0], _itemSpawnLocation.transform.position, _itemSpawnLocation.transform.rotation);
                tempResource.GetComponent<Resource_Item>().PickupTimerCount = 1;
                tempResource.GetComponent<Resource_Item>().ResourceAmount = printedAmount;
                break;
            case ResourceType.Advanced_Sensors:
                //WorkshopManager.Instance.WorkshopStorage.SensorCount++;
                tempResource = Instantiate(ResourceDataList[2], _itemSpawnLocation.transform.position, _itemSpawnLocation.transform.rotation);
                tempResource.GetComponent<Resource_Item>().PickupTimerCount = 1;
                tempResource.GetComponent<Resource_Item>().ResourceAmount = printedAmount;
                break;
            case ResourceType.MotherBoard:
                //WorkshopManager.Instance.WorkshopStorage.MotherBoardCount++;
                tempResource = Instantiate(ResourceDataList[1], _itemSpawnLocation.transform.position, _itemSpawnLocation.transform.rotation);
                tempResource.GetComponent<Resource_Item>().PickupTimerCount = 1;
                tempResource.GetComponent<Resource_Item>().ResourceAmount = printedAmount;
                break;
            case ResourceType.Black_Matter:
                //WorkshopManager.Instance.WorkshopStorage.BlackMatterCount++;
                tempResource = Instantiate(ResourceDataList[4], _itemSpawnLocation.transform.position, _itemSpawnLocation.transform.rotation);
                tempResource.GetComponent<Resource_Item>().PickupTimerCount = 1;
                tempResource.GetComponent<Resource_Item>().ResourceAmount = printedAmount;
                break;
            case ResourceType.Z_Crystal:
                //WorkshopManager.Instance.WorkshopStorage.ZCrystalCount++;
                tempResource = Instantiate(ResourceDataList[5], _itemSpawnLocation.transform.position, _itemSpawnLocation.transform.rotation);
                tempResource.GetComponent<Resource_Item>().PickupTimerCount = 1;
                tempResource.GetComponent<Resource_Item>().ResourceAmount = printedAmount;
                break;
            case ResourceType.Radioactive_Waste:
                //WorkshopManager.Instance.WorkshopStorage.RadioactiveWasteCount++;
                tempResource = Instantiate(ResourceDataList[3], _itemSpawnLocation.transform.position, _itemSpawnLocation.transform.rotation);
                tempResource.GetComponent<Resource_Item>().PickupTimerCount = 1;
                tempResource.GetComponent<Resource_Item>().ResourceAmount = printedAmount;
                break;
        }

        if (_printerState == PrinterState.Completed)
        {
            printMenuUI.SetActive(true);
            printerCountDownUI.SetActive(false);
            _printerState = PrinterState.Available;
        }

        else if (_printerState == PrinterState.PrinterFull)
        {
            _printerState = PrinterState.NextPrint;
        }

        printedAmount = 0;
        printerCollect_Text.text = "COLLECT " + "[" + printedAmount + "]";
        printResourceCollectionButton.interactable = false;
    }

    public IEnumerator PrintOrder(Resource_ItemData printResource, Sprite resourceImage)
    {
        if (_printerState != PrinterState.Halting)
        {
            while (_printerState != PrinterState.Halting)
            {
                if (_printerState == PrinterState.PrinterFull)
                {
                    printerTime_Text.text = "Printer: Full";
                    yield return new WaitUntil(() => _printerState != PrinterState.PrinterFull);
                }
                else
                {
                    _printerState = PrinterState.Printing;
                    printingResource = printResource.ResourceName;
                    AudioManager.Instance.PlayClip(this.GetComponent<AudioSource>(), AudioManager.Instance.FindClip(AudioType.Printer_Hum, AudioManager.Instance.effectAudio));
                    while (_printerState == PrinterState.Printing)
                    {
                        printerResourceImage.sprite = resourceImage;
                        if (Clock_PrintTime == 0)
                        {
                            Clock_PrintTime = printResource.ResourcePrintTime;
                            AudioManager.Instance.PlayClip(this.GetComponent<AudioSource>(), AudioManager.Instance.FindClip(AudioType.Printer_Ding, AudioManager.Instance.effectAudio));
                            printedAmount++;
                            printResourceCollectionButton.interactable = true;
                            printerCollect_Text.text = "COLLECT " + "[" + printedAmount + "]";
                            if (printedAmount == printedMaxAmount)
                            {
                                _printerState = PrinterState.PrinterFull;
                            }
                            else
                            {
                                _printerState = PrinterState.NextPrint;
                            }
                        }
                        else
                        {
                            Clock_PrintTime--;
                            if (Clock_PrintTime%7 == 0)
                            {
                                AudioManager.Instance.PlayClip(this.GetComponent<AudioSource>(), AudioManager.Instance.FindClip(AudioType.Printer_Hum, AudioManager.Instance.effectAudio));
                            }
                        }
                        yield return new WaitForSeconds(1);
                    }
                }
                yield return new WaitForSeconds(0.1f);
            }
        }

        while (_printerState == PrinterState.Halting)
        {
            printMenuUI.SetActive(true);
            printerCountDownUI.SetActive(false);
            _printerState = PrinterState.Available;
            printedAmount = 0;
            printerTime_Text.text = "Printer: Halted";
            printerCollect_Text.text = "COLLECT " + "[" + printedAmount + "]";

            AudioManager.Instance.PlayClip(this.GetComponent<AudioSource>(), AudioManager.Instance.FindClip(AudioType.Printer_Ding, AudioManager.Instance.effectAudio));
            yield return new WaitForSeconds(1);
        }
    }

    public void HandleInteract()
    {
        if (!isOn)
            originalWeaponIndex = GameManager.Instance.weaponController.WeaponIndex;

        isOn = !isOn;

        //Force Weapon switch to hands
        if (isOn)
        {
            GameManager.Instance.weaponController.SwitchWeapon(2);
            GameManager.Instance.InUI = !GameManager.Instance.InUI;
        }

        else if (!isOn)
        {
            GameManager.Instance.InUI = !GameManager.Instance.InUI;
            GameManager.Instance.weaponController.SwitchWeapon(originalWeaponIndex);
        }

        if (scaler == null)
        {
            scaler = GetComponentInChildren<BilboardScaler>();
        }

        if (isOn)
            handleUI = StartCoroutine(scaler.HandleUI());

        else if (handleUI != null)
            StopCoroutine(handleUI);

        if (printerID == 1)
        {
            EventBus.Publish(EventType.TOGGLE_PRINTER1_CAM_BLEND);
        }

        else if (printerID == 2)
        {
            EventBus.Publish(EventType.TOGGLE_PRINTER2_CAM_BLEND);
        }

        else if (printerID == 3)
        {
            EventBus.Publish(EventType.TOGGLE_PRINTER3_CAM_BLEND);
        }

        else if (printerID == 4)
        {
            EventBus.Publish(EventType.TOGGLE_PRINTER4_CAM_BLEND);
        }

        else if (printerID == 5)
        {
            EventBus.Publish(EventType.TOGGLE_PRINTER5_CAM_BLEND);
        }

        else if (printerID == 6)
        {
            EventBus.Publish(EventType.TOGGLE_PRINTER6_CAM_BLEND);
        }
    }

    enum PrinterState
    {
        Available,
        Printing,
        PrinterFull,
        NextPrint,
        Completed,
        Halting
    }

}