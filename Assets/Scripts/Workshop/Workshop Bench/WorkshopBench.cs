using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

namespace Agyr.Workshop
{
    [System.Serializable]
    public class WorkshopBench : Singleton<WorkshopBench>, IInteractable
    {
        [SerializeField]
        private GameObject selectionCanvas;
        private BilboardScaler scaler;
        
        
        private Coroutine handleUI;

        private bool isOn = false;


        public void HandleInteract()
        {
            isOn = !isOn;
            GameManager.Instance.InUI = !GameManager.Instance.InUI;
            if(scaler == null)
            {
                scaler = GetComponentInChildren<BilboardScaler>();
            }

            if (isOn)
                handleUI = StartCoroutine(scaler.HandleUI());
            else if(handleUI != null)
                StopCoroutine(handleUI);

            EventBus.Publish(EventType.TOGGLE_WORKBENCH_CAM_BLEND);
            
            //selectionCanvas.SetActive(!selectionCanvas.activeInHierarchy);


        }
    }
}