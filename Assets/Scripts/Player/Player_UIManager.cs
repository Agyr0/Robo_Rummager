using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _inventory_UI;
    [SerializeField]
    private GameObject _contracts_UI;
    [SerializeField]
    private GameObject _options_UI;

    public void OnDisplayInventory()
    {
        _inventory_UI.SetActive(true);
        _contracts_UI.SetActive(false);
        _options_UI.SetActive(false);
    }

    public void OnDisplayContacts()
    {
        _inventory_UI.SetActive(false);
        _contracts_UI.SetActive(true);
        _options_UI.SetActive(false);
    }

    public void OnDisplayOptions()
    {
        _inventory_UI.SetActive(false);
        _contracts_UI.SetActive(false);
        _options_UI.SetActive(true);
    }


    
}
