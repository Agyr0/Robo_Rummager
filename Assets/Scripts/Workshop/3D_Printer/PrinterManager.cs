using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrinterManager : MonoBehaviour
{
    public bool isPrinting = false;
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
        if (isPrinting == false)
        {
            isPrinting = true;

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
                    StartCoroutine(PrintOrder(ResourceType.Z_Crystal));
                    break;
                case 7:
                    StartCoroutine(PrintOrder(ResourceType.Radioactive_Waste));
                    break;
            }
        }
    }
    

    public IEnumerator PrintOrder(ResourceType printResource, Image resourceImage)
    {
        
        clock_PrintTime = printTime;
        while (isPrinting == true)
        {
            printerResourceImage.sprite = resourceImage.sprite;
            if (clock_PrintTime == 0)
            {
                isPrinting = false;
            }
            clock_PrintTime--;
            yield return new WaitForSeconds(1);
        }
    }
    
}
