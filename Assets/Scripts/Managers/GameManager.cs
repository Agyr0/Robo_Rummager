using Cinemachine;
using Cinemachine.PostFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : Singleton<GameManager>
{
    public VolumeProfile scannerGogglesVolume;
    public VolumeProfile defaultVolume;

    [HideInInspector]
    public PlayerController playerController;
    [HideInInspector]
    public WeaponController weaponController;
    [HideInInspector]
    public CinemachineInputProvider inputProvider;
    [HideInInspector]
    public Player_InventoryManager inventoryManager;


    private CinemachineVirtualCamera playerVCam;
    public CinemachineVirtualCamera PlayerVCam
    {
        get { return playerVCam; }
        set { playerVCam = value; }
    }

    private Transform _cameraTransform;
    public Transform CameraTransform
    {
        get 
        {
            if (_cameraTransform == null)
                _cameraTransform = Camera.main.transform;
            return _cameraTransform;
        }
        private set 
        {
            _cameraTransform = value; 
        }
    }

    private bool _inUI = false;

    public bool InUI 
    { 
        get { return _inUI; } 
        set
        { 
            //toggle weapon controller and input provider
            weaponController.enabled = !value;
            inputProvider.enabled = !value;
            _inUI = value;
            Debug.Log("Ran set for InUI");
            if(value)
            {
                //Set cursor to visible
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                //Set cursor to hidden
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        } 
    }

    private void OnEnable()
    {
        
        EventBus.Subscribe(EventType.INVENTORY_TOGGLE, ToggleInput);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.INVENTORY_TOGGLE, ToggleInput);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
        Cursor.visible = false;

        playerVCam = (CinemachineVirtualCamera)Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera;
        inputProvider = PlayerVCam.gameObject.GetComponent<CinemachineInputProvider>();
        EventBus.Publish(EventType.REFRESH_RESOURCES);
    }

    private void ToggleInput()
    {
        InUI = !InUI;
    }

}
