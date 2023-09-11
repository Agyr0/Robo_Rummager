using Cinemachine.PostFX;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;


public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;

    private float _health;
    private float _maxHealth = 100f;

    public float Health
    { 
        get { return _health; } 
         set {_health = value; } 
    }
    #region Player Movement
    private CharacterController controller;
    private Vector3 playerVelocity;

    private float _curSpeed;
    [Header("Movement")]
    [SerializeField]
    private float walkSpeed = 7f;
    [SerializeField]
    private float runSpeed = 12f;
    [SerializeField]
    private float dashSpeed = 20f;
    [SerializeField]
    private float _curStamina;
    [SerializeField]
    private float staminaMax = 100f;
    [SerializeField]
    private float staminaDrain = 3f;
    private float staminaRegen;


    [Space(5)]
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    public float CurSpeed
    {
        get { return _curSpeed; }
        private set { _curSpeed = value; }
    }
    public float CurStamina
    {
        get { return _curStamina; }
        private set { _curStamina = value; }
    }
    public bool GroundedPlayer
    {
        get { return controller.isGrounded; }
    }
    #endregion

    #region Input
    private InputManager inputManager;
    [Space(10)]
    [Header("Restrictions")]
    [SerializeField]
    private bool _canMove = true;
    [SerializeField]
    private bool _canJump = true;
    [SerializeField]
    private bool _canSprint = true;
    [SerializeField]
    private bool _canDash = true;

    private bool isSprinting, isDashing, scannerActive = false;
    private Coroutine startSprint;
    private Coroutine stopSprint;
    private Coroutine playerDash;


    public bool CanMove
    {
        get { return _canMove; }
        private set { _canMove = value; }
    }
    public bool CanJump
    {
        get { return _canJump; }
        private set { _canJump = value; }
    }
    public bool CanSprint
    {
        get { return _canSprint; }
        private set { _canSprint = value; }
    }
    public bool CanDash
    {
        get { return _canDash; }
        private set { _canDash = value; }
    }
    #endregion

    #region Holding Robots
    public Transform handTransform;
    [HideInInspector]
    public bool holdingRobot = false;

    #endregion

    private CinemachineVolumeSettings scannerVolume;

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.PLAYER_START_SPRINT, PlayerStartSprint);
        EventBus.Subscribe(EventType.PLAYER_STOP_SPRINT, PlayerStopSprint);
        EventBus.Subscribe(EventType.PLAYER_DASH, StartDash);
        EventBus.Subscribe(EventType.TOGGLE_SCANNER, ToggleScanner);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.PLAYER_START_SPRINT, PlayerStartSprint);
        EventBus.Unsubscribe(EventType.PLAYER_STOP_SPRINT, PlayerStopSprint);
        EventBus.Unsubscribe(EventType.PLAYER_DASH, StartDash);
        EventBus.Unsubscribe(EventType.TOGGLE_SCANNER, ToggleScanner);

    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.playerController = this;

        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;   
        CurSpeed = walkSpeed;
        CanMove = _canMove;
        CanJump = _canJump;
        CurStamina = staminaMax;
        staminaRegen = staminaDrain * 2f;

        //Input Events
        SubscribeInputEvents();

        scannerVolume = gameManager.PlayerVCam.GetComponent<CinemachineVolumeSettings>();
    }

    private void Update()
    {
        if (CanMove)
            HandleMovement();
        if (CanJump)
            HandleJump();
    }

    private void SubscribeInputEvents()
    {
        //Sprint and stamina
        inputManager.playerControls.Player.Sprint.performed += _ => EventBus.Publish(EventType.PLAYER_START_SPRINT);
        inputManager.playerControls.Player.Sprint.canceled += _ => EventBus.Publish(EventType.PLAYER_STOP_SPRINT);
        //Dash
        inputManager.playerControls.Player.Dash.performed += _ => EventBus.Publish(EventType.PLAYER_DASH);
        //ScannerGoggles
        inputManager.playerControls.Player.Scanner.performed += _ => EventBus.Publish(EventType.TOGGLE_SCANNER);
    }
    private void HandleMovement()
    {

        //Keep y velocity 0 if grounded
        if (GroundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 _movement = inputManager.playerControls.Player.Movement.ReadValue<Vector2>();
        Vector3 move = new Vector3(_movement.x, 0f, _movement.y);
        move = gameManager.CameraTransform.forward * move.z + gameManager.CameraTransform.right * move.x;
        move.y = 0f;
        move.Normalize();

        controller.Move(move * Time.deltaTime * CurSpeed);


        //Handle Gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //Keep player facing the same direction as the camera
        transform.rotation = Quaternion.Euler(0, gameManager.CameraTransform.rotation.eulerAngles.y, 0);
    }
    private void HandleJump()
    {
        // Changes the height position of the player..
        if (inputManager.playerControls.Player.Jump.triggered && GroundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        
    }

    private void StartDash()
    {
        if (playerDash == null && !isDashing && CanDash)
            playerDash = StartCoroutine(StartDashCoolDown());
        else
            playerDash = null;
    }

    private IEnumerator StartDashCoolDown()
    {
        isDashing = true;
        CurSpeed = dashSpeed;
        yield return new WaitForSeconds(0.25f);
        CurSpeed = walkSpeed;
        yield return new WaitForSeconds(5f);
        isDashing = false;
    }

    #region Handle Stamina

    private void PlayerStartSprint()
    {
        if (CanSprint)
        {
            isSprinting = true;
            StopCoroutine(IncreaseStamina());
            stopSprint = null;
            if (startSprint == null)
                startSprint = StartCoroutine(ReduceStamina());
        }
    }

    private void PlayerStopSprint()
    {
        if (CanSprint)
        {
            isSprinting = false;
            StopCoroutine(ReduceStamina());
            startSprint = null;
            if (stopSprint == null)
                stopSprint = StartCoroutine(IncreaseStamina());
        }
    }


    private IEnumerator ReduceStamina()
    {

        while (true && CurStamina >= (0 + staminaDrain) && isSprinting)
        {
            CurSpeed = runSpeed;
            yield return new WaitForSeconds(1f);
            CurStamina -= staminaDrain;
            yield return null;
        }
        CurSpeed = walkSpeed;

    }

    private IEnumerator IncreaseStamina()
    {

        while (true && CurStamina <= staminaMax && !isSprinting)
        {
            CurSpeed = walkSpeed;
            yield return new WaitForSeconds(1f);
            CurStamina += staminaRegen;
            if(CurStamina > staminaMax)
                CurStamina = staminaMax;
            yield return null;
        }
    }
    #endregion

    #region Scanner Goggles
    private void ToggleScanner()
    {
        scannerActive = !scannerActive;
        scannerVolume.enabled = scannerActive;
    }

    #endregion
}

