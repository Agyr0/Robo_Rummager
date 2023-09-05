using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField]
    private float interactionDistance = 10f;

    private void Start()
    {
        //subscribe to event for when the player presses interaction key
        InputManager.Instance.playerControls.Player.Interact.performed += _ =>
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, interactionDistance))
            {
                //If not looking at an IInteractable return
                if (hit.transform.gameObject.GetComponent<IInteractable>() == null)
                    return;

                //Run HandleInteract on interactable object hit
                hit.transform.gameObject.GetComponent<IInteractable>().HandleInteract();
            }
        };
    }   
}
