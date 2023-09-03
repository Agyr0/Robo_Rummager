using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tier1BaseController : MonoBehaviour, IInteractable
{
    private bool pickedUp = false;

    public void HandleInteract()
    {
        if(!pickedUp)
        {
            pickedUp = true;
            transform.position = GameManager.Instance.playerController.handTransform.position;
            transform.parent = GameManager.Instance.playerController.handTransform;
        }
        else
        {
            pickedUp = false;
            transform.parent = null;
        }
    }
}
