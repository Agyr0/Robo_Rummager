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
        private int originalWeaponIndex;
        
        private Coroutine handleUI;

        [HideInInspector]
        public TabManager tabManager;

        private bool isOn = false;

        private void Start()
        {
            tabManager = GetComponentInChildren<TabManager>();
        }

        public void HandleInteract()
        {
            if (!isOn)
                originalWeaponIndex = GameManager.Instance.weaponController.WeaponIndex;
            isOn = !isOn;

            
            //Force Weapon switch to hands
            if (isOn)
            {
                tabManager.FindActiveTab().CheckResourceCount(WorkshopManager.Instance.WorkshopStorage);
                GameManager.Instance.weaponController.SwitchWeapon(2);
                GameManager.Instance.InUI = !GameManager.Instance.InUI;
            }
            else if (!isOn)
            {
                GameManager.Instance.InUI = !GameManager.Instance.InUI;
                GameManager.Instance.weaponController.SwitchWeapon(originalWeaponIndex);
            }

            if(scaler == null)
            {
                scaler = GetComponentInChildren<BilboardScaler>();
            }



            if (isOn)
                handleUI = StartCoroutine(scaler.HandleUI());
            else if(handleUI != null)
                StopCoroutine(handleUI);

            EventBus.Publish(EventType.TOGGLE_WORKBENCH_CAM_BLEND);
        }
    }
}