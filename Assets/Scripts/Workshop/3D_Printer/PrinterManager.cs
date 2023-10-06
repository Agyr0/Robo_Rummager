using Agyr.Workshop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrinterManager : MonoBehaviour
{
    private PrinterState printerState = PrinterState.Available;
    public int clock_PrintTime = 0;

    public List<Sprite> ResourceImageList;

    [SerializeField]
    private GameObject printMenuUI;
    [SerializeField]
    private GameObject printerTimerUI;
    [SerializeField]
    private GameObject printerCompleteUI;
    [SerializeField]
    private Image printerResourceImage;

    private ResourceType printingResource;


    public void StartPrintOrder(int order)
    {
        if (printerState == PrinterState.Available)
        {
            printerState = PrinterState.Printing;
            clock_PrintTime = 10;
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
        if (printerState == PrinterState.Completed)
        {
            switch (printingResource)
            {
                case ResourceType.Metal_Scrap:
                    WorkshopManager.Instance.WorkshopStorage.MetalScrapCount++;
                    break;
                case ResourceType.Oil:
                    WorkshopManager.Instance.WorkshopStorage.OilCount++;
                    break;
                case ResourceType.Advanced_Sensors:
                    WorkshopManager.Instance.WorkshopStorage.SensorCount++;
                    break;
                case ResourceType.Wire:
                    WorkshopManager.Instance.WorkshopStorage.WireCount++;
                    break;
                case ResourceType.MotherBoard:
                    WorkshopManager.Instance.WorkshopStorage.MotherBoardCount++;
                    break;
                case ResourceType.Black_Matter:
                    WorkshopManager.Instance.WorkshopStorage.BlackMatterCount++;
                    break;
                case ResourceType.Z_Crystal:
                    WorkshopManager.Instance.WorkshopStorage.ZCrystalCount++;
                    break;
                case ResourceType.Radioactive_Waste:
                    WorkshopManager.Instance.WorkshopStorage.RadioactiveWasteCount++;
                    break;
            }
        }
    }
    

    public IEnumerator PrintOrder(ResourceType printResource, Sprite resourceImage)
    {
        printingResource = printResource;
        while (printerState == PrinterState.Printing)
        {
            printerResourceImage.sprite = resourceImage;
            if (clock_PrintTime == 0)
            {
                printerState = PrinterState.Completed;
            }
            clock_PrintTime--;
            yield return new WaitForSeconds(1);
        }
    }

    enum PrinterState
    {
        Available,
        Printing,
        Completed
    }
    
}
