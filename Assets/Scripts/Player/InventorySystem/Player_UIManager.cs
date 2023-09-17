using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _inventory_UI;
    [SerializeField]
    private GameObject _fannyPack_UI;
    [SerializeField]
    private GameObject _contracts_UI;
    [SerializeField]
    private GameObject _options_UI;
    [SerializeField]
    private GameObject _playerHUD_UI;
    [SerializeField]
    private GameObject _creditBox;
    [SerializeField]
    private GameObject _bulletinBoard_UI;
    [SerializeField]
    private GameObject _bulletinBoardInteract_UI;

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.BULLETINBOARD_INTERACT, OnDisplayToggle_Bulletin);
        EventBus.Subscribe(EventType.TOGGLE_INTERACT_HOVER, OnDisplayToggle_BulletinInteract);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.BULLETINBOARD_INTERACT, OnDisplayToggle_Bulletin);
        EventBus.Unsubscribe(EventType.TOGGLE_INTERACT_HOVER, OnDisplayToggle_BulletinInteract);
    }

    public void OnDisplay_Inventory()
    {
        _fannyPack_UI.SetActive(true);
        _inventory_UI.SetActive(true);
        _contracts_UI.SetActive(false);
        _options_UI.SetActive(false);
        _creditBox.SetActive(true);
    }

    public void OnDisplay_Contacts()
    {
        _inventory_UI.SetActive(false);
        _contracts_UI.SetActive(true);
        _options_UI.SetActive(false);
    }

    public void OnDisplay_Options()
    {
        _inventory_UI.SetActive(false);
        _contracts_UI.SetActive(false);
        _options_UI.SetActive(true);
    }

    public void OnDisplay_PlayerHUD()
    {
        _fannyPack_UI.SetActive(false);
        _inventory_UI.SetActive(false);
        _contracts_UI.SetActive(false);
        _options_UI.SetActive(false);
        _playerHUD_UI.SetActive(true);
        _bulletinBoard_UI.SetActive(false);
        _creditBox.SetActive(false);
    }

    public void OnDisplayToggle_BulletinInteract()
    {
        Debug.Log("Toggle interact prompt");
        if (_bulletinBoardInteract_UI.activeSelf)
            _bulletinBoardInteract_UI.SetActive(false);
        else
            _bulletinBoardInteract_UI.SetActive(true);
    }

    public void OnDisplayToggle_Bulletin()
    {
        if (_bulletinBoardInteract_UI.activeSelf ||
            !_bulletinBoard_UI.activeSelf)
        {
            if (_bulletinBoard_UI.activeSelf)
                OnDisplay_ExitBulletin();

            else
                OnDisplay_EnterBulletin();
        }
    }

    public void OnDisplay_EnterBulletin()
    {
        _inventory_UI.SetActive(false);
        _contracts_UI.SetActive(false);
        _options_UI.SetActive(false);
        _playerHUD_UI.SetActive(false);
        _fannyPack_UI.SetActive(false);
        _bulletinBoard_UI.SetActive(true);
        _creditBox.SetActive(true);
    }

    public void OnDisplay_ExitBulletin()
    {
        _inventory_UI.SetActive(false);
        _contracts_UI.SetActive(false);
        _options_UI.SetActive(false);
        _playerHUD_UI.SetActive(true);
        _bulletinBoard_UI.SetActive(false);
        _creditBox.SetActive(false);
    }
}