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

    private CinemachineStoryboard _storyboard;
    public CinemachineStoryboard Storyboard { get { return _storyboard; } }
    [SerializeField]
    private CinemachineVirtualCamera playerVCam;
    public CinemachineVirtualCamera PlayerVCam
    {
        get { return playerVCam; }
        set { playerVCam = value; }
    }


    [SerializeField]
    private CinemachineStateDrivenCamera _stateDrivenCamera;
    public CinemachineStateDrivenCamera StateDrivenCamera
    {
        get { return _stateDrivenCamera; }
        set { _stateDrivenCamera = value; }
    }

    private Animator _camAnimator;
    private bool isPlayerCam = true;

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

    private bool _inUI = true;

    public bool InUI 
    { 
        get { return _inUI; } 
        set
        { 
            //toggle weapon controller and input provider
            weaponController.enabled = !value;
            inputProvider.enabled = !value;
            playerController.CanMove = !value;
            playerController.CanJump = !value;
            playerController.CanSprint = !value;
            playerController.CanDash = !value;
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
        EventBus.Subscribe(EventType.TOGGLE_WORKBENCH_CAM_BLEND, SwitchWorkbenchCam);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.INVENTORY_TOGGLE, ToggleInput);
        EventBus.Unsubscribe(EventType.TOGGLE_WORKBENCH_CAM_BLEND, SwitchWorkbenchCam);
    }
    public override void Awake()
    {
        base.Awake();
        //playerVCam = (CinemachineVirtualCamera)Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera;
        inputProvider = PlayerVCam.gameObject.GetComponent<CinemachineInputProvider>();
        _storyboard = PlayerVCam.gameObject.GetComponent<CinemachineStoryboard>();
        _camAnimator = StateDrivenCamera.gameObject.GetComponent<Animator>();

    }
    private void Start()
    {
        inputProvider.enabled = false;
        weaponController.enabled = false;
        EventBus.Publish(EventType.REFRESH_RESOURCES);
    }

    private void ToggleInput()
    {
        InUI = !InUI;
    }

    private void SwitchWorkbenchCam()
    {
        isPlayerCam = !isPlayerCam;

        if (isPlayerCam)
            _camAnimator.Play("PlayerCam");
        else if(!isPlayerCam)
            _camAnimator.Play("WorkbenchCam");

    }
}
