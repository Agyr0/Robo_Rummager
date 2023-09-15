using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField]
    private float interactionDistance = 5f;
    private RaycastHit hit;
    bool hittingInteractable;
    bool isShowing = false;


    private void OnEnable()
    {
        EventBus.Subscribe(EventType.TOGGLE_INTERACT_HOVER, DebugInteractHover);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.TOGGLE_INTERACT_HOVER, DebugInteractHover);
    }

    private void Start()
    {
        StartCoroutine(CheckForInteract());
        //subscribe to event for when the player presses interaction key
        InputManager.Instance.playerControls.Player.Interact.performed += _ => SendInteract(hit);
    }

    
    //Run HandleInteract on hit object
    private void SendInteract(RaycastHit _hit)
    {
        if (_hit.transform.gameObject.GetComponent<IInteractable>() == null)
            return;

        //Run HandleInteract on interactable object hit
        _hit.transform.gameObject.GetComponent<IInteractable>().HandleInteract();   
    }

    //Check and assign hit with what youre looking at
    private RaycastHit HandleInteractHover()
    {
        hittingInteractable = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionDistance) && hit.transform.gameObject.GetComponent<IInteractable>() != null;

        if (hittingInteractable && !isShowing)
        {
            isShowing = true;
            EventBus.Publish(EventType.TOGGLE_INTERACT_HOVER);
        }
        else if (!hittingInteractable && isShowing)
        {
            EventBus.Publish(EventType.TOGGLE_INTERACT_HOVER);
            isShowing = false;

        }

        return hit;
    }

    //Run HandleInteractHover every .25 seconds
    private IEnumerator CheckForInteract()
    {
        while (true)
        {
            hit = HandleInteractHover();
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void DebugInteractHover()
    {
        Debug.Log("Toggling Interact hover");
    }
}
