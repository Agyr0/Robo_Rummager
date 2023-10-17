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
    private int _upgradeCountCurrentHealth;
    [SerializeField]
    private int _upgradeCountMaxHealth;
    [SerializeField]
    private int _upgradeCostHealth;
    [SerializeField]
    private TextMeshProUGUI _textHealthDesc;
    [SerializeField]
    private TextMeshProUGUI _textHealthCost;
    [SerializeField]
    private TextMeshProUGUI _textHealthLevel;
    [SerializeField]
    private TextMeshProUGUI _textHealthButton;
    [SerializeField]
    private Button _buttonHealth;
    [SerializeField]
    private GameObject _textHealthSold;

    [SerializeField]
    private int _upgradeCountCurrentStamina;
    [SerializeField]
    private int _upgradeCountMaxStamina;
    [SerializeField]
    private int _upgradeCostStamina;
    [SerializeField]
    private TextMeshProUGUI _textStaminaDesc;
    [SerializeField]
    private TextMeshProUGUI _textStaminaCost;
    [SerializeField]
    private TextMeshProUGUI _textStaminaLevel;
    [SerializeField]
    private TextMeshProUGUI _textStaminaButton;
    [SerializeField]
    private Button _buttonStamina;
    [SerializeField]
    private GameObject _textStaminaSold;

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
    private TextMeshProUGUI _textDamageButton;
    [SerializeField]
    private Button _buttonDamage;
    [SerializeField]
    private GameObject _textDamageSold;

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
    private TextMeshProUGUI _textInventorySlotButton;
    [SerializeField]
    private Button _buttonInventorySlot;
    [SerializeField]
    private GameObject _textInventorySlotSold;

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
    private TextMeshProUGUI _textStackSizeButton;
    [SerializeField]
    private Button _buttonStackSize;
    [SerializeField]
    private GameObject _textStackSizeSold;


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
    private GameObject selectionCanvas;
    private BilboardScaler scaler;
    private int originalWeaponIndex;

    private Coroutine handleUI;

    private bool isOn = false;

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.TOGGLE_DARKPC_CAM_BLEND, ToggleDisplayUpgrades);

        UpdateUpgradeHealth();
        UpdateUpgradeStamina();
        UpdateUpgradeDamage();
        UpdateUpgradeInventorySlot();
        UpdateUpgradeStackSize();

        WorkshopManager.Instance.WorkshopStorage.CreditCount = 200;
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.TOGGLE_DARKPC_CAM_BLEND, ToggleDisplayUpgrades);
    }

    private void UpdateUpgradeHealth()
    {
        _textHealthCost.text = "Cost: " + _upgradeCostHealth;
        _textHealthDesc.text = "+25 to Max Health";
        _textHealthLevel.text = "Level " + _upgradeCountCurrentHealth + " of " + _upgradeCountMaxHealth;
    }
    private void UpdateUpgradeStamina()
    {
        _textStaminaCost.text = "Cost: " + _upgradeCostStamina;
        _textStaminaDesc.text = "+25 to Max Stamina";
        _textStaminaLevel.text = "Level " + _upgradeCountCurrentStamina + " of " + _upgradeCountMaxStamina;
    }
    private void UpdateUpgradeDamage()
    {
        _textDamageCost.text = "Cost: " + _upgradeCostDamage;
        _textDamageDesc.text = "+10 to Wrench Swing Damage";
        _textDamageLevel.text = "Level " + _upgradeCountCurrentDamage + " of " + _upgradeCountMaxDamage;
    }
    private void UpdateUpgradeInventorySlot()
    {
        _textInventorySlotCost.text = "Cost: " + _upgradeCostInventorySlot;
        _textInventorySlotDesc.text = "+1 Inventory Slot";
        _textInventorySlotLevel.text = "Level " + _upgradeCountCurrentInventorySlot + " of " + _upgradeCountMaxInventorySlot;
    }
    private void UpdateUpgradeStackSize()
    {
        _textStackSizeCost.text = "Cost: " + _upgradeCostStackSize;
        _textStackSizeDesc.text = "Upgrade Stackable Resource Size";
        _textStackSizeLevel.text = "Level " + _upgradeCountCurrentStackSize + " of " + _upgradeCountMaxStackSize;
    }

    public void UpgradeHealth()
    {
        if (_upgradeCountCurrentHealth < _upgradeCountMaxHealth)
        {
            if (WorkshopManager.Instance.WorkshopStorage.CreditCount >= _upgradeCostHealth)
            {
                WorkshopManager.Instance.WorkshopStorage.CreditCount -= _upgradeCostHealth;
                _upgradeCountCurrentHealth++;
                EventBus.Publish(EventType.UPGRADE_HEALTH, 5f);
                _textHealthCost.text = "Cost: " + _upgradeCostHealth;
                _textHealthDesc.text = "+25 to Max Health";
                _textHealthLevel.text = "Level " + _upgradeCountCurrentHealth + " of " + _upgradeCountMaxHealth;
            }

            if (_upgradeCountCurrentHealth == _upgradeCountMaxHealth)
            {
                _buttonHealth.interactable = false;
                _textHealthSold.SetActive(true);
            }
        }
    }
    public void UpgradeStamina()
    {
        if (_upgradeCountCurrentStamina < _upgradeCountMaxStamina)
        {
            if (WorkshopManager.Instance.WorkshopStorage.CreditCount >= _upgradeCostStamina)
            {
                WorkshopManager.Instance.WorkshopStorage.CreditCount -= _upgradeCostStamina;
                _upgradeCountCurrentStamina++;
                EventBus.Publish(EventType.UPGRADE_STAMINA, 5f);
                _textStaminaCost.text = "Cost: " + _upgradeCostStamina;
                _textStaminaDesc.text = "+25 to Max Stamina";
                _textStaminaLevel.text = "Level " + _upgradeCountCurrentStamina + " of " + _upgradeCountMaxStamina;
            }

            if (_upgradeCountCurrentStamina == _upgradeCountMaxStamina)
            {
                _buttonStamina.interactable = false;
                _textStaminaSold.SetActive(true);
            }
        }
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
                _textDamageCost.text = "Cost: " + _upgradeCostDamage;
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
                _textInventorySlotCost.text = "Cost: " + _upgradeCostInventorySlot;
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
                _textStackSizeCost.text = "Cost: " + _upgradeCostStackSize;
                _textStackSizeDesc.text = "Upgrade Stackable Resource Size";
                _textStackSizeLevel.text = "Level " + _upgradeCountCurrentStackSize + " of " + _upgradeCountMaxStackSize;
            }

            if (_upgradeCountCurrentStackSize == _upgradeCountMaxStackSize)
            {
                _buttonStackSize.interactable = false;
                _textStackSizeSold.SetActive(true);
            }
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

    public enum Upgrade
    {
        InventorySlot,
        ResourceStackSize,

    }
}
