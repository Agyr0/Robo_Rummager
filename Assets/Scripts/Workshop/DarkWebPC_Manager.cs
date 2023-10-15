using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DarkWebPC_Manager : MonoBehaviour, IInteractable
{
    [SerializeField]
    private List<GameObject> _upgradesPlayer;
    [SerializeField]
    private List<GameObject> _upgradesWrench;
    [SerializeField]
    private List<GameObject> _upgradesScannerGoggles;
    [SerializeField]
    private List<GameObject> _upgradesLaserPistol;
    [SerializeField]
    private List<GameObject> _upgradesMachines;

    [SerializeField]
    private GameObject _loginMenu;
    [SerializeField]
    private GameObject _upgradeMenu;

    [SerializeField]
    private GameObject selectionCanvas;
    private BilboardScaler scaler;
    private int originalWeaponIndex;

    private Coroutine handleUI;

    private bool isOn = false;

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.TOGGLE_DARKPC_CAM_BLEND, ToggleDisplayUpgrades);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.TOGGLE_DARKPC_CAM_BLEND, ToggleDisplayUpgrades);
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

        EventBus.Publish(EventType.TOGGLE_DARKPC_CAM_BLEND);
    }

    public void ToggleDisplayUpgrades()
    {
        if (_upgradeMenu.activeSelf == true)
        {
            _upgradeMenu.SetActive(false);
            _loginMenu.SetActive(true);
        }
        else
        {
            _upgradeMenu.SetActive(true);
            _loginMenu.SetActive(false);
        }
    }

    public void DisplayAll()
    {
        foreach(GameObject item in _upgradesPlayer)
        {
            item.SetActive(true);
        }
        foreach (GameObject item in _upgradesWrench)
        {
            item.SetActive(true);
        }
        foreach (GameObject item in _upgradesScannerGoggles)
        {
            item.SetActive(true);
        }
        foreach (GameObject item in _upgradesLaserPistol)
        {
            item.SetActive(true);
        }
        foreach (GameObject item in _upgradesMachines)
        {
            item.SetActive(true);
        }
    }

    public void DisplayPlayer()
    {
        foreach (GameObject item in _upgradesPlayer)
        {
            item.SetActive(true);
        }
        foreach (GameObject item in _upgradesWrench)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in _upgradesScannerGoggles)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in _upgradesLaserPistol)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in _upgradesMachines)
        {
            item.SetActive(false);
        }
    }

    public void DisplayWrench()
    {
        foreach (GameObject item in _upgradesPlayer)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in _upgradesWrench)
        {
            item.SetActive(true);
        }
        foreach (GameObject item in _upgradesScannerGoggles)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in _upgradesLaserPistol)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in _upgradesMachines)
        {
            item.SetActive(false);
        }
    }

    public void DisplayScannerGoggles()
    {
        foreach (GameObject item in _upgradesPlayer)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in _upgradesWrench)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in _upgradesScannerGoggles)
        {
            item.SetActive(true);
        }
        foreach (GameObject item in _upgradesLaserPistol)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in _upgradesMachines)
        {
            item.SetActive(false);
        }
    }

    public void DisplayLaser()
    {
        foreach (GameObject item in _upgradesPlayer)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in _upgradesWrench)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in _upgradesScannerGoggles)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in _upgradesLaserPistol)
        {
            item.SetActive(true);
        }
        foreach (GameObject item in _upgradesMachines)
        {
            item.SetActive(false);
        }
    }

    public void DisplayMachines()
    {
        foreach (GameObject item in _upgradesPlayer)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in _upgradesWrench)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in _upgradesScannerGoggles)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in _upgradesLaserPistol)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in _upgradesMachines)
        {
            item.SetActive(true);
        }
    }
}
