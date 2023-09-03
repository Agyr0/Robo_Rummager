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
    [SerializeField]
    private float staminaRegen = 5f;


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


    private bool isSprinting, isDashing = false;

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
    #endregion

    private void OnEnable()
    {
        EventBus.Subscribe(EventType.PLAYER_START_SPRINT, ReduceStamina);
        EventBus.Subscribe(EventType.PLAYER_STOP_SPRINT, IncreaseStamina);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.PLAYER_START_SPRINT, ReduceStamina);
        EventBus.Unsubscribe(EventType.PLAYER_STOP_SPRINT, IncreaseStamina);

    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        inputManager = InputManager.Instance;   
        CurSpeed = walkSpeed;
        CanMove = _canMove;
        CanJump = _canJump;
        CurStamina = staminaMax;
    }

    private void Update()
    {
        if (CanMove)
            HandleMovement();
        if (CanJump)
            HandleJump();
        if(CanSprint) 
            HandleSprint();
        
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
        if(CurStamina > 0 && inputManager.GetPlayerSprint())
        {
            if (!isSprinting)
            {
                CurSpeed = runSpeed;
                //EventBus.Publish(EventType.PLAYER_START_SPRINT);
                isSprinting = true;
            }
        }
        else if( !inputManager.GetPlayerSprint() || CurStamina <= 0)
        {
            //EventBus.Publish(EventType.PLAYER_STOP_SPRINT);
            CurSpeed = walkSpeed;
            isSprinting = false;
        }
    }


    #region Handle Stamina

    private void ReduceStamina()
    {
        while(CurStamina > 0 - staminaDrain)
        {
            CurStamina -= staminaDrain * Time.deltaTime;
        }
        CurStamina = 0f;
    }

    private void IncreaseStamina()
    {
        while(CurStamina < staminaMax - staminaRegen)
        {
            CurStamina += staminaRegen * Time.deltaTime;
        }
        CurStamina = staminaMax;
    }


    //private IEnumerator ReduceStamina()
    //{
    //    while (true && CurStamina > 0)
    //    {
    //        yield return new WaitForSeconds(1f);
    //        CurStamina--;
    //        yield return null;
    //    }
    //}
    //private IEnumerator ReduceStamina(float modifier)
    //{
    //    while (true && CurStamina > 0)
    //    {
    //        yield return new WaitForSeconds(1f);
    //        CurStamina = CurStamina - modifier;
    //        yield return null;
    //    }
    //}
    //private IEnumerator ReduceStamina(float modifier, float waitTime)
    //{
    //    while (true && CurStamina > 0)
    //    {
    //        yield return new WaitForSeconds(waitTime);
    //        CurStamina = CurStamina - modifier;
    //        yield return null;
    //    }
    //}

    //private IEnumerator IncreaseStamina()
    //{
    //    while(true && CurStamina < staminaMax)
    //    {
    //        yield return new WaitForSeconds(1f);
    //        CurStamina++;
    //        yield return null;
    //    }
    //}
    #endregion
}

