using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Contract_Cancel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject _cancel_Text;
    
    public void FailContract(bool buttonPressed)
    {
        if (buttonPressed)
            GameManager.Instance.inventoryManager.player_Contract_Manager.Contract_Status = ContractStatus.Failed;

        else
            _cancel_Text.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!GameManager.Instance.inventoryManager.player_Contract_Manager.firstContract)
            _cancel_Text.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _cancel_Text.SetActive(false);
    }
}
