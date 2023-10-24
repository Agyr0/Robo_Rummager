using Agyr.Workshop;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class DarkWebPC_Manager : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int _upgradeCountCurrentDamage;
    [SerializeField]
    private int _upgradeCountMaxDamage;
    [SerializeField]
    private int _upgradeCostDamage;
    [SerializeField]
    private TextMeshProUGUI _textDamageDesc;
    [SerializeField]
    private TextMeshProUGUI _textDamageCost;
    [SerializeField]
    private TextMeshProUGUI _textDamageLevel;
    [SerializeField]
    private Button _buttonDamage;
    [SerializeField]
    private GameObject _textDamageSold;
    [SerializeField] 
    private GameObject _textDamagePurchase;

    [SerializeField]
    private int _upgradeCountCurrentInventorySlot;
    [SerializeField]
    private int _upgradeCountMaxInventorySlot;
    [SerializeField]
    private int _upgradeCostInventorySlot;
    [SerializeField]
    private TextMeshProUGUI _textInventorySlotDesc;
    [SerializeField]
    private TextMeshProUGUI _textInventorySlotCost;
    [SerializeField]
    private TextMeshProUGUI _textInventorySlotLevel;
    [SerializeField]
    private Button _buttonInventorySlot;
    [SerializeField]
    private GameObject _textInventorySlotSold;
    [SerializeField]
    private GameObject _textInventorySlotPurchase;

    [SerializeField]
    private int _upgradeCountCurrentStackSize;
    [SerializeField]
    private int _upgradeCountMaxStackSize;
    [SerializeField]
    private int _upgradeCostStackSize;
    [SerializeField]
    private TextMeshProUGUI _textStackSizeDesc;
    [SerializeField]
    private TextMeshProUGUI _textStackSizeCost;
    [SerializeField]
    private TextMeshProUGUI _textStackSizeLevel;
    [SerializeField]
    private Button _buttonStackSize;
    [SerializeField]
    private GameObject _textStackSizeSold;
    [SerializeField]
    private GameObject _textStackSizePurchase;


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
    private WeaponData _wrench;

    [SerializeField]
    private GameObject _allTab;
    [SerializeField]
    private GameObject _playerTab;
    [SerializeField]
    private GameObject _wrenchTab;
    [SerializeField]
    private GameObject _gunTab;
    [SerializeField]
    private GameObject _gogglesTab;
    [SerializeField]
    private GameObject _machineTab;

    [SerializeField]
    private GameObject selectionCanvas;
    private BilboardScaler scaler;
    private int originalWeaponIndex;

    private Coroutine handleUI;

    private bool isOn = false;

    private void OnEnable()
    {
        //EventBus.Subscribe(EventType.TOGGLE_DARKPC_CAM_BLEND, ToggleDisplayUpgrades);

        UpdateUpgradeDamage();
        UpdateUpgradeInventorySlot();
        UpdateUpgradeStackSize();
    }

    private void OnDisable()
    {
        //EventBus.Unsubscribe(EventType.TOGGLE_DARKPC_CAM_BLEND, ToggleDisplayUpgrades);
    }

    private void Start()
    {
        WorkshopManager.Instance.WorkshopStorage.CreditCount += 300;
    }
    
    private void UpdateUpgradeDamage()
    {
        _textDamageCost.text = _upgradeCostDamage.ToString();
        _textDamageDesc.text = "+10 to Wrench Swing Damage";
        _textDamageLevel.text = "Level " + _upgradeCountCurrentDamage + " of " + _upgradeCountMaxDamage;
    }
    private void UpdateUpgradeInventorySlot()
    {
        _textInventorySlotCost.text = _upgradeCostInventorySlot.ToString();
        _textInventorySlotDesc.text = "+1 Inventory Slot";
        _textInventorySlotLevel.text = "Level " + _upgradeCountCurrentInventorySlot + " of " + _upgradeCountMaxInventorySlot;
    }
    private void UpdateUpgradeStackSize()
    {
        _textStackSizeCost.text = _upgradeCostStackSize.ToString();
        _textStackSizeDesc.text = "+5 to Stack Size";
        _textStackSizeLevel.text = "Level " + _upgradeCountCurrentStackSize + " of " + _upgradeCountMaxStackSize;
    }

    public void UpgradeDamage()
    {
        if (_upgradeCountCurrentDamage < _upgradeCountMaxDamage)
        {
            if (WorkshopManager.Instance.WorkshopStorage.CreditCount >= _upgradeCostDamage)
            {
                WorkshopManager.Instance.WorkshopStorage.CreditCount -= _upgradeCostDamage;
                _wrench.Damage += 10;
               _upgradeCountCurrentDamage++;
                _textDamageCost.text = _upgradeCostDamage.ToString();
                _textDamageDesc.text = "+10 to Wrench Swing Damage";
                _textDamageLevel.text = "Level " + _upgradeCountCurrentDamage + " of " + _upgradeCountMaxDamage;
            }

            if (_upgradeCountCurrentDamage == _upgradeCountMaxDamage)
            {
                _buttonDamage.interactable = false;
                _textDamageSold.SetActive(true);
            }
        }
    }

    public void UpgradeInventorySlot()
    {
        if (_upgradeCountCurrentInventorySlot < _upgradeCountMaxInventorySlot)
        {
            if (WorkshopManager.Instance.WorkshopStorage.CreditCount >= _upgradeCostInventorySlot)
            {
                WorkshopManager.Instance.WorkshopStorage.CreditCount -= _upgradeCostInventorySlot;
                _upgradeCountCurrentInventorySlot++;
                EventBus.Publish(EventType.INVENTORY_ADDSLOT);
                _textInventorySlotCost.text = _upgradeCostInventorySlot.ToString();
                _textInventorySlotDesc.text = "+1 Inventory Slot";
                _textInventorySlotLevel.text = "Level " + _upgradeCountCurrentInventorySlot + " of " + _upgradeCountMaxInventorySlot;
            }

            if (_upgradeCountCurrentInventorySlot == _upgradeCountMaxInventorySlot)
            {
                _buttonInventorySlot.interactable = false;
                _textInventorySlotSold.SetActive(true);
            }
        }
    }

    public void UpgradeStackSize()
    {
        if (_upgradeCountCurrentStackSize < _upgradeCountMaxStackSize)
        {
            if (WorkshopManager.Instance.WorkshopStorage.CreditCount >= _upgradeCostStackSize)
            {
                WorkshopManager.Instance.WorkshopStorage.CreditCount -= _upgradeCostStackSize;
                _upgradeCountCurrentStackSize++;
                EventBus.Publish(EventType.UPGRADE_STACKSIZE, 5);
                _textStackSizeCost.text = _upgradeCostStackSize.ToString();
                _textStackSizeDesc.text = "+5 to Stack Size";
                _textStackSizeLevel.text = "Level " + _upgradeCountCurrentStackSize + " of " + _upgradeCountMaxStackSize;
            }

            if (_upgradeCountCurrentStackSize == _upgradeCountMaxStackSize)
            {
                _buttonStackSize.interactable = false;
                _textStackSizeSold.SetActive(true);
            }
        }
    }

    public void LeavePC()
    {
        HandleInteract();
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
        _allTab.SetActive(true);
        _playerTab.SetActive(false);
        _wrenchTab.SetActive(false);
        _gunTab.SetActive(false);
        _gogglesTab.SetActive(false);
        _machineTab.SetActive(false);

        foreach (GameObject item in _upgradesPlayer)
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

        _allTab.SetActive(false);
        _playerTab.SetActive(true);
        _wrenchTab.SetActive(false);
        _gunTab.SetActive(false);
        _gogglesTab.SetActive(false);
        _machineTab.SetActive(false);

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

        _allTab.SetActive(false);
        _playerTab.SetActive(false);
        _wrenchTab.SetActive(true);
        _gunTab.SetActive(false);
        _gogglesTab.SetActive(false);
        _machineTab.SetActive(false);

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

        _allTab.SetActive(false);
        _playerTab.SetActive(false);
        _wrenchTab.SetActive(false);
        _gunTab.SetActive(false);
        _gogglesTab.SetActive(true);
        _machineTab.SetActive(false);

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

        _allTab.SetActive(false);
        _playerTab.SetActive(false);
        _wrenchTab.SetActive(false);
        _gunTab.SetActive(true);
        _gogglesTab.SetActive(false);
        _machineTab.SetActive(false);

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

        _allTab.SetActive(false);
        _playerTab.SetActive(false);
        _wrenchTab.SetActive(false);
        _gunTab.SetActive(false);
        _gogglesTab.SetActive(false);
        _machineTab.SetActive(true);

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

    public enum Upgrade
    {
        InventorySlot,
        ResourceStackSize,

    }
}
