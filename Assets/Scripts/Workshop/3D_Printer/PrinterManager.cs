using Agyr.Workshop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrinterManager : MonoBehaviour
{
    private PrinterState printerState = PrinterState.Available;
    public int clock_PrintTime = 0;

    private List<Image> ResourceImageList;

    [SerializeField]
    private GameObject printMenuUI;
    [SerializeField]
    private GameObject printerUI;
    [SerializeField]
    private Image printerResourceImage;
    
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
                    StartCoroutine(PrintOrder(ResourceType.Z_Crystal, ResourceImageList[5]));
                    break;
                case 7:
                    StartCoroutine(PrintOrder(ResourceType.Radioactive_Waste, ResourceImageList[5]));
                    break;
            }
        }
    }

    public void CollectPrint(ResourceType printResource)
    {
        if (printerState == PrinterState.Completed)
        {
            
            switch (printResource)
            {
                case 0:
                    WorkshopManager.Instance.WorkshopStorage.RadioactiveWasteCount++;
                    break;
                case 1:
                    WorkshopManager.Instance.WorkshopStorage.RadioactiveWasteCount++;
                    break;
                case 2:
                    WorkshopManager.Instance.WorkshopStorage.RadioactiveWasteCount++;
                    break;
                case 3:
                    WorkshopManager.Instance.WorkshopStorage.RadioactiveWasteCount++;
                    break;
                case 4:
                    WorkshopManager.Instance.WorkshopStorage.RadioactiveWasteCount++;
                    break;
                case 5:
                    WorkshopManager.Instance.WorkshopStorage.RadioactiveWasteCount++;
                    break;
                case 6:
                    WorkshopManager.Instance.WorkshopStorage.RadioactiveWasteCount++;
                    break;
                case 7:
                    WorkshopManager.Instance.WorkshopStorage.RadioactiveWasteCount++;
                    break;
            }
        }
    }
    

    public IEnumerator PrintOrder(ResourceType printResource, Image resourceImage)
    {
        while (printerState == PrinterState.Printing)
        {
            printerResourceImage.sprite = resourceImage.sprite;
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
