using Agyr.Workshop;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class PrinterManager : MonoBehaviour, IInteractable
{
    [SerializeField]
    private PrinterState _printerState = PrinterState.Available;

    public int clock_PrintTime = 0;

    public List<Sprite> ResourceImageList;

    public List<Resource_ItemData> ResourceDataList;

    [SerializeField]
    private GameObject _itemPrefab;
    [SerializeField]
    private GameObject _itemSpawnLocation;

    [SerializeField]
    private GameObject printMenuUI;
    [SerializeField]
    private GameObject printerTimerUI;
    [SerializeField]
    private GameObject printerTimerTextUI;
    [SerializeField]
    private GameObject printerCompleteUI;
    [SerializeField]
    private Image printerResourceImage;
    [SerializeField]
    private TextMeshProUGUI printerTime_Text;

    [SerializeField]
    private GameObject selectionCanvas;
    private BilboardScaler scaler;
    private int originalWeaponIndex;

    private ResourceType printingResource;

    private Coroutine handleUI;

    private bool isOn = false;

    public int Clock_PrintTime
    {
        get 
        { 
            return clock_PrintTime; 
        }
        set 
        {
            clock_PrintTime = value;
            printerTime_Text.text = clock_PrintTime.ToString();
        }
    }

    public void StartPrintOrder(int order)
    {
        if (_printerState == PrinterState.Available)
        {
            printMenuUI.SetActive(false);
            printerTimerUI.SetActive(true);
            printerTimerTextUI.SetActive(true);
            printerCompleteUI.SetActive(false);

            _printerState = PrinterState.Printing;
            Clock_PrintTime = 10;
            switch (order)
            {
                case 0:
                    StartCoroutine(PrintOrder(ResourceType.Metal_Scrap, ResourceImageList[0]));
                    break;
                case 1:
                    StartCoroutine(PrintOrder(ResourceType.Oil, ResourceImageList[1]));
                    break;
                case 2:
                    StartCoroutine(PrintOrder(ResourceType.Advanced_Sensors, ResourceImageList[2]));
                    break;
                case 3:
                    StartCoroutine(PrintOrder(ResourceType.Wire, ResourceImageList[3]));
                    break;
                case 4:
                    StartCoroutine(PrintOrder(ResourceType.MotherBoard, ResourceImageList[4]));
                    break;
                case 5:
                    StartCoroutine(PrintOrder(ResourceType.Black_Matter, ResourceImageList[5]));
                    break;
                case 6:
                    StartCoroutine(PrintOrder(ResourceType.Z_Crystal, ResourceImageList[6]));
                    break;
                case 7:
                    StartCoroutine(PrintOrder(ResourceType.Radioactive_Waste, ResourceImageList[7]));
                    break;
            }
        }
    }

    public void CollectPrint()
    {
        if (_printerState == PrinterState.Completed)
        {
            GameObject tempResource = Instantiate(_itemPrefab, _itemSpawnLocation.transform.position, _itemSpawnLocation.transform.rotation);
            
            switch (printingResource)
            {
                case ResourceType.Metal_Scrap:
                    //WorkshopManager.Instance.WorkshopStorage.MetalScrapCount++;
                    tempResource.GetComponent<Resource_Item>().ItemData = ResourceDataList[0];
                    tempResource.GetComponent<Resource_Item>().PickupTimerCount = 2;
                    tempResource.GetComponent<Resource_Item>().ResourceAmount = 1;
                    tempResource.GetComponent<MeshRenderer>().material = ResourceDataList[0].ResourceMaterial;
                    tempResource.GetComponent<MeshFilter>().mesh = ResourceDataList[0].ResourceMesh;
                    break;
                case ResourceType.Oil:
                    //WorkshopManager.Instance.WorkshopStorage.OilCount++;
                    tempResource.GetComponent<Resource_Item>().ItemData = ResourceDataList[1];
                    tempResource.GetComponent<Resource_Item>().PickupTimerCount = 2;
                    tempResource.GetComponent<Resource_Item>().ResourceAmount = 1;
                    tempResource.GetComponent<MeshRenderer>().material = ResourceDataList[1].ResourceMaterial;
                    tempResource.GetComponent<MeshFilter>().mesh = ResourceDataList[1].ResourceMesh;
                    break;
                case ResourceType.Advanced_Sensors:
                    //WorkshopManager.Instance.WorkshopStorage.SensorCount++;
                    tempResource.GetComponent<Resource_Item>().ItemData = ResourceDataList[2];
                    tempResource.GetComponent<Resource_Item>().PickupTimerCount = 2;
                    tempResource.GetComponent<Resource_Item>().ResourceAmount = 1;
                    tempResource.GetComponent<MeshRenderer>().material = ResourceDataList[2].ResourceMaterial;
                    tempResource.GetComponent<MeshFilter>().mesh = ResourceDataList[2].ResourceMesh;
                    break;
                case ResourceType.Wire:
                    //WorkshopManager.Instance.WorkshopStorage.WireCount++;
                    tempResource.GetComponent<Resource_Item>().ItemData = ResourceDataList[3];
                    tempResource.GetComponent<Resource_Item>().PickupTimerCount = 2;
                    tempResource.GetComponent<Resource_Item>().ResourceAmount = 1;
                    tempResource.GetComponent<MeshRenderer>().material = ResourceDataList[3].ResourceMaterial;
                    tempResource.GetComponent<MeshFilter>().mesh = ResourceDataList[3].ResourceMesh;
                    break;
                case ResourceType.MotherBoard:
                    //WorkshopManager.Instance.WorkshopStorage.MotherBoardCount++;
                    tempResource.GetComponent<Resource_Item>().ItemData = ResourceDataList[4];
                    tempResource.GetComponent<Resource_Item>().PickupTimerCount = 2;
                    tempResource.GetComponent<Resource_Item>().ResourceAmount = 1;
                    tempResource.GetComponent<MeshRenderer>().material = ResourceDataList[4].ResourceMaterial;
                    tempResource.GetComponent<MeshFilter>().mesh = ResourceDataList[4].ResourceMesh;
                    break;
                case ResourceType.Black_Matter:
                    //WorkshopManager.Instance.WorkshopStorage.BlackMatterCount++;
                    tempResource.GetComponent<Resource_Item>().ItemData = ResourceDataList[5];
                    tempResource.GetComponent<Resource_Item>().PickupTimerCount = 2;
                    tempResource.GetComponent<Resource_Item>().ResourceAmount = 1;
                    tempResource.GetComponent<MeshRenderer>().material = ResourceDataList[5].ResourceMaterial;
                    tempResource.GetComponent<MeshFilter>().mesh = ResourceDataList[5].ResourceMesh;
                    break;
                case ResourceType.Z_Crystal:
                    //WorkshopManager.Instance.WorkshopStorage.ZCrystalCount++;
                    tempResource.GetComponent<Resource_Item>().ItemData = ResourceDataList[6];
                    tempResource.GetComponent<Resource_Item>().PickupTimerCount = 2;
                    tempResource.GetComponent<Resource_Item>().ResourceAmount = 1;
                    tempResource.GetComponent<MeshRenderer>().material = ResourceDataList[6].ResourceMaterial;
                    tempResource.GetComponent<MeshFilter>().mesh = ResourceDataList[6].ResourceMesh;
                    break;
                case ResourceType.Radioactive_Waste:
                    //WorkshopManager.Instance.WorkshopStorage.RadioactiveWasteCount++;
                    tempResource.GetComponent<Resource_Item>().ItemData = ResourceDataList[7];
                    tempResource.GetComponent<Resource_Item>().PickupTimerCount = 2;
                    tempResource.GetComponent<Resource_Item>().ResourceAmount = 1;
                    tempResource.GetComponent<MeshRenderer>().material = ResourceDataList[7].ResourceMaterial;
                    tempResource.GetComponent<MeshFilter>().mesh = ResourceDataList[7].ResourceMesh;
                    break;
            }
     
            printMenuUI.SetActive(true);
            printerTimerUI.SetActive(false);
            printerTimerTextUI.SetActive(false);
            printerCompleteUI.SetActive(false);

            _printerState = PrinterState.Available;
        }
    }
    

    public IEnumerator PrintOrder(ResourceType printResource, Sprite resourceImage)
    {
        printingResource = printResource;
        while (_printerState == PrinterState.Printing)
        {
            printerResourceImage.sprite = resourceImage;
            if (Clock_PrintTime == 0)
            {
                printMenuUI.SetActive(false);
                printerTimerUI.SetActive(true);
                printerTimerTextUI.SetActive(false);
                printerCompleteUI.SetActive(true);

                _printerState = PrinterState.Completed;
            }
            Clock_PrintTime--;
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

        EventBus.Publish(EventType.TOGGLE_WORKBENCH_CAM_BLEND);
    }

    enum PrinterState
    {
        Available,
        Printing,
        Completed
    }
    
}
