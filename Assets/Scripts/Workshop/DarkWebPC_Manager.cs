using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DarkWebPC_Manager : MonoBehaviour, IInteractable
{

    [SerializeField]
    private GameObject selectionCanvas;
    private BilboardScaler scaler;
    private int originalWeaponIndex;

    private Coroutine handleUI;

    private bool isOn = false;

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

        EventBus.Publish(EventType.TOGGLE_DARKPC_CAM_BLEND);
    }
}
