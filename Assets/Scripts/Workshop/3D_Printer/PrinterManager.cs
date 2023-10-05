using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterManager : MonoBehaviour
{
    public bool isPrinting = false;
    public int clock_PrintTime = 0;

    [SerializeField]
    private GameObject printMenuUI;
    [SerializeField]
    private GameObject printerUI;

    public void StartPrintOrder(int order)
    {
        if (isPrinting == false)
        {
            switch (order)
            {
                case 0:

                    break;
            }
        }
    }
    /*
    IEnumerator PrintOrder(ResourceType resourcePrinting)
    {

    }
    */
}
