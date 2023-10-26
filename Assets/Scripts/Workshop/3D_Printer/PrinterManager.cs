using Agyr.Workshop;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.VFX;
using Image = UnityEngine.UI.Image;

public class PrinterManager : MonoBehaviour, IInteractable
{
    [SerializeField]
    private PrinterState _printerState = PrinterState.Available;

    public int clock_PrintTime = 0;

    public List<Sprite> ResourceImageList;

    public List<GameObject> ResourceDataList;

    [SerializeField]
    private VisualEffect _printingEffect_VFX;

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

    [SerializeField]
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
            string mintutes = ((float)clock_PrintTime / 60).ToString().Split('.')[0];
            string seconds = (clock_PrintTime % 60).ToString();

            if (seconds.Length == 2)
            {
                printerTime_Text.text = mintutes + ':' + seconds;
            }
            else
            {
                printerTime_Text.text = mintutes + ":0" + seconds;
            }
            
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
            switch (order)
            {
                case 0:
                    Clock_PrintTime = ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourcePrintTime;
                    StartCoroutine(PrintOrder(ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourceName, ResourceImageList[order]));
                    _printingEffect_VFX.SetMesh("MaterializingMesh", ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourceMesh);
                    _printingEffect_VFX.SetFloat("MeshLifeTime", ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourcePrintTime);
                    
                    break;
                case 1:
                    Clock_PrintTime = ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourcePrintTime;
                    StartCoroutine(PrintOrder(ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourceName, ResourceImageList[order]));
                    _printingEffect_VFX.SetMesh("MaterializingMesh", ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourceMesh);
                    _printingEffect_VFX.SetFloat("MeshLifeTime", ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourcePrintTime);
                    break;
                case 2:
                    Clock_PrintTime = ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourcePrintTime;
                    StartCoroutine(PrintOrder(ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourceName, ResourceImageList[order]));
                    _printingEffect_VFX.SetMesh("MaterializingMesh", ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourceMesh);
                    _printingEffect_VFX.SetFloat("MeshLifeTime", ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourcePrintTime);
                    break;
                case 3:
                    Clock_PrintTime = ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourcePrintTime;
                    StartCoroutine(PrintOrder(ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourceName, ResourceImageList[order]));
                    _printingEffect_VFX.SetMesh("MaterializingMesh", ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourceMesh);
                    _printingEffect_VFX.SetFloat("MeshLifeTime", ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourcePrintTime);
                    break;
                case 4:
                    Clock_PrintTime = ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourcePrintTime;
                    StartCoroutine(PrintOrder(ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourceName, ResourceImageList[order]));
                    _printingEffect_VFX.SetMesh("MaterializingMesh", ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourceMesh);
                    _printingEffect_VFX.SetFloat("MeshLifeTime", ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourcePrintTime);
                    break;
                case 5:
                    Clock_PrintTime = ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourcePrintTime;
                    StartCoroutine(PrintOrder(ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourceName, ResourceImageList[order]));
                    _printingEffect_VFX.SetMesh("MaterializingMesh", ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourceMesh);
                    _printingEffect_VFX.SetFloat("MeshLifeTime", ResourceDataList[order].GetComponent<Resource_Item>().ItemData.ResourcePrintTime);
                    break;
            }
        }
    }

    public void CollectPrint()
    {
        if (_printerState == PrinterState.Completed)
        {
            GameObject tempResource;
            
            switch (printingResource)
            {
                case ResourceType.Oil:
                    //WorkshopManager.Instance.WorkshopStorage.OilCount++;
                    tempResource = Instantiate(ResourceDataList[0], _itemSpawnLocation.transform.position, _itemSpawnLocation.transform.rotation);
                    tempResource.GetComponent<Resource_Item>().PickupTimerCount = 2;
                    tempResource.GetComponent<Resource_Item>().ResourceAmount = 1;
                    break;
                case ResourceType.Advanced_Sensors:
                    //WorkshopManager.Instance.WorkshopStorage.SensorCount++;
                    tempResource = Instantiate(ResourceDataList[2], _itemSpawnLocation.transform.position, _itemSpawnLocation.transform.rotation);
                    tempResource.GetComponent<Resource_Item>().PickupTimerCount = 2;
                    tempResource.GetComponent<Resource_Item>().ResourceAmount = 1;
                    break;
                case ResourceType.MotherBoard:
                    //WorkshopManager.Instance.WorkshopStorage.MotherBoardCount++;
                    tempResource = Instantiate(ResourceDataList[1], _itemSpawnLocation.transform.position, _itemSpawnLocation.transform.rotation);
                    tempResource.GetComponent<Resource_Item>().PickupTimerCount = 2;
                    tempResource.GetComponent<Resource_Item>().ResourceAmount = 1;
                    break;
                case ResourceType.Black_Matter:
                    //WorkshopManager.Instance.WorkshopStorage.BlackMatterCount++;
                    tempResource = Instantiate(ResourceDataList[4], _itemSpawnLocation.transform.position, _itemSpawnLocation.transform.rotation);
                    tempResource.GetComponent<Resource_Item>().PickupTimerCount = 2;
                    tempResource.GetComponent<Resource_Item>().ResourceAmount = 1;
                    break;
                case ResourceType.Z_Crystal:
                    //WorkshopManager.Instance.WorkshopStorage.ZCrystalCount++;
                    tempResource = Instantiate(ResourceDataList[5], _itemSpawnLocation.transform.position, _itemSpawnLocation.transform.rotation);
                    tempResource.GetComponent<Resource_Item>().PickupTimerCount = 2;
                    tempResource.GetComponent<Resource_Item>().ResourceAmount = 1;
                    break;
                case ResourceType.Radioactive_Waste:
                    //WorkshopManager.Instance.WorkshopStorage.RadioactiveWasteCount++;
                    tempResource = Instantiate(ResourceDataList[3], _itemSpawnLocation.transform.position, _itemSpawnLocation.transform.rotation);
                    tempResource.GetComponent<Resource_Item>().PickupTimerCount = 2;
                    tempResource.GetComponent<Resource_Item>().ResourceAmount = 1;
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
        _printingEffect_VFX.Play();
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

        EventBus.Publish(EventType.TOGGLE_PRINTER_CAM_BLEND);
    }

    enum PrinterState
    {
        Available,
        Printing,
        Completed
    }
    
}
