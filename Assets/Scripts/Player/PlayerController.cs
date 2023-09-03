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
    #region Player Movement
    private Transform cameraTransform;
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

    private bool isSprinting, isDashing = false;
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



    private void OnEnable()
    {
        EventBus.Subscribe(EventType.PLAYER_START_SPRINT, PlayerStartSprint);
        EventBus.Subscribe(EventType.PLAYER_STOP_SPRINT, PlayerStopSprint);
        EventBus.Subscribe(EventType.PLAYER_DASH, StartDash);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.PLAYER_START_SPRINT, PlayerStartSprint);
        EventBus.Unsubscribe(EventType.PLAYER_STOP_SPRINT, PlayerStopSprint);
        EventBus.Unsubscribe(EventType.PLAYER_DASH, StartDash);

    }

    private void Start()
    {
        GameManager.Instance.playerController = this;

        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        inputManager = InputManager.Instance;   
        CurSpeed = walkSpeed;
        CanMove = _canMove;
        CanJump = _canJump;
        CurStamina = staminaMax;
        staminaRegen = staminaDrain * 2f;
    }

    private void Update()
    {
        if (CanMove)
            HandleMovement();
        if (CanJump)
            HandleJump();
        if(CanSprint) 
            HandleSprint();
        if(CanDash)
            HandleDash();
    }

    private void HandleMovement()
    {

        //Keep y velocity 0 if grounded
        if (GroundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 _movement = inputManager.GetMovement();
        Vector3 move = new Vector3(_movement.x, 0f, _movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        move.Normalize();

        controller.Move(move * Time.deltaTime * CurSpeed);


        //Handle Gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //Keep player facing the same direction as the camera
        transform.rotation = Quaternion.Euler(0, cameraTransform.rotation.eulerAngles.y, 0);
    }
    private void HandleJump()
    {
        // Changes the height position of the player..
        if (inputManager.GetPlayerJump() && GroundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        
    }

    private void HandleSprint()
    {
        inputManager.playerControls.Player.Sprint.performed += context =>
        {
            EventBus.Publish(EventType.PLAYER_START_SPRINT);
        };
        inputManager.playerControls.Player.Sprint.canceled += context =>
        {
            EventBus.Publish(EventType.PLAYER_STOP_SPRINT);
        };
    }

    private void HandleDash()
    {
        inputManager.playerControls.Player.Dash.performed += context =>
        {
            EventBus.Publish(EventType.PLAYER_DASH);
        };
    }

    private void StartDash()
    {
        if (playerDash == null && !isDashing)
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
        isSprinting = true;
        StopCoroutine(IncreaseStamina());
        stopSprint = null;
        if(startSprint == null)
            startSprint = StartCoroutine(ReduceStamina());
    }

    private void PlayerStopSprint()
    {
        isSprinting = false;
        StopCoroutine(ReduceStamina());
        startSprint = null;
        if(stopSprint == null)
            stopSprint = StartCoroutine(IncreaseStamina());
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
}

